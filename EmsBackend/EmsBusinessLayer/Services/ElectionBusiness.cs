using EmsBusinessLayer.Interface;
using EmsCommonLayer.Model;
using EmsRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Services
{
    public class ElectionBusiness : IElectionBusiness
    {

        private readonly IElectionRepository _electionRepository;

        public ElectionBusiness(IElectionRepository electionRepository)
        {
            _electionRepository = electionRepository;
        }

        /// <summary>
        /// It determine the election by party wise
        /// </summary>
        /// <param name="partyRequest">State</param>
        /// <returns>List of Party Response Model</returns>
        public List<PartyResponseModel> PartyWise(PartyWiseRequestModel partyRequest)
        {
            try
            {
                if (partyRequest == null)
                    return null;
                else
                    return _electionRepository.PartyWise(partyRequest);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
