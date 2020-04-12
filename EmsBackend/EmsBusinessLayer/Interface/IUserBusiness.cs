using EmsCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Interface
{
    public interface IUserBusiness
    {

        ResultConstituencyWiseResponseModel ConstituencyWise(ConstituencyWiseRequestModel constituencyWise);

        ResultPartyWiseResponseModel PartyWise(PartyWiseRequestModel partyWise);

    }
}
