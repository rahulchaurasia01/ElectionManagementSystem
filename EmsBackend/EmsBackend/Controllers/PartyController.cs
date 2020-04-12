using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmsBusinessLayer.Interface;
using EmsCommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PartyController : ControllerBase
    {

        private readonly IPartyBusiness _partyBusiness;

        public PartyController(IPartyBusiness partyBusiness)
        {
            _partyBusiness = partyBusiness;            
        }

        /// <summary>
        /// It Create a new Party
        /// </summary>
        /// <param name="createPartyRequest">Party Name</param>
        /// <returns>if Party Created Successfully, It Return Ok with status true and Data 
        /// else with status false</returns>
        [HttpPost]
        public IActionResult CreateParty(CreatePartyRequestModel createPartyRequest)
        {
            try
            {
                var admin = HttpContext.User;
                bool status = false;
                string message;

                if(admin.HasClaim(c => c.Type == "TokenType"))
                {
                    if (admin.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {
                        CreatePartyResponseModel createParty = _partyBusiness.CreateParty(createPartyRequest);

                        if (createParty != null)
                        {
                            if (createParty.ErrorResponse.ErrorStatus)
                            {
                                message = createParty.ErrorResponse.Message;
                                return Ok(new { status, message });
                            }
                            status = true;
                            message = "Party Created Successfully";
                            PartyCreatedResponseModel data = createParty.PartyCreated;
                            return Ok(new { status, message, data });
                        }
                        message = "Unable to Create Party";
                        return Ok(new { status, message });
                    }
                }

                message = "Invalid Token";
                return BadRequest(new { status, message });
            }
            catch(Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It Get All the Party
        /// </summary>
        /// <returns>If all the party is retrieve, it return Ok with status true and Data
        /// else with status false</returns>
        [HttpGet]
        public IActionResult GetAllParty()
        {
            try
            {
                var admin = HttpContext.User;
                bool status = false;
                string message;

                if (admin.HasClaim(c => c.Type == "TokenType"))
                {
                    if (admin.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {

                        List<PartyCreatedResponseModel> data = _partyBusiness.GetAllParty();

                        if (data != null)
                        {
                            if (data.Count == 0)
                            {
                                message = "No Party Data is Present";
                                return Ok(new { status, message });
                            }
                            else
                            {
                                status = true;
                                message = "Here is the list of all Party";
                                return Ok(new { status, message, data });
                            }
                        }
                        message = "Unable to get all the party";
                        return Ok(new { status, message });
                    }
                }
                message = "Invalid Token";
                return BadRequest(new { status, message });
            }
            catch(Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// Get Party Details By it's Id
        /// </summary>
        /// <param name="PartyId">Party Id</param>
        /// <returns>If the party data is retrive, It return Ok with status true and data
        /// else with status false</returns>
        [HttpGet]
        [Route("{PartyId}")]
        public IActionResult GetPartyId(int PartyId)
        {
            try
            {
                var admin = HttpContext.User;
                bool status = false;
                string message;

                if (admin.HasClaim(c => c.Type == "TokenType"))
                {
                    if (admin.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {

                        PartyCreatedResponseModel data = _partyBusiness.GetPartyById(PartyId);

                        if (data != null)
                        {
                            status = true;
                            message = "Here is the Party Details";
                            return Ok(new { status, message, data });
                        }
                        message = "Unable to get the Party Details";
                        return Ok(new { status, message });

                    }
                }
                message = "Invalid Token";
                return BadRequest(new { status, message });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It Update the Party Name
        /// </summary>
        /// <param name="PartyId">PartyId</param>
        /// <param name="updateParty">Update Party Name</param>
        /// <returns>If the Party name is successfully Updated, It Return Ok With status true and Data
        /// else with status false</returns>
        [HttpPut]
        [Route("{PartyId}")]
        public IActionResult UpdateParty(int PartyId, UpdatePartyRequestModel updateParty)
        {
            try
            {
                var admin = HttpContext.User;
                bool status = false;
                string message;

                if (admin.HasClaim(c => c.Type == "TokenType"))
                {
                    if (admin.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {

                        UpdatepartyResponseModel updatePartyModel = _partyBusiness.UpdateParty(PartyId, updateParty);

                        if (updatePartyModel != null)
                        {
                            if (updatePartyModel.ErrorResponse.ErrorStatus)
                            {
                                message = updatePartyModel.ErrorResponse.Message;
                                return Ok(new { status, message });
                            }
                            status = true;
                            message = "Party Name has Been Updated";
                            PartyUpdatedResponseModel data = updatePartyModel.PartyUpdated;
                            return Ok(new { status, message, data });

                        }
                        message = "Unable to update the Party Name";
                        return Ok(new { status, message });
                    }
                }
                message = "Invalid Token";
                return BadRequest(new { status, message });
            }
            catch(Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It Delete the Party
        /// </summary>
        /// <param name="PartyId">Party Id</param>
        /// <returns>If the Party is deleted Successfully, It return Ok with status True or else false</returns>
        [HttpDelete]
        [Route("{PartyId}")]
        public IActionResult DeleteParty(int PartyId)
        {
            try
            {
                var admin = HttpContext.User;
                bool status = false;
                string message;

                if (admin.HasClaim(c => c.Type == "TokenType"))
                {
                    if (admin.Claims.FirstOrDefault(c => c.Type == "TokenType").Value == "Login")
                    {

                        status = _partyBusiness.DeleteParty(PartyId);

                        if (status)
                        {
                            message = "Party Deleted Successfully";
                            return Ok(new { status, message });
                        }

                        message = "Unable to Delete the Party";
                        return Ok(new { status, message });
                    }
                }
                message = "Invalid Token";
                return BadRequest(new { status, message });
            }
            catch(Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

    }
}