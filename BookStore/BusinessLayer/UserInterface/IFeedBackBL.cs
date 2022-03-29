using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.UserInterface
{
    public interface IFeedBackBL
    {
        string AddFeedBack(FeedBackPostModel feedBackPost);

        List<FeedBackModel> GetAllFeedBacks(int BookId);
    }
}
