using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmsBusinessLayer.Interface;
using EmsCommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {

        private readonly ICityBusiness _cityBusiness;

        public CityController(ICityBusiness cityBusiness)
        {
            _cityBusiness = cityBusiness;
        }
        
        /// <summary>
        /// It Add a city to the respective State
        /// </summary>
        /// <param name="addCity">City Name and the stateId</param>
        /// <returns>if city is added succesfully then it return ok with status true and data or else status false</returns>
        [HttpPost]
        public IActionResult AddCity(AddCityRequestModel addCity)
        {
            try
            {
                bool status = false;
                string message;

                AddCityResponseModel cityResponse = _cityBusiness.AddCity(addCity);
                
                if(cityResponse != null)
                {
                    if(cityResponse.ErrorResponse.ErrorStatus)
                    {
                        message = cityResponse.ErrorResponse.Message;
                        return Ok(new { status, message });
                    }
                    else
                    {
                        status = true;
                        message = "City Has Been Successfully Added to State.";
                        CityAddResponseModel data = cityResponse.CityAdd;
                        return Ok(new { status, message, data });
                    }
                }

                message = "Unable to Add City.";
                return Ok(new { status, message });

            }
            catch(Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It Fetch all the City With there StateId
        /// </summary>
        /// <returns>If City is fetched successfully then it return Ok with status true and data or else status false</returns>
        [HttpGet]
        public IActionResult GetAllCity()
        {
            try
            {
                bool status = false;
                string message;

                List<CityAddResponseModel> data = _cityBusiness.GetAllCity();

                if(data != null)
                {
                    if(data.Count > 0)
                    {
                        status = true;
                        message = "Here is the list of all cities";
                        return Ok(new { status, message, data });
                    }
                    else
                    {
                        message = "No Cities Present";
                        return Ok(new { status, message });
                    }
                }

                message = "Unable to Fetch the City";
                return Ok(new { status, message });
            }
            catch(Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It Fetch the specified City details
        /// </summary>
        /// <param name="CityId">City Id</param>
        /// <returns>If the Fetching is successfull then it return ok with status true and data or else false</returns>
        [HttpGet]
        [Route("{CityId}")]
        public IActionResult GetCityById(int CityId)
        {
            try
            {
                bool status = false;
                string message;

                CityAddResponseModel data = _cityBusiness.GetCityById(CityId);
                if(data != null)
                {
                    status = true;
                    message = "City Details has been Successfully fetched";
                    return Ok(new { status, message, data });
                }

                message = "Unable to get the specificed City Details";
                return Ok(new { status, message });
            }
            catch(Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It update the City details
        /// </summary>
        /// <param name="CityId">City Id</param>
        /// <param name="updateCity">Update Data</param>
        /// <returns>If Updation is successfull, it will return ok with status true and data or else false</returns>
        [HttpPut]
        [Route("{CityId}")]
        public IActionResult UpdateCity(int CityId, UpdateCityRequestModel updateCity)
        {
            try
            {
                bool status = false;
                string message;

                UpdateCityResponseModel cityResponse = _cityBusiness.UpdateCity(CityId, updateCity);

                if (cityResponse != null)
                {
                    if (cityResponse.ErrorResponse.ErrorStatus)
                    {
                        message = cityResponse.ErrorResponse.Message;
                        return Ok(new { status, message });
                    }
                    else
                    {
                        status = true;
                        message = "City Has Been Successfully Updated.";
                        CityUpdateResponseModel data = cityResponse.CityUpdate;
                        return Ok(new { status, message, data });
                    }
                }

                message = "Unable to Update the City Details";
                return Ok(new { status, message });

            }
            catch(Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It Delete the City.
        /// </summary>
        /// <param name="CityId">City Id</param>
        /// <returns>If the City is deleted Successfully, It return Ok with status True or else false</returns>
        [HttpDelete]
        [Route("{CityId}")]
        public IActionResult DeleteCity(int CityId)
        {
            try
            {
                bool status = false;
                string message;

                status = _cityBusiness.DeleteCity(CityId);

                if (status)
                {
                    message = "City has been Deleted Successfully";
                    return Ok(new { status, message });
                }
                message = "Unable to Delete the City";
                return Ok(new { status, message });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

    }
}