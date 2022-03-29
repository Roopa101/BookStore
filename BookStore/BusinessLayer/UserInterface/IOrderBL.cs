using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.UserInterface
{
    public interface IOrderBL
    {
        string AddOrder(OrderPostModel orderPost);

        List<OrderModel> OrderBooks(int userId);
        bool DeleteOrder(int OrderId);

    }
}
