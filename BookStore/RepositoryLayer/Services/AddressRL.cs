using CommonLayer.User;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class AddressRL : IAddressRL
    {
        private SqlConnection sqlConnection;

        public AddressRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public string AddAddress(int userId, AddressPostModel addressPost)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("sp_AddAddress", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    cmd.Parameters.AddWithValue("@City", addressPost.City);
                    cmd.Parameters.AddWithValue("@State", addressPost.State);
                    cmd.Parameters.AddWithValue("@Address", addressPost.Address);
                    cmd.Parameters.AddWithValue("@TypeId", addressPost.IdType);

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    return "Address added  successfully";

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }





        public bool UpdateAddress(int AddressId, AddressPostModel addressPost)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateAddress", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AddressId", AddressId);
                    cmd.Parameters.AddWithValue("@City", addressPost.City);
                    cmd.Parameters.AddWithValue("@State", addressPost.State);
                    cmd.Parameters.AddWithValue("@Address", addressPost.Address);
                    cmd.Parameters.AddWithValue("@TypeId", addressPost.IdType);

                    sqlConnection.Open();
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result != 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public bool DeleteAddress(int AddressId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteAddress", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AddressId", AddressId);
                    sqlConnection.Open();
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result != 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }


                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public List<AddressModel> GetAllAddress()
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    List<AddressModel> address = new List<AddressModel>();
                    SqlCommand cmd = new SqlCommand("sp_GetAllAddresses", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    sqlConnection.Open();
                    SqlDataReader fetch = cmd.ExecuteReader();
                    if (fetch.HasRows)
                    {
                        while (fetch.Read())
                        {
                            AddressModel model = new AddressModel();
                            model.userId = Convert.ToInt32(fetch["userId"]);
                            model.AddressId = Convert.ToInt32(fetch["AddressId"]);
                            model.Address = fetch["Address"].ToString();
                            model.City = fetch["City"].ToString();
                            model.State = fetch["State"].ToString();
                            model.TypeId = Convert.ToInt32(fetch["TypeId"]);


                            address.Add(model);
                        }
                        return address;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }

        }


        public List<AddressModel> GetAddressByAddressId(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    List<AddressModel> address = new List<AddressModel>();
                    SqlCommand cmd = new SqlCommand("sp_GetAddressbyUserid", sqlConnection);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userId", userId);
                    sqlConnection.Open();
                    SqlDataReader fetch = cmd.ExecuteReader();
                    if (fetch.HasRows)
                    {
                        while (fetch.Read())
                        {
                            AddressModel model = new AddressModel();
                            model.userId = Convert.ToInt32(fetch["UserId"]);
                            model.AddressId = Convert.ToInt32(fetch["AddressId"]);
                            model.Address = fetch["Address"].ToString();
                            model.City = fetch["City"].ToString();
                            model.State = fetch["State"].ToString();
                            model.TypeId = Convert.ToInt32(fetch["TypeId"]);


                            address.Add(model);
                        }
                        return address;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                sqlConnection.Close();
            }




        }

    }
}

