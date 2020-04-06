using EmsBusinessLayer.Interface;
using EmsCommonLayer.Model;
using EmsRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Services
{
    public class PartyBusiness : IPartyBusiness
    {

        private readonly IPartyRepository _partyRepository;

        public PartyBusiness(IPartyRepository partyRepository)
        {
            _partyRepository = partyRepository;
        }

        public CreatePartyResponseModel CreateParty(CreatePartyRequestModel createPartyRequest)
        {
            try
            {
                if (createPartyRequest == null)
                    return null;
                else
                    return _partyRepository.CreateParty(createPartyRequest);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
