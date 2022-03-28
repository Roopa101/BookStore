using BusinessLayer.UserInterface;
using CommonLayer.User;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBL : IWishListBL
    {
        IWishListRL wishListRL;

        public WishListBL(IWishListRL wishListRL)
        {
            this.wishListRL = wishListRL;
        }
        public string AddBookinWishList(WishListPostModel wishListPost)
        {
            try
            {
                return wishListRL.AddBookinWishList(wishListPost);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        

        public bool RemoveBookinWishList(int WishListId)
        {
            try
            {
                return wishListRL.RemoveBookinWishList(WishListId);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<WishListModel> GetAllBooksinWishList(int userId)
        {
            try
            {
                return wishListRL.GetAllBooksinWishList(userId);

            }
            catch( Exception e)
            {
                throw e;
            }
        }
    }
}
