using EmsCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsRepositoryLayer.Interface
{
    public interface IUserRepository
    {

        ResultConstituencyWiseResponseModel ConstituencyWise(ConstituencyWiseRequestModel constituencyWise);

        ResultPartyWiseResponseModel PartyWise(PartyWiseRequestModel partyWise);

    }
}
