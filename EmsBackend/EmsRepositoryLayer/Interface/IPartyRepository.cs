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

        PartyCreatedResponseModel GetPartyById(int PartyId);

        UpdatepartyResponseModel UpdateParty(int PartyId, UpdatePartyRequestModel updateParty);

        bool DeleteParty(int PartyId);

    }
}
