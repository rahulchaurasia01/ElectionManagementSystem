﻿using EmsCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Interface
{
    public interface IPartyBusiness
    {

        CreatePartyResponseModel CreateParty(CreatePartyRequestModel createPartyRequest);

    }
}