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
    public class OrderRL : IOrderRL
    {
        private SqlConnection sqlConnection;

        public OrderRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public List<OrderModel> OrderBooks(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    List<OrderModel> order = new List<OrderModel>();
                    SqlCommand cmd = new SqlCommand("sp_GetAllOrder", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userId", userId);
                    sqlConnection.Open();
                    SqlDataReader fetch = cmd.ExecuteReader();
                    if (fetch.HasRows)
                    {
                        while (fetch.Read())
                        {
                            OrderModel model = new OrderModel();
                            BookPostModel bookModel = new BookPostModel();
                            model.OrderId = Convert.ToInt32(fetch["OrderId"]);
                            model.UserId = Convert.ToInt32(fetch["UserId"]);
                            model.AddressId = Convert.ToInt32(fetch["AddressId"]);
                            bookModel.BookName = fetch["BookName"].ToString();
                            bookModel.AuthorName = fetch["AuthorName"].ToString();
                            bookModel.DiscountPrice = Convert.ToInt32(fetch["DiscountPrice"]);
                            bookModel.OriginalPrice = Convert.ToInt32(fetch["OriginalPrice"]);
                            bookModel.BookDescription = fetch["BookDescription"].ToString();
                            bookModel.Rating = Convert.ToInt32(fetch["Rating"]);
                            bookModel.Reviewer = Convert.ToInt32(fetch["Reviewer"]);
                            bookModel.Image = fetch["Image"].ToString();
                            bookModel.BookCount = Convert.ToInt32(fetch["BookCount"]);
                            model.BookId = Convert.ToInt32(fetch["BookId"]);
                            model.BookQuantity = Convert.ToInt32(fetch["BookQuantity"]);
                            model.TotalPrice = Convert.ToInt32(fetch["TotalPrice"]);
                            model.bookModel = bookModel;
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

        public string AddOrder(OrderPostModel orderPost)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("sp_BooksOrder", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@userId", orderPost.userId);
                    cmd.Parameters.AddWithValue("@BookId", orderPost.BookId);
                    cmd.Parameters.AddWithValue("@AddressId", orderPost.AddressId);
                    cmd.Parameters.AddWithValue("@TotalPrice", orderPost.TotalPrice);
                    cmd.Parameters.AddWithValue("@BookQuantity", orderPost.BookQuantity);
                    cmd.Parameters.AddWithValue("@OrderDate", orderPost.OrderDate);
                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    return "books ordered successfully";

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

        public bool DeleteOrder(int OrderId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteOrder", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@OrderId", OrderId);
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
    }
}
