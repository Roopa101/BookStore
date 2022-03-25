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
    public class BookRL : IBookRL
    {
        private SqlConnection sqlConnection;

        public BookRL(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public string AddBooks(BookPostModel addBook)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("sp_AddingBooks", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookName", addBook.BookName);
                    cmd.Parameters.AddWithValue("@AuthorName", addBook.AuthorName);
                    cmd.Parameters.AddWithValue("@DiscountPrice", addBook.DiscountPrice);
                    cmd.Parameters.AddWithValue("@OriginalPrice", addBook.OriginalPrice);
                    cmd.Parameters.AddWithValue("@BookDescription", addBook.BookDescription);
                    cmd.Parameters.AddWithValue("@Rating ", addBook.Rating);
                    cmd.Parameters.AddWithValue("@Reviewer", addBook.Reviewer);
                    cmd.Parameters.AddWithValue("@Image", addBook.Image);
                    cmd.Parameters.AddWithValue("@BookCount", addBook.BookCount);


                    sqlConnection.Open();
                    cmd.ExecuteNonQuery();
                    return "book added successfully";

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

      

        public bool UpdateBooks(int BookId, BookPostModel updateBook)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("sp_UpdateBooks", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    cmd.Parameters.AddWithValue("@BookName", updateBook.BookName);
                    cmd.Parameters.AddWithValue("@AuthorName", updateBook.AuthorName);
                    cmd.Parameters.AddWithValue("@DiscountPrice", updateBook.DiscountPrice);
                    cmd.Parameters.AddWithValue("@OriginalPrice", updateBook.OriginalPrice);
                    cmd.Parameters.AddWithValue("@BookDescription", updateBook.BookDescription);
                    cmd.Parameters.AddWithValue("@Rating ", updateBook.Rating);
                    cmd.Parameters.AddWithValue("@Image", updateBook.Image);
                    cmd.Parameters.AddWithValue("@Reviewer", updateBook.Reviewer);
                    cmd.Parameters.AddWithValue("@BookCount", updateBook.BookCount);
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
        public bool DeleteBook(int BookId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));

            try
            {
                using (sqlConnection)
                {
                    SqlCommand cmd = new SqlCommand("sp_DeleteBooks", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);
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

       
        public List<BookPostModel> GetBookByBookId(int BookId)
        {
            sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
            try
            {
                using (sqlConnection)
                {
                    List<BookPostModel> book = new List<BookPostModel>();
                    SqlCommand cmd = new SqlCommand("sp_GetAllBookById", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);
                    sqlConnection.Open();
                    SqlDataReader fetch = cmd.ExecuteReader();
                    if (fetch.HasRows)
                    {
                        while (fetch.Read())
                        {
                            BookPostModel data = new BookPostModel();

                            data.BookName = fetch["BookName"].ToString();
                            data.AuthorName = fetch["AuthorName"].ToString();
                            data.DiscountPrice = Convert.ToInt32(fetch["DiscountPrice"]);
                            data.OriginalPrice = Convert.ToInt32(fetch["OriginalPrice"]);
                            data.BookDescription = fetch["BookDescription"].ToString();
                            data.Rating = Convert.ToInt32(fetch["Rating"]);
                            data.Reviewer = Convert.ToInt32(fetch["Reviewer"]);
                            data.Image = fetch["Image"].ToString();
                            data.BookCount = Convert.ToInt32(fetch["BookCount"]);
                            book.Add(data);
                        }
                        return book;
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

           
        List<BookModel> IBookRL.GetAllBooks()
        {
                sqlConnection = new SqlConnection(this.Configuration.GetConnectionString("BookStore"));
                try
                {
                    using (sqlConnection)
                    {
                        List<BookModel> book = new List<BookModel>();
                        SqlCommand cmd = new SqlCommand("sp_GetAllBooks", sqlConnection);
                        cmd.CommandType = CommandType.StoredProcedure;
                        sqlConnection.Open();
                        SqlDataReader fetch = cmd.ExecuteReader();
                        if (fetch.HasRows)
                        {
                            while (fetch.Read())
                            {
                                BookModel data = new BookModel();
                                data.BookId = Convert.ToInt32(fetch["BookId"]);
                                data.BookName = fetch["BookName"].ToString();
                                data.AuthorName = fetch["AuthorName"].ToString();
                                data.DiscountPrice = Convert.ToInt32(fetch["DiscountPrice"]);
                                data.OriginalPrice = Convert.ToInt32(fetch["OriginalPrice"]);
                                data.BookDescription = fetch["BookDescription"].ToString();
                                data.Rating = Convert.ToInt32(fetch["Rating"]);
                                data.Reviewer = Convert.ToInt32(fetch["Reviewer"]);
                                data.Image = fetch["Image"].ToString();
                                data.BookCount = Convert.ToInt32(fetch["BookCount"]);
                                book.Add(data);
                            }
                            return book;
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

    
    

