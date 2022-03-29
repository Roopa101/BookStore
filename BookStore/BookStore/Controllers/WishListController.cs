using BusinessLayer.UserInterface;
using CommonLayer.User;
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
    public class WishListController : ControllerBase
    {
        IWishListBL wishListBL;
        public WishListController(IWishListBL wishListBL)
        {
            this.wishListBL = wishListBL;
        }

        [HttpPost("addBooksInWishList")]
        public IActionResult AddBookinWishList(WishListPostModel wishListPost)
        {
            try
            {

                var result = this.wishListBL.AddBookinWishList(wishListPost);
                if (result.Equals("book is added in WishList successfully"))
                {
                    return this.Ok(new { success = true, message = $"Book is added in WishList  Successfully " });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result, message = "Unable to add book in wishlist" });
                }



            }
            catch (Exception e)
            {
                throw e;
            }

        }


        [HttpDelete("removeBookinWishList/{WishListId}")]
        public IActionResult RemoveBookinWishList(int WishListId)
        {
            try
            {

                var result = this.wishListBL.RemoveBookinWishList(WishListId);
                if (result.Equals(true))
                {
                    return this.Ok(new { success = true, message = $"Book is removed from the WishList " });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = result, message = "Unable to delete book in wishlist" });
                }


            }
            catch (Exception e)
            {
                throw e;
            }

        }


       

    [HttpGet("getallBookinWishList/{userId}")]
    public IActionResult GetAllBooksinWishList(int userId)
    {
              
         var result = this.wishListBL.GetAllBooksinWishList(userId);
         if (result != null)
         {
            return this.Ok(new { success = true, message = $"All Books Displayed in the WishList Successfully ", response = result });
                        }
                        else
                        {
                            return this.BadRequest(new { Status = false, Message = $"Books are not there in WishList " });
                        }
         }
   
    }

}



    