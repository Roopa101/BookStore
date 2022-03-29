using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IFeedBackRL
    {
        string AddFeedBack(FeedBackPostModel feedBackPost);

        List<FeedBackModel> GetAllFeedBacks(int BookId);
    }
}
