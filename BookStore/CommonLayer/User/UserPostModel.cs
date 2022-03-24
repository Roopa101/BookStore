using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.User
{
    public class UserPostModel
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string PhNo { get; set; }
        public string Password { get; set; }
    }
}
