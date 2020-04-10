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
    public class CandidateController : ControllerBase
    {

        private readonly ICandidateBusiness _candidateBusiness;

        public CandidateController(ICandidateBusiness candidateBusiness)
        {
            _candidateBusiness = candidateBusiness;
        }

        /// <summary>
        /// It Add a Candidate
        /// </summary>
        /// <param name="addCandidate">Candidate Name and constituencyId and PartyId</param>
        /// <returns>if Candidate is added succesfully then it return ok with status true and data or else status false</returns>
        [HttpPost]
        public IActionResult AddCandidate(AddCandidateRequestModel addCandidate)
        {
            try
            {
                bool status = false;
                string message;

                AddCandidateResponseModel candidateResponse = _candidateBusiness.AddCandidate(addCandidate);

                if (candidateResponse != null)
                {
                    if (candidateResponse.ErrorResponse.ErrorStatus)
                    {
                        message = candidateResponse.ErrorResponse.Message;
                        return Ok(new { status, message });
                    }
                    else
                    {
                        status = true;
                        message = "Candidate Has Been Successfully Added to City.";
                        CandidateAddResponseModel data = candidateResponse.CandidateAdd;
                        return Ok(new { status, message, data });
                    }
                }

                message = "Unable to Add Candidate.";
                return Ok(new { status, message });

            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It Fetch all the Candidate with their StateId
        /// </summary>
        /// <returns>If Candidate is fetched successfully then it return Ok with status true and data or else status false</returns>
        [HttpGet]
        public IActionResult GetAllCandidate()
        {
            try
            {
                bool status = false;
                string message;

                List<CandidateAddResponseModel> data = _candidateBusiness.GetAllCandidate();

                if (data != null)
                {
                    if (data.Count > 0)
                    {
                        status = true;
                        message = "Here is the list of all Candidates";
                        return Ok(new { status, message, data });
                    }
                    else
                    {
                        message = "No Candidates Present";
                        return Ok(new { status, message });
                    }
                }

                message = "Unable to Fetch the Candidates";
                return Ok(new { status, message });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It Fetch the specified Candidate details
        /// </summary>
        /// <param name="CandidateId">Candidate Id</param>
        /// <returns>If the Fetching is successfull then it return ok with status true and data or else false</returns>
        [HttpGet]
        [Route("{CandidateId}")]
        public IActionResult GetCandidateById(int CandidateId)
        {
            try
            {
                bool status = false;
                string message;

                CandidateAddResponseModel data = _candidateBusiness.GetCandidateById(CandidateId);
                if (data != null)
                {
                    status = true;
                    message = "Candidate Details has been Successfully fetched";
                    return Ok(new { status, message, data });
                }

                message = "Unable to get the specificed Candidate Details";
                return Ok(new { status, message });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It update the Candidate details
        /// </summary>
        /// <param name="CandidateId">Candidate Id</param>
        /// <param name="updateConstituency">Update Candidate Data</param>
        /// <returns>If Updation is successfull, it will return ok with status true and data or else false</returns>
        [HttpPut]
        [Route("{CandidateId}")]
        public IActionResult UpdateCandidate(int CandidateId, UpdateCandidateRequestModel updateCandidate)
        {
            try
            {
                bool status = false;
                string message;

                UpdateCandidateResponseModel candidateResponse = _candidateBusiness.UpdateCandidate(CandidateId, updateCandidate);

                if (candidateResponse != null)
                {
                    if (candidateResponse.ErrorResponse.ErrorStatus)
                    {
                        message = candidateResponse.ErrorResponse.Message;
                        return Ok(new { status, message });
                    }
                    else
                    {
                        status = true;
                        message = "Candidate Has Been Successfully Updated.";
                        CandidateUpdateResponseModel data = candidateResponse.CandidateUpdate;
                        return Ok(new { status, message, data });
                    }
                }

                message = "Unable to Update the Candidate Details";
                return Ok(new { status, message });

            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It Delete the Candidate.
        /// </summary>
        /// <param name="CandidateId">Candidate Id</param>
        /// <returns>If the Candidate is deleted Successfully, It return Ok with status True or else false</returns>
        [HttpDelete]
        [Route("{CandidateId}")]
        public IActionResult DeleteCandidate(int CandidateId)
        {
            try
            {
                bool status = false;
                string message;

                status = _candidateBusiness.DeleteCandidate(CandidateId);

                if (status)
                {
                    message = "Candidate has been Deleted Successfully";
                    return Ok(new { status, message });
                }
                message = "Unable to Delete the Candidate";
                return Ok(new { status, message });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

    }
}