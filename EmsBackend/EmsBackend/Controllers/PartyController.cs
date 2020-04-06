﻿using System;
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
    public class PartyController : ControllerBase
    {

        private readonly IPartyBusiness _partyBusiness;

        public PartyController(IPartyBusiness partyBusiness)
        {
            _partyBusiness = partyBusiness;            
        }

        [HttpPost]
        [Route("CreateParty")]
        public IActionResult CreateParty(CreatePartyRequestModel createPartyRequest)
        {
            try
            {
                bool status = false;
                string message = "";

                CreatePartyResponseModel data = _partyBusiness.CreateParty(createPartyRequest);

                if(data != null)
                {
                    status = true;
                    message = "Party Created Successfully";
                    return Ok(new { status, message, data });
                }

                return Ok(new { status, message });

            }
            catch(Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

    }
}