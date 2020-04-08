using EmsCommonLayer.Model;
using EmsRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmsRepositoryLayer.Services
{
    public class CityRepository : ICityRepository
    {

        public static string sqlConnection = "Data Source=.;Initial Catalog=ElectionDB;Integrated Security=True";

        public AddCityResponseModel AddCity(AddCityRequestModel addCity)
        {
            try
            {
                SqlDataReader reader;
                int statusCode, CityPresent;
                string errorMsg = "";
                bool errorFlag = false;
                AddCityResponseModel cityResponse = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spCity", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@CityId", -1);
                    sqlCommand.Parameters.AddWithValue("@Name", addCity.Name);
                    sqlCommand.Parameters.AddWithValue("@StateId", addCity.StateId);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "Add");

                    SqlParameter CityPresentParameter = sqlCommand.Parameters.Add("@CityPresentCount", System.Data.SqlDbType.Int);
                    CityPresentParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                    SqlParameter cmdExecuteSuccess = sqlCommand.Parameters.Add("@return_value", System.Data.SqlDbType.Int);
                    cmdExecuteSuccess.Direction = System.Data.ParameterDirection.ReturnValue;

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();
                    CityPresent = Convert.ToInt32(sqlCommand.Parameters["@CityPresentCount"].Value);
                    statusCode = Convert.ToInt32(sqlCommand.Parameters["@return_Value"].Value);

                    if (CityPresent == -1)
                    {
                        errorFlag = true;
                        errorMsg = "This State is Not Present";
                    }
                    else if(CityPresent == -2)
                    {
                        errorFlag = true;
                        errorMsg = "A State Cannot have Same City";
                    }

                    if(errorFlag)
                    {
                        cityResponse = new AddCityResponseModel
                        {
                            ErrorResponse = new ErrorResponseModel
                            {
                                ErrorStatus = errorFlag,
                                Message = errorMsg
                            }
                        };

                        return cityResponse;
                    }

                    if (statusCode == 0)
                    {
                        if (reader.HasRows)
                        {
                            cityResponse = new AddCityResponseModel();

                            while (reader.Read())
                            {
                                cityResponse.CityAdd = new CityAddResponseModel
                                {
                                    CityId = Convert.ToInt32(reader[0]),
                                    Name = reader[1].ToString(),
                                    StateId = Convert.ToInt32(reader[2]),
                                    CreatedAt = Convert.ToDateTime(reader[3]),
                                    ModifiedAt = Convert.ToDateTime(reader[4])
                                };

                                cityResponse.ErrorResponse = new ErrorResponseModel()
                                {
                                    ErrorStatus = false
                                };

                            }
                        }
                    }
                }

                return cityResponse;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
