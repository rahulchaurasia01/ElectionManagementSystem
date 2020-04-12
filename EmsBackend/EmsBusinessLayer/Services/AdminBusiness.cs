using EmsBusinessLayer.Interface;
using EmsCommonLayer.Model;
using EmsRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Services
{
    public class AdminBusiness : IAdminBusiness
    {

        private readonly IAdminRepository _adminRepository;

        public AdminBusiness(IAdminRepository adminRepository)
        {
            _adminRepository = adminRepository;
        }

        /// <summary>
        /// It Create an Admin Account
        /// </summary>
        /// <param name="createAdmin">Admin Data</param>
        /// <returns>CreateAdminResponseModel</returns>
        public CreateAdminResponseModel CreateAdmin(CreateAdminRequestModel createAdmin)
        {
            try
            {
                if (createAdmin == null)
                    return null;
                else
                    return _adminRepository.CreateAdmin(createAdmin);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Lgoin an Admin Account
        /// </summary>
        /// <param name="loginAdmin">Admin Email and Password</param>
        /// <returns>Create Admin Response Model</returns>
        public CreateAdminResponseModel LoginAdmin(LoginAdminRequestModel loginAdmin)
        {
            try
            {
                if (loginAdmin == null)
                    return null;
                else
                    return _adminRepository.LoginAdmin(loginAdmin);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
