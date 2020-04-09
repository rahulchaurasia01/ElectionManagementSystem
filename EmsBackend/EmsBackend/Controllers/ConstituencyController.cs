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
    public class ConstituencyController : ControllerBase
    {

        private readonly IConstituencyBusiness _constituencyBusiness;

        public ConstituencyController(IConstituencyBusiness constituencyBusiness)
        {
            _constituencyBusiness = constituencyBusiness;
        }

        /// <summary>
        /// It Add a Constituency to the respective City
        /// </summary>
        /// <param name="addConstituency">Constituency Name and the CityId</param>
        /// <returns>if Constituency is added succesfully then it return ok with status true and data or else status false</returns>
        [HttpPost]
        public IActionResult AddConstituency(AddConstituencyRequestModel addConstituency)
        {
            try
            {
                bool status = false;
                string message;

                AddConstituencyResponseModel constituencyResponse = _constituencyBusiness.AddConstituency(addConstituency);

                if (constituencyResponse != null)
                {
                    if (constituencyResponse.ErrorResponse.ErrorStatus)
                    {
                        message = constituencyResponse.ErrorResponse.Message;
                        return Ok(new { status, message });
                    }
                    else
                    {
                        status = true;
                        message = "Constituency Has Been Successfully Added to City.";
                        ConstituencyAddResponseModel data = constituencyResponse.ConstituencyAdd;
                        return Ok(new { status, message, data });
                    }
                }

                message = "Unable to Add Constituency.";
                return Ok(new { status, message });

            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It Fetch all the Constituency with their StateId
        /// </summary>
        /// <returns>If Constituency is fetched successfully then it return Ok with status true and data or else status false</returns>
        [HttpGet]
        public IActionResult GetAllConstituency()
        {
            try
            {
                bool status = false;
                string message;

                List<ConstituencyAddResponseModel> data = _constituencyBusiness.GetAllConstituency();

                if (data != null)
                {
                    if (data.Count > 0)
                    {
                        status = true;
                        message = "Here is the list of all Constituencies";
                        return Ok(new { status, message, data });
                    }
                    else
                    {
                        message = "No Constituencies Present";
                        return Ok(new { status, message });
                    }
                }

                message = "Unable to Fetch the Constituencies";
                return Ok(new { status, message });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It Fetch the specified Constituency details
        /// </summary>
        /// <param name="ConstituencyId">Constituency Id</param>
        /// <returns>If the Fetching is successfull then it return ok with status true and data or else false</returns>
        [HttpGet]
        [Route("{ConstituencyId}")]
        public IActionResult GetConstituencyById(int ConstituencyId)
        {
            try
            {
                bool status = false;
                string message;

                ConstituencyAddResponseModel data = _constituencyBusiness.GetConstituencyById(ConstituencyId);
                if (data != null)
                {
                    status = true;
                    message = "Constituency Details has been Successfully fetched";
                    return Ok(new { status, message, data });
                }

                message = "Unable to get the specificed Constituency Details";
                return Ok(new { status, message });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It update the Constituency details
        /// </summary>
        /// <param name="ConstituencyId">Constituency Id</param>
        /// <param name="updateConstituency">Update Constituency Data</param>
        /// <returns>If Updation is successfull, it will return ok with status true and data or else false</returns>
        [HttpPut]
        [Route("{ConstituencyId}")]
        public IActionResult UpdateConstituency(int ConstituencyId, UpdateConstituencyRequestModel updateConstituency)
        {
            try
            {
                bool status = false;
                string message;

                UpdateConstituencyResponseModel constituencyResponse = _constituencyBusiness.UpdateConstituency(ConstituencyId, updateConstituency);

                if (constituencyResponse != null)
                {
                    if (constituencyResponse.ErrorResponse.ErrorStatus)
                    {
                        message = constituencyResponse.ErrorResponse.Message;
                        return Ok(new { status, message });
                    }
                    else
                    {
                        status = true;
                        message = "Constituency Has Been Successfully Updated.";
                        ConstituencyUpdateResponseModel data = constituencyResponse.ConstituencyUpdate;
                        return Ok(new { status, message, data });
                    }
                }

                message = "Unable to Update the Constituency Details";
                return Ok(new { status, message });

            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It Delete the Constituency.
        /// </summary>
        /// <param name="ConstituencyId">Constituency Id</param>
        /// <returns>If the Constituency is deleted Successfully, It return Ok with status True or else false</returns>
        [HttpDelete]
        [Route("{ConstituencyId}")]
        public IActionResult DeleteConstituency(int ConstituencyId)
        {
            try
            {
                bool status = false;
                string message;

                status = _constituencyBusiness.DeleteConstituency(ConstituencyId);

                if (status)
                {
                    message = "Constituency has been Deleted Successfully";
                    return Ok(new { status, message });
                }
                message = "Unable to Delete the Constituency";
                return Ok(new { status, message });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

    }
}