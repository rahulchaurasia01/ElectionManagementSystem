using EmsCommonLayer.Model;
using EmsRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmsRepositoryLayer.Services
{
    public class AdminRepository : IAdminRepository
    {

        public static string sqlConnection = "Data Source=.;Initial Catalog=ElectionDB;Integrated Security=True";

        /// <summary>
        /// It Create An Admin Account
        /// </summary>
        /// <param name="createAdmin">Admin Data</param>
        /// <returns>Create Admin Response Model</returns>
        public CreateAdminResponseModel CreateAdmin(CreateAdminRequestModel createAdmin)
        {
            try
            {
                SqlDataReader reader;
                int statusCode, AdminPresent;
                string errorMsg = "";
                bool errorFlag = false;
                CreateAdminResponseModel adminResponse = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spAdmin", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@EmailId", createAdmin.EmailId);

                    createAdmin.Password = EncodeDecode.EncodePasswordToBase64(createAdmin.Password);

                    sqlCommand.Parameters.AddWithValue("@Password", createAdmin.Password);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "Create");

                    SqlParameter CandidatePresentParameter = sqlCommand.Parameters.Add("@EmailPresent", System.Data.SqlDbType.Int);
                    CandidatePresentParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                    SqlParameter cmdExecuteSuccess = sqlCommand.Parameters.Add("@return_value", System.Data.SqlDbType.Int);
                    cmdExecuteSuccess.Direction = System.Data.ParameterDirection.ReturnValue;

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();
                    AdminPresent = Convert.ToInt32(sqlCommand.Parameters["@EmailPresent"].Value);
                    statusCode = Convert.ToInt32(sqlCommand.Parameters["@return_Value"].Value);

                    if (AdminPresent == -1)
                    {
                        errorFlag = true;
                        errorMsg = "This EmailId is already Present";
                    }

                    if (errorFlag)
                    {
                        adminResponse = new CreateAdminResponseModel
                        {
                            ErrorResponse = new ErrorResponseModel
                            {
                                ErrorStatus = errorFlag,
                                Message = errorMsg
                            }
                        };

                        return adminResponse;
                    }

                    if (statusCode == 0)
                    {
                        if (reader.HasRows)
                        {
                            adminResponse = new CreateAdminResponseModel();

                            while (reader.Read())
                            {
                                adminResponse.AdminCreate = new AdminCreateResponseModel
                                {
                                    AdminId = Convert.ToInt32(reader[0]),
                                    EmailId = reader[1].ToString(),
                                    CreatedAt = Convert.ToDateTime(reader[2]),
                                    ModifiedAt = Convert.ToDateTime(reader[3])
                                };

                                adminResponse.ErrorResponse = new ErrorResponseModel
                                {
                                    ErrorStatus = false
                                };

                            }
                        }
                    }
                }

                return adminResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Login an Admin Account
        /// </summary>
        /// <param name="loginAdmin">Login admin</param>
        /// <returns>CreateAdminResponseModel</returns>
        public CreateAdminResponseModel LoginAdmin(LoginAdminRequestModel loginAdmin)
        {
            try
            {
                SqlDataReader reader;
                int statusCode, AdminPresent;
                string errorMsg = "";
                bool errorFlag = false;
                CreateAdminResponseModel adminResponse = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spAdmin", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@EmailId", loginAdmin.EmailId);

                    loginAdmin.Password = EncodeDecode.EncodePasswordToBase64(loginAdmin.Password);

                    sqlCommand.Parameters.AddWithValue("@Password", loginAdmin.Password);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "Login");

                    SqlParameter CandidatePresentParameter = sqlCommand.Parameters.Add("@adminPresent", System.Data.SqlDbType.Int);
                    CandidatePresentParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                    SqlParameter cmdExecuteSuccess = sqlCommand.Parameters.Add("@return_value", System.Data.SqlDbType.Int);
                    cmdExecuteSuccess.Direction = System.Data.ParameterDirection.ReturnValue;

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();
                    AdminPresent = Convert.ToInt32(sqlCommand.Parameters["@adminPresent"].Value);
                    statusCode = Convert.ToInt32(sqlCommand.Parameters["@return_Value"].Value);

                    if (AdminPresent == -1)
                    {
                        errorFlag = true;
                        errorMsg = "Your EmailId Or Password is Incorrect";
                    }

                    if (errorFlag)
                    {
                        adminResponse = new CreateAdminResponseModel
                        {
                            ErrorResponse = new ErrorResponseModel
                            {
                                ErrorStatus = errorFlag,
                                Message = errorMsg
                            }
                        };

                        return adminResponse;
                    }

                    if (statusCode == 0)
                    {
                        if (reader.HasRows)
                        {
                            adminResponse = new CreateAdminResponseModel();

                            while (reader.Read())
                            {
                                adminResponse.AdminCreate = new AdminCreateResponseModel
                                {
                                    AdminId = Convert.ToInt32(reader[0]),
                                    EmailId = reader[1].ToString(),
                                    CreatedAt = Convert.ToDateTime(reader[2]),
                                    ModifiedAt = Convert.ToDateTime(reader[3])
                                };

                                adminResponse.ErrorResponse = new ErrorResponseModel
                                {
                                    ErrorStatus = false
                                };

                            }
                        }
                    }
                }

                return adminResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
