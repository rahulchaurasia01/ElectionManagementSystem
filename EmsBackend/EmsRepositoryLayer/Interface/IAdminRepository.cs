using EmsCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsRepositoryLayer.Interface
{
    public interface IAdminRepository
    {

        CreateAdminResponseModel CreateAdmin(CreateAdminRequestModel createAdmin);

        CreateAdminResponseModel LoginAdmin(LoginAdminRequestModel loginAdmin);

    }
}
