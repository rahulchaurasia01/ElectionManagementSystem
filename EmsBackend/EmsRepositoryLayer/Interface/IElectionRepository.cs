using EmsCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsRepositoryLayer.Interface
{
    public interface IElectionRepository
    {

        List<PartyResponseModel> PartyWise(PartyWiseRequestModel partyRequest);

    }
}
