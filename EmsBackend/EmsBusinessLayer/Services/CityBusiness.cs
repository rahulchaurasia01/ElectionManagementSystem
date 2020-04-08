using EmsBusinessLayer.Interface;
using EmsCommonLayer.Model;
using EmsRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Services
{
    public class CityBusiness : ICityBusiness
    {

        private readonly ICityRepository _cityRepository;

        public CityBusiness(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        /// <summary>
        /// It add City to the state in db
        /// </summary>
        /// <param name="addCity">City Name and state Id</param>
        /// <returns>Add City Response Model</returns>
        public AddCityResponseModel AddCity(AddCityRequestModel addCity)
        {
            try
            {
                if (addCity == null)
                    return null;
                else
                    return _cityRepository.AddCity(addCity);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        
    }
}
