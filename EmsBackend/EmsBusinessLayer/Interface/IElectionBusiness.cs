using EmsCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Interface
{
    public interface IElectionBusiness
    {

        List<PartyResponseModel> PartyWise(PartyWiseRequestModel partyRequest);

    }
}
