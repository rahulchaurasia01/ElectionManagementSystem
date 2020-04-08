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
    public class StateController : ControllerBase
    {

        private readonly IStateBusiness _stateBusiness;

        public StateController(IStateBusiness stateBusiness)
        {
            _stateBusiness = stateBusiness;
        }

        /// <summary>
        /// It Add a State to the Database
        /// </summary>
        /// <param name="stateRequest">State Name</param>
        /// <returns>If Added Successfully, It return Ok with status true and data or else false</returns>
        [HttpPost]
        public IActionResult AddState(AddStateRequestModel stateRequest)
        {
            try
            {
                bool status = false;
                string message;

                AddStateResponseModel addState = _stateBusiness.AddState(stateRequest);

                if(addState != null)
                {
                    if(addState.ErrorResponse.ErrorStatus)
                    {
                        message = addState.ErrorResponse.Message;
                        return Ok(new { status, message });
                    }
                    status = true;
                    message = "State Added Sucessfully";
                    StateAddResponseModel data = addState.StateAdd;
                    return Ok(new { status, message, data });
                }

                message = "Unable to Add the State";
                return Ok(new { status, message });
            }
            catch(Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It Fetch all the State Available in the database
        /// </summary>
        /// <returns>If fetching of state is successfull then it return ok with status true and data or else false</returns>
        [HttpGet]
        public IActionResult GetAllState()
        {
            try
            {
                bool status = false;
                string message;

                List<StateAddResponseModel> data = _stateBusiness.GetAllState();

                if (data != null)
                {
                    if (data.Count == 0)
                    {
                        message = "No State is available";
                        return Ok(new { status, message });
                    }
                    else
                    {
                        status = true;
                        message = "Here is the list of all State";
                        return Ok(new { status, message, data });
                    }
                }
                message = "Unable to Fetch the list of State";
                return Ok(new { status, message });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It Fetch the Specific State by it's Id.
        /// </summary>
        /// <param name="StateId">State Id</param>
        /// <returns>If fetching of state is successfull then it return ok with status true and data or else false</returns>
        [HttpGet]
        [Route("{StateId}")]
        public IActionResult GetStateById(int StateId)
        {
            try
            {
                bool status = false;
                string message;

                StateAddResponseModel data = _stateBusiness.GetStateById(StateId);

                if(data != null)
                {
                    status = true;
                    message = "Here is the State Details";
                    return Ok(new { status, message, data });
                }
                message = "Unable to Fetch the State Details";
                return Ok(new { status, message });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It Update the State Name
        /// </summary>
        /// <param name="StateId">State Id</param>
        /// <param name="stateRequest">State Name</param>
        /// <returns>If the State name is successfully Updated, It Return Ok With status true and Data
        /// else with status false</returns>
        [HttpPut]
        [Route("{StateId}")]
        public IActionResult UpdateState(int StateId, UpdateStateRequestModel stateRequest)
        {
            try
            {
                bool status = false;
                string message;

                UpdateStateResponseModel updateState = _stateBusiness.UpdateState(StateId, stateRequest);

                if (updateState != null)
                {
                    if (updateState.ErrorResponse.ErrorStatus)
                    {
                        message = updateState.ErrorResponse.Message;
                        return Ok(new { status, message });
                    }
                    status = true;
                    message = "State Name has Been Updated";
                    StateUpdateResponseModel data = updateState.StateUpdate;
                    return Ok(new { status, message, data });
                }

                message = "Unable to Update the state";
                return Ok(new { status, message });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        /// <summary>
        /// It Delete the State.
        /// </summary>
        /// <param name="StateId">State Id</param>
        /// <returns>If the State is deleted Successfully, It return Ok with status True or else false</returns>
        [HttpDelete]
        [Route("{StateId}")]
        public IActionResult DeleteState(int StateId)
        {
            try
            {
                bool status = false;
                string message;

                status = _stateBusiness.DeleteState(StateId);

                if(status)
                {
                    message = "State has been Deleted Successfully";
                    return Ok(new { status, message });
                }
                message = "Unable to Delete the state";
                return Ok(new { status, message });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }


    }
}