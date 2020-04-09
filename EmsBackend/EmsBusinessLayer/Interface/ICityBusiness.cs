using EmsCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Interface
{
    public interface ICityBusiness
    {

        AddCityResponseModel AddCity(AddCityRequestModel addCity);

        List<CityAddResponseModel> GetAllCity();

        CityAddResponseModel GetCityById(int CityId);

        UpdateCityResponseModel UpdateCity(int CityId, UpdateCityRequestModel updateCity);

        bool DeleteCity(int CityId);

    }
}
