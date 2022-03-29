using BusinessLayer.UserInterface;
using CommonLayer.User;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class AddressBL : IAddressBL
    {
        IAddressRL addressRL;

        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }
        public string AddAddress(int userId, AddressPostModel addressPost)
        {
            try
            {
                return addressRL.AddAddress(userId, addressPost);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool UpdateAddress(int AddressId, AddressPostModel addressPost)
        {
            try
            {
                return addressRL.UpdateAddress(AddressId, addressPost);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public bool DeleteAddress(int AddressId)
        {

            try
            {
                return addressRL.DeleteAddress(AddressId);

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<AddressModel> GetAllAddress()
        {
            try
            {
                return addressRL.GetAllAddress();

            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public List<AddressModel> GetAddressByAddressId(int userId)
        {
            try
            {
                return addressRL.GetAddressByAddressId(userId);

            }
            catch (Exception e)
            {
                throw e;
            }
        }

       
       
    }
}
