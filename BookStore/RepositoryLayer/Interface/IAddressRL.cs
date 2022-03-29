using CommonLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAddressRL
    {
        string AddAddress(int userId, AddressPostModel addressPost);

        bool UpdateAddress(int AddressId, AddressPostModel addressPost);

        bool DeleteAddress(int AddressId);

        List<AddressModel> GetAllAddress();

        List<AddressModel> GetAddressByAddressId(int userId);
    }
}
