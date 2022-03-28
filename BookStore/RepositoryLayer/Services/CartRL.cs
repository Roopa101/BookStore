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
    public class CartRL : ICartRL
    {
        private SqlConnection sqlConnection;

        public CartRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public string AddBookToCart(CartPostModel cartBook)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("sp_AddingBookinCart", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Quantity", cartBook.Quantity);
                    cmd.Parameters.AddWithValue("@userId", cartBook.userId);
                    cmd.Parameters.AddWithValue("@BookId", cartBook.BookId);

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    return "book added in cart successfully";

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

        public bool UpdateCart(int CartId, int Quantity)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateCart", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CartId", CartId);
                    cmd.Parameters.AddWithValue("@Quantity", Quantity);
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
        public string DeleteCart(int CartId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteCart", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CartId", CartId);
                    sqlConnection.Open();
                    int result = Convert.ToInt32(cmd.ExecuteScalar());
                    if (result != 1)
                    {
                        return "Book Deleted in Cart Successfully";
                    }
                    else
                    {
                        return "Book is not Deleted";
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

        public List<CartModel> GetAllBooksinCart(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    List<CartModel> cart = new List<CartModel>();
                    SqlCommand cmd = new SqlCommand("sp_GetAllBooksinCart", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userId", userId);
                    sqlConnection.Open();
                    SqlDataReader fetch = cmd.ExecuteReader();
                    if (fetch.HasRows)
                    {
                        while (fetch.Read())
                        {
                            CartModel model = new CartModel();
                            BookPostModel bookModel = new BookPostModel();
                            model.userId = Convert.ToInt32(fetch["userId"]);
                            model.CartID = Convert.ToInt32(fetch["CartID"]);
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
                            model.Quantity = Convert.ToInt32(fetch["Quantity"]);
                            model.bookModel = bookModel;
                            cart.Add(model);

                        }
                        return cart;
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
