using EmsCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Interface
{
    public interface IStateBusiness
    {

        AddStateResponseModel AddState(AddStateRequestModel stateRequest);

        List<StateAddResponseModel> GetAllState();

        StateAddResponseModel GetStateById(int StateId);

        UpdateStateResponseModel UpdateState(int StateId, UpdateStateRequestModel stateRequest);

        bool DeleteState(int StateId);

    }
}
