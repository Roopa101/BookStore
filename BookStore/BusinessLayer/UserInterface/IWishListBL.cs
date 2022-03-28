using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.UserInterface
{
    public interface IWishListBL
    {

        string AddBookinWishList(WishListPostModel wishListPost);

        bool RemoveBookinWishList(int WishListId);

        List<WishListModel> GetAllBooksinWishList(int userId);
    }
}
