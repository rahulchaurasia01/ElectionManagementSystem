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

        /// <summary>
        /// It Add City to the State
        /// </summary>
        /// <param name="addCity">City Name and State Id</param>
        /// <returns>Add City Response Model</returns>
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
                        errorMsg = "A State Cannot have Same City Name";
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

        /// <summary>
        /// It Fetch the list of all Cities.
        /// </summary>
        /// <returns>List of Cities</returns>
        public List<CityAddResponseModel> GetAllCity()
        {
            try
            {
                int statusCode;
                SqlDataReader reader;
                List<CityAddResponseModel> cities = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {

                    SqlCommand sqlCommand = new SqlCommand("spCity", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@CityId", -1);
                    sqlCommand.Parameters.AddWithValue("@Name", "");
                    sqlCommand.Parameters.AddWithValue("@StateId", -1);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "GetAllCity");

                    SqlParameter cmdExecuteSuccess = sqlCommand.Parameters.Add("@return_value", System.Data.SqlDbType.Int);
                    cmdExecuteSuccess.Direction = System.Data.ParameterDirection.ReturnValue;

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();
                    statusCode = Convert.ToInt32(sqlCommand.Parameters["@return_Value"].Value);

                    if (statusCode == 0)
                    {
                        cities = new List<CityAddResponseModel>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                CityAddResponseModel cityAdd = new CityAddResponseModel
                                {
                                    CityId = Convert.ToInt32(reader[0]),
                                    Name = reader[1].ToString(),
                                    StateId = Convert.ToInt32(reader[2]),
                                    CreatedAt = Convert.ToDateTime(reader[3]),
                                    ModifiedAt = Convert.ToDateTime(reader[4])
                                };
                                cities.Add(cityAdd);
                            }
                        }
                    }
                }

                return cities;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It fetchs the Specified city Details
        /// </summary>
        /// <param name="CityId">City Id</param>
        /// <returns>If fetching successfull, it return CityAddResponseModel or else null</returns>
        public CityAddResponseModel GetCityById(int CityId)
        {
            try
            {
                SqlDataReader reader;
                CityAddResponseModel city = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {

                    SqlCommand sqlCommand = new SqlCommand("spCity", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@CityId", CityId);
                    sqlCommand.Parameters.AddWithValue("@Name", "");
                    sqlCommand.Parameters.AddWithValue("@StateId", -1);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "GetCityById");

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        city = new CityAddResponseModel();

                        while (reader.Read())
                        {
                            city.CityId = Convert.ToInt32(reader[0]);
                            city.Name = reader[1].ToString();
                            city.StateId = Convert.ToInt32(reader[2]);
                            city.CreatedAt = Convert.ToDateTime(reader[3]);
                            city.ModifiedAt = Convert.ToDateTime(reader[4]);
                        }
                    }
                }

                return city;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Update the City
        /// </summary>
        /// <param name="CityId">City Id</param>
        /// <param name="updateCity">Update City Name or State Id</param>
        /// <returns>UpdateCityResponseModel</returns>
        public UpdateCityResponseModel UpdateCity(int CityId, UpdateCityRequestModel updateCity)
        {
            try
            {
                SqlDataReader reader;
                int statusCode, CityPresent;
                string errorMsg = "";
                bool errorFlag = false;
                UpdateCityResponseModel cityResponse = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spCity", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@CityId", CityId);
                    sqlCommand.Parameters.AddWithValue("@Name", updateCity.Name);
                    sqlCommand.Parameters.AddWithValue("@StateId", updateCity.StateId);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "Update");

                    SqlParameter CityPresentParameter = sqlCommand.Parameters.Add("@CityNamePresentCount", System.Data.SqlDbType.Int);
                    CityPresentParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                    SqlParameter cmdExecuteSuccess = sqlCommand.Parameters.Add("@return_value", System.Data.SqlDbType.Int);
                    cmdExecuteSuccess.Direction = System.Data.ParameterDirection.ReturnValue;

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();
                    CityPresent = Convert.ToInt32(sqlCommand.Parameters["@CityNamePresentCount"].Value);
                    statusCode = Convert.ToInt32(sqlCommand.Parameters["@return_Value"].Value);

                    if (CityPresent == -1)
                    {
                        errorFlag = true;
                        errorMsg = "This State is Not Present";
                    }
                    else if (CityPresent == -2)
                    {
                        errorFlag = true;
                        errorMsg = "A State Cannot have Same City Name";
                    }

                    if (errorFlag)
                    {
                        cityResponse = new UpdateCityResponseModel
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
                            cityResponse = new UpdateCityResponseModel();

                            while (reader.Read())
                            {
                                cityResponse.CityUpdate = new CityUpdateResponseModel
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

        /// <summary>
        /// It Delete the City
        /// </summary>
        /// <param name="CityId">City Id</param>
        /// <returns>If Delete Successfully, it return true or else false</returns>
        public bool DeleteCity(int CityId)
        {
            try
            {
                int count;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spCity", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@CityId", CityId);
                    sqlCommand.Parameters.AddWithValue("@Name", "");
                    sqlCommand.Parameters.AddWithValue("@StateId", -1);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "Delete");

                    connection.Open();

                    count = sqlCommand.ExecuteNonQuery();
                }

                return (count > 0) ? true : false;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
