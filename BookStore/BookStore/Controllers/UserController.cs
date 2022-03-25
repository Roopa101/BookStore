using BusinessLayer.UserInterface;
using CommonLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBl userBL;
        public UserController(IUserBl userBL)
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
            try
            {
                var result = this.userBL.ForgetPassword(EmailId);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, message = "Email is invalid" });
                }
                else
                {

                    return this.Ok(new { success = true, message = "Token sent succesfully to email for password reset" });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    
        [AllowAnonymous]
        [HttpPut("ResetPassword")]

        public ActionResult ResetPassword(string Password)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims;
                    var UserEmailObject = claims.FirstOrDefault()?.Value;
                    if (UserEmailObject != null)
                    {
                        this.userBL.ResetPassword(UserEmailObject, Password);
                        return Ok(new { success = true, message = "Password Changed Sucessfully" });
                    }
                    else
                    {
                        return this.BadRequest(new { success = false, message = $"Email is not Authorized" });
                    }
                }
                return this.BadRequest(new { success = false, message = $"Password not changed Successfully" });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}

