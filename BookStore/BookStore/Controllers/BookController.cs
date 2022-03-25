using BusinessLayer.UserInterface;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        IBookBL bookBL;
        public BookController(IBookBL bookBL)
        {
            this.bookBL = bookBL;

        }
        [Authorize]
        [HttpPost("addbooks")]
        public IActionResult AddBooks(BookPostModel addBooks)
        {
            try
            {
                string result = this.bookBL.AddBooks(addBooks);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"Book Added Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = $"Book not Added" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
        [Authorize]
        [HttpPut("updatebooks/{BookId}")]
        public IActionResult UpdateBooks(int BookId, BookPostModel updateBooks)
        {
            try
            {
                bool result = this.bookBL.UpdateBooks(BookId, updateBooks);
                if (result.Equals(true))
                {
                    return this.Ok(new { success = true, message = $"Book Update Successfully" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = $"Book not updated" });
                }
            }
            catch (Exception e)
            {
                return this.NotFound(new { Status = false, Message = e.Message });
            }
        }
        [HttpDelete("deletebook/{BookId}")]
        public IActionResult DeleteBook(int BookId)
        {
            try
            {
                var result = this.bookBL.DeleteBook(BookId);
                if (result.Equals(true))
                {
                    return this.Ok(new { success = true, message = $"Book deleted Successfully ", response = BookId });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result });
                }

            }
            catch (Exception e)
            {
                throw e;
            }

        }
        [HttpGet("getallbook")]
        public IActionResult GetAllBooks()
        {
            try
            {
                var result = this.bookBL.GetAllBooks();
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"All Books are : ", response = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = $"Books are not there " });
                }

            }
            catch (Exception e)
            {
                throw e;
            }

        }
        [HttpGet("getallbookbyBookId/{BookId}")]
        public IActionResult GetAllBookByBookId(int BookId)
        {
            try
            {
                var result = this.bookBL.GetBookByBookId(BookId);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"Displayed Book by BookId ", response = result });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = $"Book id not exists " });
                }

            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }

}
