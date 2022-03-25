using BusinessLayer.UserInterface;
using CommonLayer.User;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookBL : IBookBL
    {
        IBookRL bookRL;

        public BookBL(IBookRL bookRL)
        {
            this.bookRL = bookRL;
        }
        public string AddBooks(BookPostModel addBook)
        {
            try
            {
                return bookRL.AddBooks(addBook);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateBooks(int BookId, BookPostModel updateBook)
        {
            try
            {
                return bookRL.UpdateBooks(BookId, updateBook);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool DeleteBook(int BookId)
        {
            try
            {
                return bookRL.DeleteBook(BookId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
     

        public List<BookPostModel> GetBookByBookId(int BookId)
        {
            try
            {
                return bookRL.GetBookByBookId(BookId);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

       

        List<BookModel> IBookBL.GetAllBooks()
        {
            try
            {
                return bookRL.GetAllBooks();

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
