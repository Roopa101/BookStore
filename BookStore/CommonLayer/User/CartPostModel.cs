using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.User
{
    public class CartPostModel
    {
        public int Quantity { get; set; }
        public int userId { get; set; }
        public int BookId { get; set; }
    }
}
