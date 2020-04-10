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
    public class VoteController : ControllerBase
    {

        private readonly IVoteBusiness _voteBusiness;

        public VoteController(IVoteBusiness voteBusiness)
        {
            _voteBusiness = voteBusiness;
        }

        /// <summary>
        /// It Add a vote to the candidate
        /// </summary>
        /// <param name="addVote">Candidate Id and EvmVote or PostalVote</param>
        /// <returns>If Vote is successfully added. It Return Ok with status true and data or else false</returns>
        [HttpPost]
        public IActionResult AddVote(AddVoteRequestModel addVote)
        {
            try
            {
                bool status = false;
                string message;

                AddVoteResponseModel VoteResponse = _voteBusiness.AddVote(addVote);

                if(VoteResponse != null)
                {
                    if (VoteResponse.ErrorResponse.ErrorStatus)
                    {
                        message = VoteResponse.ErrorResponse.Message;
                        return Ok(new { status, message });
                    }
                    else
                    {
                        status = true;
                        message = "Your Votes has Been Successfully Added.";
                        VoteAddResponseModel data = VoteResponse.VoteAdd;
                        return Ok(new { status, message, data });
                    }
                }

                message = "Unable to add your Vote";
                return Ok(new { status, message });
            }
            catch(Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It Delete the Vote
        /// </summary>
        /// <param name="VotesId">Vote Id</param>
        /// <returns>If Deleted Successfully, it Return ok with status true or else false</returns>
        [HttpDelete]
        [Route("{VotesId}")]
        public IActionResult DeleteVote(int VotesId)
        {
            try
            {
                bool status = false;
                string message;

                status = _voteBusiness.DeleteVote(VotesId);

                if(status)
                {
                    message = "This Vote has been successfully deleted.";
                    return Ok(new { status, message });
                }

                message = "Unable to Delete the votes.";
                return Ok(new { status, message });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }

        }

    }
}