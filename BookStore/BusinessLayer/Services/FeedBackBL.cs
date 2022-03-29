using BusinessLayer.UserInterface;
using CommonLayer.User;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class FeedBackBL : IFeedBackBL
    {
      

            IFeedBackRL feedBackRL;

            public FeedBackBL(IFeedBackRL feedBackRL)
            {
                this.feedBackRL = feedBackRL;
            }

            public string AddFeedBack(FeedBackPostModel feedBackPost)
            {
                try
                {
                     return feedBackRL.AddFeedBack(feedBackPost);

                }
                catch (Exception e)
                {
                    throw e;
                }
            }

        public List<FeedBackModel> GetAllFeedBacks(int BookId)
        {
            try
            {
                return feedBackRL.GetAllFeedBacks(BookId);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
