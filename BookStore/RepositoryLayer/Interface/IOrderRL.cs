using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IOrderRL
    {
        string AddOrder(OrderPostModel orderPost);

        List<OrderModel> OrderBooks(int userId);
        bool DeleteOrder(int OrderId);

    }
}
