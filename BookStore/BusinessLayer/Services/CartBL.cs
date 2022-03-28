using BusinessLayer.UserInterface;
using CommonLayer.User;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        ICartRL cartRL;

        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }
        public string AddBookToCart(CartPostModel cartBook)
        {
            try
            {
                return cartRL.AddBookToCart(cartBook);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

       
        public bool UpdateCart(int CartId, int Quantity)
        {
            try
            {
                return cartRL.UpdateCart(CartId, Quantity);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public string DeleteCart(int CartId)
        {
            try
            {
                return cartRL.DeleteCart(CartId);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<CartModel> GetAllBooksinCart(int userId)
        {
            try
            {
                return cartRL.GetAllBooksinCart(userId);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
