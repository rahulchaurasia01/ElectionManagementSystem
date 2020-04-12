using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EmsBusinessLayer.Interface;
using EmsCommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace EmsBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly IAdminBusiness _adminBusiness;
        private readonly IConfiguration _configuration;

        public AdminController(IAdminBusiness adminBusiness, IConfiguration configuration)
        {
            _adminBusiness = adminBusiness;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("Registration")]
        public IActionResult CreateAdmin(CreateAdminRequestModel createAdmin)
        {
            try
            {
                bool status = false;
                string message;

                CreateAdminResponseModel adminResponse = _adminBusiness.CreateAdmin(createAdmin);

                if(adminResponse != null)
                {
                    if(adminResponse.ErrorResponse.ErrorStatus)
                    {
                        message = adminResponse.ErrorResponse.Message;
                        return Ok(new { status, message });
                    }
                    else
                    {
                        status = true;
                        message = "Admin Account Created Successfully";
                        AdminCreateResponseModel data = adminResponse.AdminCreate;
                        string token = GenerateToken(data, "Registration");
                        return Ok(new { status, message, data, token });
                    }
                }
                message = "Unable to Create the Admin Account.";
                return Ok(new { status, message });
            }
            catch(Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult LoginAdmin(LoginAdminRequestModel loginAdmin)
        {
            try
            {
                bool status = false;
                string message;

                CreateAdminResponseModel adminLogin = _adminBusiness.LoginAdmin(loginAdmin);

                if (adminLogin != null)
                {
                    if (adminLogin.ErrorResponse.ErrorStatus)
                    {
                        message = adminLogin.ErrorResponse.Message;
                        return Ok(new { status, message });
                    }
                    else
                    {
                        status = true;
                        message = "You has been Successfully Login";
                        AdminCreateResponseModel data = adminLogin.AdminCreate;
                        string token = GenerateToken(data, "Login");
                        return Ok(new { status, message, data, token });
                    }
                }

                message = "Unable to Login Admin";
                return Ok(new { status, message });
            }
            catch(Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }


        /// <summary>
        /// It Generate the Token
        /// </summary>
        /// <param name="createAdmin">Admin Data</param>
        /// <param name="type">Access Type</param>
        /// <returns>Jwt Generated Token</returns>
        private string GenerateToken(AdminCreateResponseModel createAdmin, string type)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim("AdminId", createAdmin.AdminId.ToString()),
                    new Claim("EmailId", createAdmin.EmailId.ToString()),
                    new Claim("TokenType", type)
                };

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"],
                    claims, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}