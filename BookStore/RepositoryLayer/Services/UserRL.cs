using CommonLayer.User;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;



namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        private readonly IConfiguration _config;
        public string ConnectionStringName { get; set; } = "BookStore";
        public UserRL(IConfiguration config)
        {
            _config = config;
        }

        public void UserRegistration(UserPostModel user)
        {


            string connectionString = _config.GetConnectionString(ConnectionStringName);

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("sp_UserRegister", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@FullName", user.FullName);
                cmd.Parameters.AddWithValue("@EmailId", user.EmailId);
                cmd.Parameters.AddWithValue("@PhNo", user.PhNo);
                cmd.Parameters.AddWithValue("@Password", user.Password);


                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public string Login(string EmailId, string Password)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    UserLogin userLogin = new UserLogin();
                    SqlCommand cmd = new SqlCommand("sp_UserLogin", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@EmailId", EmailId);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();


                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            userLogin.EmailId = reader["EmailId"].ToString();
                            userLogin.Password = reader["Password"].ToString();
                        }
                        string token = GenerateToken(EmailId);
                        return token;
                    }
                    else
                    {
                        return null;
                    }

                    con.Close();
                }
            }

            catch (Exception e)
            {
                throw e;
            }
        }
        private static string GenerateToken(string EmailId)
        {
            if (EmailId == null)
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("EmailId", EmailId),
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private static string GenerateJWTToken(string EmailId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("EmailId", EmailId),
                 //   new Claim("userId", userId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public bool ForgetPassword(string EmailId)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    UserPostModel model = new UserPostModel();
                    SqlCommand command = new SqlCommand("sp_ForgetPassword", con);

                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@EmailId", EmailId);
                    con.Open();
                    var result = command.ExecuteNonQuery();


                    MessageQueue queue;
                    //ADD MESSAGE TO QUEUE
                    if (MessageQueue.Exists(@".\Private$\BookStoreQueue"))
                    {
                        queue = new MessageQueue(@".\Private$\BookStoreQueue");
                    }
                    else
                    {
                        queue = MessageQueue.Create(@".\Private$\BookStoreQueue");
                    }

                    Message MyMessage = new Message();
                    MyMessage.Formatter = new BinaryMessageFormatter();
                    MyMessage.Body = GenerateJWTToken(EmailId);
                    MyMessage.Label = "Forget Password Email";
                    queue.Send(MyMessage);
                    Message msg = queue.Receive();
                    msg.Formatter = new BinaryMessageFormatter();
                    EmailServices.sendmail(EmailId, msg.Body.ToString());
                    queue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                    queue.BeginReceive();
                    queue.Close();
                    return true;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            //finally
            //{
            //    sqlConnection.Close();
            //}

        }
        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailServices.sendmail(e.Message.ToString(), GenerateToken(e.Message.ToString()));
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode ==

                    MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " +
                        "Queue might be a system queue.");
                }

            }
        }

        public bool ResetPassword(string EmailId, string Password)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);
            try
            {

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    UserPostModel model = new UserPostModel();
                    SqlCommand command = new SqlCommand("sp_ResetPassword", con);

                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@EmailId", EmailId);
                    command.Parameters.AddWithValue("@Password", Password);
                    con.Open();
                    SqlDataReader dr = command.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            model.EmailId = Convert.ToString(dr["EmailId"] == DBNull.Value ? default : dr["EmailId"]);
                            model.Password = Convert.ToString(dr["Password"] == DBNull.Value ? default : dr["Password"]);

                        }
                        return true;
                    }
                    else
                    {
                        return false;

                    }
                    con.Close();

                }

            }
            catch (Exception e)
            {
                throw e;
            }
         

        }
    }
 }


