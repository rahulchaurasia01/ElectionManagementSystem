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


    }
}