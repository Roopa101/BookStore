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
    public class WishListRL : IWishListRL
    {
        private SqlConnection sqlConnection;

        public WishListRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public string AddBookinWishList(WishListPostModel wishListPost)
        {

            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("sp_CreateWishlist", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;


                    cmd.Parameters.AddWithValue("@UserId", wishListPost.UserId);
                    cmd.Parameters.AddWithValue("@BookId", wishListPost.BookId);

                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    return "book is added in WishList successfully";

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

       
        public bool RemoveBookinWishList(int WishListId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteWishlist", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@WishListId", WishListId);
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

        public List<WishListModel> GetAllBooksinWishList(int userId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    List<WishListModel> wishList = new List<WishListModel>();
                    SqlCommand cmd = new SqlCommand("sp_GetWishListbyUserId", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", userId);
                    sqlConnection.Open();
                    SqlDataReader fetch = cmd.ExecuteReader();
                    if (fetch.HasRows)
                    {
                        while (fetch.Read())
                        {
                            WishListModel model = new WishListModel();
                            BookPostModel bookModel = new BookPostModel();
                            model.UserId = Convert.ToInt32(fetch["UserId"]);
                            model.WishListId = Convert.ToInt32(fetch["WishListId"]);
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
                            model.bookModel = bookModel;
                            wishList.Add(model);

                        }
                        return wishList;
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