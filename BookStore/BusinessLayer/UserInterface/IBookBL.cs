using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.UserInterface
{
    public interface IBookBL
    {
        string AddBooks(BookPostModel addBook);
        bool UpdateBooks(int BookId, BookPostModel updateBook);
        bool DeleteBook(int BookId);
        List<BookModel> GetAllBooks();
        List<BookPostModel> GetBookByBookId(int BookId);

    }
}
