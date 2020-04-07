using EmsCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsRepositoryLayer.Interface
{
    public interface IPartyRepository
    {

        CreatePartyResponseModel CreateParty(CreatePartyRequestModel createPartyRequest);

        List<PartyCreatedResponseModel> GetAllParty();

        UpdatepartyResponseModel UpdateParty(int PartyId, UpdatePartyRequestModel updateParty);

        bool DeleteParty(int PartyId);

    }
}
