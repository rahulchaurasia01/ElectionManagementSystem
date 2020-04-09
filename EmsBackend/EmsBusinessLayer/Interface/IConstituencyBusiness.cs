using EmsCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Interface
{
    public interface IConstituencyBusiness
    {

        AddConstituencyResponseModel AddConstituency(AddConstituencyRequestModel addConstituency);

        List<ConstituencyAddResponseModel>  GetAllConstituency();

        ConstituencyAddResponseModel GetConstituencyById(int ConstituencyId);

        UpdateConstituencyResponseModel UpdateConstituency(int ConstituencyId, UpdateConstituencyRequestModel updateConstituency);

        bool DeleteConstituency(int ConstituencyId);

    }
}
