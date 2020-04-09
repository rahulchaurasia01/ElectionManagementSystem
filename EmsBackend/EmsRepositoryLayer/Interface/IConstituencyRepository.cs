using EmsCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsRepositoryLayer.Interface
{
    public interface IConstituencyRepository
    {

        AddConstituencyResponseModel AddConstituency(AddConstituencyRequestModel addConstituency);

        List<ConstituencyAddResponseModel> GetAllConstituency();

        ConstituencyAddResponseModel GetConstituencyById(int ConstituencyId);

        UpdateConstituencyResponseModel UpdateConstituency(int ConstituencyId, UpdateConstituencyRequestModel updateConstituency);

        bool DeleteConstituency(int ConstituencyId);

    }
}
