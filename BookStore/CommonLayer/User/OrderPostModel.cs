using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.User
{
    public class OrderPostModel
    {
        public int userId { get; set; }
        public int AddressId { get; set; }
        public int BookId { get; set; }
        public int TotalPrice { get; set; }
        public int BookQuantity { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
