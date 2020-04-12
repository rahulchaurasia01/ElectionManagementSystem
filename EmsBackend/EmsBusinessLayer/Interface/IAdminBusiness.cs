using EmsCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Interface
{
    public interface IAdminBusiness
    {

        CreateAdminResponseModel CreateAdmin(CreateAdminRequestModel createAdmin);

        CreateAdminResponseModel LoginAdmin(LoginAdminRequestModel loginAdmin);

    }
}
