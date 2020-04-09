using EmsCommonLayer.Model;
using EmsRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmsRepositoryLayer.Services
{
    public class ConstituencyRepository : IConstituencyRepository
    {

        public static string sqlConnection = "Data Source=.;Initial Catalog=ElectionDB;Integrated Security=True";

        /// <summary>
        /// It Add Constituency to the City
        /// </summary>
        /// <param name="addConstituency">Constituency Name and City Id</param>
        /// <returns>Add Constituency Response Model</returns>
        public AddConstituencyResponseModel AddConstituency(AddConstituencyRequestModel addConstituency)
        {
            try
            {
                SqlDataReader reader;
                int statusCode, ConstituencyPresent;
                string errorMsg = "";
                bool errorFlag = false;
                AddConstituencyResponseModel constituencyResponse = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spConstituency", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ConstituencyId", -1);
                    sqlCommand.Parameters.AddWithValue("@Name", addConstituency.Name);
                    sqlCommand.Parameters.AddWithValue("@CityId", addConstituency.CityId);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "Add");

                    SqlParameter CityPresentParameter = sqlCommand.Parameters.Add("@ConstituencyPresentCount", System.Data.SqlDbType.Int);
                    CityPresentParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                    SqlParameter cmdExecuteSuccess = sqlCommand.Parameters.Add("@return_value", System.Data.SqlDbType.Int);
                    cmdExecuteSuccess.Direction = System.Data.ParameterDirection.ReturnValue;

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();
                    ConstituencyPresent = Convert.ToInt32(sqlCommand.Parameters["@ConstituencyPresentCount"].Value);
                    statusCode = Convert.ToInt32(sqlCommand.Parameters["@return_Value"].Value);

                    if (ConstituencyPresent == -1)
                    {
                        errorFlag = true;
                        errorMsg = "This City is Not Present";
                    }
                    else if (ConstituencyPresent == -2)
                    {
                        errorFlag = true;
                        errorMsg = "A City Cannot have Same Constituency Name";
                    }

                    if (errorFlag)
                    {
                        constituencyResponse = new AddConstituencyResponseModel
                        {
                            ErrorResponse = new ErrorResponseModel
                            {
                                ErrorStatus = errorFlag,
                                Message = errorMsg
                            }
                        };

                        return constituencyResponse;
                    }

                    if (statusCode == 0)
                    {
                        if (reader.HasRows)
                        {
                            constituencyResponse = new AddConstituencyResponseModel();

                            while (reader.Read())
                            {
                                constituencyResponse.ConstituencyAdd = new ConstituencyAddResponseModel
                                {
                                    ConstituencyId = Convert.ToInt32(reader[0]),
                                    Name = reader[1].ToString(),
                                    CityId = Convert.ToInt32(reader[2]),
                                    CreatedAt = Convert.ToDateTime(reader[3]),
                                    ModifiedAt = Convert.ToDateTime(reader[4])
                                };

                                constituencyResponse.ErrorResponse = new ErrorResponseModel()
                                {
                                    ErrorStatus = false
                                };

                            }
                        }
                    }
                }

                return constituencyResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Fetch the list of all Constituencies.
        /// </summary>
        /// <returns>List of all Constituencies</returns>
        public List<ConstituencyAddResponseModel> GetAllConstituency()
        {
            try
            {
                int statusCode;
                SqlDataReader reader;
                List<ConstituencyAddResponseModel> constituencies = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {

                    SqlCommand sqlCommand = new SqlCommand("spConstituency", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ConstituencyId", -1);
                    sqlCommand.Parameters.AddWithValue("@Name", "");
                    sqlCommand.Parameters.AddWithValue("@CityId", -1);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "GetAll");

                    SqlParameter cmdExecuteSuccess = sqlCommand.Parameters.Add("@return_value", System.Data.SqlDbType.Int);
                    cmdExecuteSuccess.Direction = System.Data.ParameterDirection.ReturnValue;

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();
                    statusCode = Convert.ToInt32(sqlCommand.Parameters["@return_Value"].Value);

                    if (statusCode == 0)
                    {
                        constituencies = new List<ConstituencyAddResponseModel>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                ConstituencyAddResponseModel constituency = new ConstituencyAddResponseModel
                                {
                                    ConstituencyId = Convert.ToInt32(reader[0]),
                                    Name = reader[1].ToString(),
                                    CityId = Convert.ToInt32(reader[2]),
                                    CreatedAt = Convert.ToDateTime(reader[3]),
                                    ModifiedAt = Convert.ToDateTime(reader[4])
                                };
                                constituencies.Add(constituency);
                            }
                        }
                    }
                }

                return constituencies;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It fetchs the Specified Constituency Details
        /// </summary>
        /// <param name="ConstituencyId">Constituency Id</param>
        /// <returns>Constituency Add Response Model</returns>
        public ConstituencyAddResponseModel GetConstituencyById(int ConstituencyId)
        {
            try
            {
                SqlDataReader reader;
                ConstituencyAddResponseModel constituency = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {

                    SqlCommand sqlCommand = new SqlCommand("spConstituency", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ConstituencyId", ConstituencyId);
                    sqlCommand.Parameters.AddWithValue("@Name", "");
                    sqlCommand.Parameters.AddWithValue("@CityId", -1);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "GetConstituencyById");

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        constituency = new ConstituencyAddResponseModel();

                        while (reader.Read())
                        {
                            constituency.ConstituencyId = Convert.ToInt32(reader[0]);
                            constituency.Name = reader[1].ToString();
                            constituency.CityId = Convert.ToInt32(reader[2]);
                            constituency.CreatedAt = Convert.ToDateTime(reader[3]);
                            constituency.ModifiedAt = Convert.ToDateTime(reader[4]);
                        }
                    }
                }

                return constituency;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Update the Constituency
        /// </summary>
        /// <param name="ConstituencyId">Constituency Id</param>
        /// <param name="updateConstituency">Update Constituency Name and City Id</param>
        /// <returns>Update Constituency Response Model</returns>
        public UpdateConstituencyResponseModel UpdateConstituency(int ConstituencyId, UpdateConstituencyRequestModel updateConstituency)
        {
            try
            {
                SqlDataReader reader;
                int statusCode, ConstituencyPresent;
                string errorMsg = "";
                bool errorFlag = false;
                UpdateConstituencyResponseModel constituencyResponse = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spConstituency", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ConstituencyId", ConstituencyId);
                    sqlCommand.Parameters.AddWithValue("@Name", updateConstituency.Name);
                    sqlCommand.Parameters.AddWithValue("@CityId", updateConstituency.CityId);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "Update");

                    SqlParameter CityPresentParameter = sqlCommand.Parameters.Add("@ConstituencyNamePresentCount", System.Data.SqlDbType.Int);
                    CityPresentParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                    SqlParameter cmdExecuteSuccess = sqlCommand.Parameters.Add("@return_value", System.Data.SqlDbType.Int);
                    cmdExecuteSuccess.Direction = System.Data.ParameterDirection.ReturnValue;

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();
                    ConstituencyPresent = Convert.ToInt32(sqlCommand.Parameters["@ConstituencyNamePresentCount"].Value);
                    statusCode = Convert.ToInt32(sqlCommand.Parameters["@return_Value"].Value);

                    if (ConstituencyPresent == -1)
                    {
                        errorFlag = true;
                        errorMsg = "This City is Not Present";
                    }
                    else if (ConstituencyPresent == -2)
                    {
                        errorFlag = true;
                        errorMsg = "A City Cannot have Same Constituency Name";
                    }

                    if (errorFlag)
                    {
                        constituencyResponse = new UpdateConstituencyResponseModel
                        {
                            ErrorResponse = new ErrorResponseModel
                            {
                                ErrorStatus = errorFlag,
                                Message = errorMsg
                            }
                        };

                        return constituencyResponse;
                    }

                    if (statusCode == 0)
                    {
                        if (reader.HasRows)
                        {
                            constituencyResponse = new UpdateConstituencyResponseModel();

                            while (reader.Read())
                            {
                                constituencyResponse.ConstituencyUpdate = new ConstituencyUpdateResponseModel
                                {
                                    ConstituencyId = Convert.ToInt32(reader[0]),
                                    Name = reader[1].ToString(),
                                    CityId = Convert.ToInt32(reader[2]),
                                    CreatedAt = Convert.ToDateTime(reader[3]),
                                    ModifiedAt = Convert.ToDateTime(reader[4])
                                };

                                constituencyResponse.ErrorResponse = new ErrorResponseModel()
                                {
                                    ErrorStatus = false
                                };

                            }
                        }
                    }
                }

                return constituencyResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Delete the Constituency
        /// </summary>
        /// <param name="ConstituencyId">Constituency Id</param>
        /// <returns>If Delete Successfully, it return true or else false</returns>
        public bool DeleteConstituency(int ConstituencyId)
        {
            try
            {
                int count;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spConstituency", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@ConstituencyId", ConstituencyId);
                    sqlCommand.Parameters.AddWithValue("@Name", "");
                    sqlCommand.Parameters.AddWithValue("@CityId", -1);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "Delete");

                    connection.Open();

                    count = sqlCommand.ExecuteNonQuery();
                }

                return (count > 0) ? true : false;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
