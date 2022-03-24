using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.UserInterface
{
    public interface IUserBl
    {
        void UserRegistration(UserPostModel user);
        string Login(string EmailId, string Password);
        public bool ForgetPassword(string Email);

        public bool ResetPassword(string EmailId, string Password);


    }
}
