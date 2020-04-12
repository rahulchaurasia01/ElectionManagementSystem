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
    public class UserController : ControllerBase
    {

        private readonly IUserBusiness _userBusiness;

        public UserController(IUserBusiness userBusiness)
        {
            _userBusiness = userBusiness;
        }

        [HttpPost]
        [Route("ConstituencyWise")]
        public IActionResult ConstituencyWise(ConstituencyWiseRequestModel constituencyWise)
        {
            try
            {
                bool status = false;
                string message;

                ResultConstituencyWiseResponseModel constituencyWiseResponse = _userBusiness.ConstituencyWise(constituencyWise);

                if(constituencyWiseResponse != null)
                {
                    if(constituencyWiseResponse.ErrorResponse.ErrorStatus)
                    {
                        message = constituencyWiseResponse.ErrorResponse.Message;
                        return Ok(new { status, message });
                    }
                    else
                    {
                        status = true;
                        message = "Your Result has been Successfully Fetched.";
                        List<ConstituencyWiseResponseModel> data = constituencyWiseResponse.ConstituencyWises;
                        return Ok(new { status, message, data });
                    }
                }

                message = "Unable to fetch the Constituency Wise election result";
                return Ok(new { status, message });
            }
            catch(Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpPost]
        [Route("PartyWise")]
        public IActionResult PartyWise(PartyWiseRequestModel partyWise)
        {
            try
            {
                bool status = false;
                string message;

                message = "Unable to fetch the PartyWise Election Result";
                return Ok(new { status, message });
            }
            catch(Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

    }
}