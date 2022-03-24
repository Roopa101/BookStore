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
    public class UserController : ControllerBase
    {
        IUserBl userBL;
        public UserController(IUserBl userBL )
        {
            this.userBL = userBL;
        }
        [HttpPost("UserRegistration")]
        public ActionResult UserRegistration(UserPostModel userPostModel)
        {
            try
            {
                this.userBL.UserRegistration(userPostModel);
                return this.Ok(new { success = true, message = $"Registration Successfull  {userPostModel.EmailId} " });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        [HttpPost("Login")]
        public ActionResult Login(string EmailId, string Password)
        {
            try
            {
                var login = this.userBL.Login(EmailId, Password);
                if (login != null)
                {
                    return this.Ok(new { Success = true, message = "Login Successful", token = login });
                }
                else
                {
                    return this.Ok(new { Success = false, message = "Invalid User Please enter valid email and password." });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });

            }
        }
        [HttpPost("ForgotPassword")]
        public IActionResult ForgotPassword(string EmailId)
        {
            if (string.IsNullOrEmpty(EmailId))
            {
                return BadRequest("Email should not be null or empty");
            }
            try
            {
                var result = this.userBL.ForgetPassword(EmailId);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Token generated.Please check your email", token = result });
                }
                else
                {
                    return this.Ok(new { Success = false, message = "Invalid User Please enter valid email and password." });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });

            }
        }
        [Authorize]
        [HttpPut("ResetPassword")]

        public ActionResult ResetPassword(string Password)
        {
            try
            {
                var userEmail = User.FindFirst("EmailId").Value.ToString();
                if (userEmail != null)
                {
                    this.userBL.ResetPassword(userEmail, Password);

                    return Ok(new { Success = true, message = "Password reset successfully" });
                }
                else
                {
                    return BadRequest(new { Success = false, message = "Password reset Unsuccesfully" });
                }
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { success = false, message = ex.Message });

            }
        }



    }
}
