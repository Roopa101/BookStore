using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IWishListRL
    {
        string AddBookinWishList(WishListPostModel wishListPost);

        bool RemoveBookinWishList(int WishListId);

        List<WishListModel> GetAllBooksinWishList(int userId);
    }
}
