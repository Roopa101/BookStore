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
    public class FeedBackRL : IFeedBackRL
    {
        private SqlConnection sqlConnection;

        public FeedBackRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public string AddFeedBack(FeedBackPostModel feedBackPost)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("Sp_AddFeedbackBook", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@UserId", feedBackPost.userId);
                    cmd.Parameters.AddWithValue("@BookId", feedBackPost.BookId);
                    cmd.Parameters.AddWithValue("@FeedBackFromUserName", feedBackPost.FeedBackFromUserName);
                    cmd.Parameters.AddWithValue("@Comments", feedBackPost.Comments);
                    cmd.Parameters.AddWithValue("@Ratings", feedBackPost.Ratings);

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    return "FeedBack added Successfully";

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

    public List<FeedBackModel> GetAllFeedBacks(int BookId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    List<FeedBackModel> order = new List<FeedBackModel>();
                    SqlCommand cmd = new SqlCommand("sp_GetAllFeedBack", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    sqlConnection.Open();
                    SqlDataReader fetch = cmd.ExecuteReader();
                    if (fetch.HasRows)
                    {
                        while (fetch.Read())
                        {
                            FeedBackModel model = new FeedBackModel();
                            model.FeedbackId = Convert.ToInt32(fetch["FeedbackId"]);
                            model.userId = Convert.ToInt32(fetch["UserId"]);
                            model.BookId = Convert.ToInt32(fetch["BookId"]);
                            model.FeedBackFromUserName = fetch["FeedBackFromUserName"].ToString();
                            model.Comments = fetch["Comments"].ToString();
                            model.Ratings = Convert.ToInt32(fetch["Ratings"]);
                            order.Add(model);

                        }
                        return order;
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