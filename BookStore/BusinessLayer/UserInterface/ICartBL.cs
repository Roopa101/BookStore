using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.UserInterface
{
    public interface ICartBL
    {

        string AddBookToCart(CartPostModel cartBook);

        bool UpdateCart(int CartId, int Quantity);

        string DeleteCart(int CartId);

       List<CartModel> GetAllBooksinCart(int userId);
    }
}
