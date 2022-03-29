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
    public class FeedBackController : ControllerBase
    {
        IFeedBackBL feedBackBL;
        public FeedBackController(IFeedBackBL feedBackBL)
        {
            this.feedBackBL = feedBackBL;

        }
        [Authorize]
        [HttpPost("feedBack")]
        public IActionResult AddFeedBack(FeedBackPostModel feedBackPost)
        {
            try
            {
               
                    var result = this.feedBackBL.AddFeedBack(feedBackPost);
                    if (result.Equals("FeedBack added Successfully"))
                    {
                        return this.Ok(new { success = true, message = $"FeedBack added Successfully" });
                    }
                    else
                    {
                        return this.BadRequest(new { Status = false, Message = result , message = "Unable to add Feedback!!!" });
                    }
               

            }
            catch (Exception e)
            {
                throw e;
            }

        }
        [Authorize]
        [HttpGet("getallFeedBacks/{BookId}")]
        public IActionResult GetAllFeedBacks(int BookId)
        {
            try
            {
               
                    var result = this.feedBackBL.GetAllFeedBacks(BookId);
                    if (result != null)
                    {
                        return this.Ok(new { success = true, message = $"All FeedBacks from User Displayed Successfully ", response = result });
                    }
                    else
                    {
                        return this.BadRequest(new { Status = false, Message = $"FeedBack for this Book is not exist" });
                    }
             

            }
            catch (Exception e)
            {
                throw e;
            }

        }

    }
}
    