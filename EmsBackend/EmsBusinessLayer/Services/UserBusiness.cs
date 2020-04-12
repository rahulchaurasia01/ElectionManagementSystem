using EmsBusinessLayer.Interface;
using EmsCommonLayer.Model;
using EmsRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Services
{
    public class UserBusiness : IUserBusiness
    {

        private readonly IUserRepository _userRepository;

        public UserBusiness(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// It displays the constituency wise election Result
        /// </summary>
        /// <param name="constituencyWise">state Name and Constituency Name</param>
        /// <returns>if fetching is successfully, it return ResultConstituencyWiseResponseModel or else null</returns>
        public ResultConstituencyWiseResponseModel ConstituencyWise(ConstituencyWiseRequestModel constituencyWise)
        {
            try
            {
                if (constituencyWise == null)
                    return null;
                else
                    return _userRepository.ConstituencyWise(constituencyWise);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Displays the Party wise election Result 
        /// </summary>
        /// <param name="partyWise">State Name</param>
        /// <returns>ResultPartyWiseResponseModel</returns>
        public ResultPartyWiseResponseModel PartyWise(PartyWiseRequestModel partyWise)
        {
            try
            {
                if (partyWise == null)
                    return null;
                else
                    return _userRepository.PartyWise(partyWise);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
