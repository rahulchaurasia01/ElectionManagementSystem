using EmsCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Interface
{
    public interface IPartyBusiness
    {

        CreatePartyResponseModel CreateParty(CreatePartyRequestModel createPartyRequest);

        List<PartyCreatedResponseModel> GetAllParty();

        PartyCreatedResponseModel GetPartyById(int PartyId);

        UpdatepartyResponseModel UpdateParty(int PartyId, UpdatePartyRequestModel updateParty);

        bool DeleteParty(int PartyId);

    }
}
