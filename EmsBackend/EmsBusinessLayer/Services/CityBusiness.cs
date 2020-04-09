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

        /// <summary>
        /// it fetchs all the City
        /// </summary>
        /// <returns>List of all City</returns>
        public List<CityAddResponseModel> GetAllCity()
        {
            try
            {
                return _cityRepository.GetAllCity();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It fetch the single specified City details
        /// </summary>
        /// <param name="CityId">City Id</param>
        /// <returns>If Successfully fetch, It return cityAddResponseModel or else null</returns>
        public CityAddResponseModel GetCityById(int CityId)
        {
            try
            {
                if (CityId <= 0)
                    return null;
                else
                    return _cityRepository.GetCityById(CityId);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It update the city
        /// </summary>
        /// <param name="CityId">City Id</param>
        /// <param name="updateCity">Update city Name or StateId</param>
        /// <returns>UpdateCityResponseModel</returns>
        public UpdateCityResponseModel UpdateCity(int CityId, UpdateCityRequestModel updateCity)
        {
            try
            {
                if (CityId <= 0 || updateCity == null)
                    return null;
                else
                    return _cityRepository.UpdateCity(CityId, updateCity);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Delete the City
        /// </summary>
        /// <param name="CityId">CityId</param>
        /// <returns>If Delete Successfully, It return true or else false</returns>
        public bool DeleteCity(int CityId)
        {
            try
            {
                if (CityId <= 0)
                    return false;
                else
                    return _cityRepository.DeleteCity(CityId);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
