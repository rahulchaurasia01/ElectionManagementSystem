using EmsCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsRepositoryLayer.Interface
{
    public interface ICityRepository
    {

        AddCityResponseModel AddCity(AddCityRequestModel addCity);

    }
}
