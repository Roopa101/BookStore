﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.User
{
    public class BookModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int DiscountPrice { get; set; }
        public int OriginalPrice { get; set; }
        public string BookDescription { get; set; }

        public string Image { get; set; }
        public Double Rating { get; set; }
        public int Reviewer { get; set; }
        public int BookCount { get; set; }
    }
}
