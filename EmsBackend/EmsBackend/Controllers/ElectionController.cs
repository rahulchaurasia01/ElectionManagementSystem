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
    public class ElectionController : ControllerBase
    {

        private readonly IElectionBusiness _electionBusiness;

        public ElectionController(IElectionBusiness electionBusiness)
        {
            _electionBusiness = electionBusiness;
        }
        
        [HttpPost]
        [Route("PartyWise")]
        /// <summary>
        /// It Determine the no. of seats won by the party in a particular region.
        /// </summary>
        /// <param name="partyRequest">Particular state name</param>
        /// <returns></returns>
        public IActionResult PartyWise(PartyWiseRequestModel partyRequest)
        {
            try
            {
                bool status = false;
                string message;

                List<PartyResponseModel> data = _electionBusiness.PartyWise(partyRequest);

                if(data != null && data.Count > 0)
                {
                    status = true;
                    message = "No. of seats won by the party";
                    return Ok(new { status, message, data });
                }
                message = "Unable to determine the no. of seats won by the party";
                return Ok(new { status, message });

            }
            catch(Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

    }
}