using EmsCommonLayer.Model;
using EmsRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmsRepositoryLayer.Services
{
    public class CandidateRepository : ICandidateRepository
    {

        public static string sqlConnection = "Data Source=.;Initial Catalog=ElectionDB;Integrated Security=True";

        /// <summary>
        /// It Add Candidate
        /// </summary>
        /// <param name="addCandidate">Candidate Name and ConstituencyId and PartyId</param>
        /// <returns>Add Candidate Response Model</returns>
        public AddCandidateResponseModel AddCandidate(AddCandidateRequestModel addCandidate)
        {
            try
            {
                SqlDataReader reader;
                int statusCode, CandidatePresent;
                string errorMsg = "";
                bool errorFlag = false;
                AddCandidateResponseModel candidateResponse = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spCandidate", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@CandidateId", -1);
                    sqlCommand.Parameters.AddWithValue("@Name", addCandidate.Name);
                    sqlCommand.Parameters.AddWithValue("@ConstituencyId", addCandidate.ConstituencyId);
                    sqlCommand.Parameters.AddWithValue("@PartyId", addCandidate.PartyId);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "Create");

                    SqlParameter CandidatePresentParameter = sqlCommand.Parameters.Add("@CandidatePresentCount", System.Data.SqlDbType.Int);
                    CandidatePresentParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                    SqlParameter cmdExecuteSuccess = sqlCommand.Parameters.Add("@return_value", System.Data.SqlDbType.Int);
                    cmdExecuteSuccess.Direction = System.Data.ParameterDirection.ReturnValue;

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();
                    CandidatePresent = Convert.ToInt32(sqlCommand.Parameters["@CandidatePresentCount"].Value);
                    statusCode = Convert.ToInt32(sqlCommand.Parameters["@return_Value"].Value);

                    if (CandidatePresent == -1)
                    {
                        errorFlag = true;
                        errorMsg = "This Constituency is Not Present";
                    }
                    else if (CandidatePresent == -2)
                    {
                        errorFlag = true;
                        errorMsg = "This Party is Not Present";
                    }
                    else if (CandidatePresent == -3)
                    {
                        errorFlag = true;
                        errorMsg = "A Candidate is already present from this Constituency with the same Party";
                    }

                    if (errorFlag)
                    {
                        candidateResponse = new AddCandidateResponseModel
                        {
                            ErrorResponse = new ErrorResponseModel
                            {
                                ErrorStatus = errorFlag,
                                Message = errorMsg
                            }
                        };

                        return candidateResponse;
                    }

                    if (statusCode == 0)
                    {
                        if (reader.HasRows)
                        {
                            candidateResponse = new AddCandidateResponseModel();

                            while (reader.Read())
                            {
                                candidateResponse.CandidateAdd = new CandidateAddResponseModel
                                {
                                    CandidateId = Convert.ToInt32(reader[0]),
                                    Name = reader[1].ToString(),
                                    ConstituencyId = Convert.ToInt32(reader[2]),
                                    PartyId = Convert.ToInt32(reader[3]),
                                    CreatedAt = Convert.ToDateTime(reader[4]),
                                    ModifiedAt = Convert.ToDateTime(reader[5])
                                };

                                candidateResponse.ErrorResponse = new ErrorResponseModel()
                                {
                                    ErrorStatus = false
                                };

                            }
                        }
                    }
                }

                return candidateResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Fetch the list of all Candidates.
        /// </summary>
        /// <returns>List of all Candidates</returns>
        public List<CandidateAddResponseModel> GetAllCandidate()
        {
            try
            {
                int statusCode;
                SqlDataReader reader;
                List<CandidateAddResponseModel> candidates = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {

                    SqlCommand sqlCommand = new SqlCommand("spCandidate", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@CandidateId", -1);
                    sqlCommand.Parameters.AddWithValue("@Name", "");
                    sqlCommand.Parameters.AddWithValue("@ConstituencyId", -1);
                    sqlCommand.Parameters.AddWithValue("@PartyId", -1);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "GetAll");

                    SqlParameter cmdExecuteSuccess = sqlCommand.Parameters.Add("@return_value", System.Data.SqlDbType.Int);
                    cmdExecuteSuccess.Direction = System.Data.ParameterDirection.ReturnValue;

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();
                    statusCode = Convert.ToInt32(sqlCommand.Parameters["@return_Value"].Value);

                    if (statusCode == 0)
                    {
                        candidates = new List<CandidateAddResponseModel>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                CandidateAddResponseModel candidate = new CandidateAddResponseModel
                                {
                                    CandidateId = Convert.ToInt32(reader[0]),
                                    Name = reader[1].ToString(),
                                    ConstituencyId = Convert.ToInt32(reader[2]),
                                    PartyId = Convert.ToInt32(reader[3]),
                                    CreatedAt = Convert.ToDateTime(reader[4]),
                                    ModifiedAt = Convert.ToDateTime(reader[5])
                                };
                                candidates.Add(candidate);
                            }
                        }
                    }
                }

                return candidates;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It fetchs the Specified Candidate Details
        /// </summary>
        /// <param name="CandidateId">Candidate Id</param>
        /// <returns>Candidate Add Response Model</returns>
        public CandidateAddResponseModel GetCandidateById(int CandidateId)
        {
            try
            {
                SqlDataReader reader;
                CandidateAddResponseModel candidate = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {

                    SqlCommand sqlCommand = new SqlCommand("spCandidate", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@CandidateId", CandidateId);
                    sqlCommand.Parameters.AddWithValue("@Name", "");
                    sqlCommand.Parameters.AddWithValue("@ConstituencyId", -1);
                    sqlCommand.Parameters.AddWithValue("@PartyId", -1);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "GetCandidateById");

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();

                    if (reader.HasRows)
                    {
                        candidate = new CandidateAddResponseModel();

                        while (reader.Read())
                        {
                            candidate.CandidateId = Convert.ToInt32(reader[0]);
                            candidate.Name = reader[1].ToString();
                            candidate.ConstituencyId = Convert.ToInt32(reader[2]);
                            candidate.PartyId = Convert.ToInt32(reader[3]);
                            candidate.CreatedAt = Convert.ToDateTime(reader[4]);
                            candidate.ModifiedAt = Convert.ToDateTime(reader[5]);
                        }
                    }
                }

                return candidate;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Update the Candidate
        /// </summary>
        /// <param name="CandidateId">Candidate Id</param>
        /// <param name="updateCandidate">Update Candidate Name, ConstituencyId and PartyId</param>
        /// <returns>Update Candidate Response Model</returns>
        public UpdateCandidateResponseModel UpdateCandidate(int CandidateId, UpdateCandidateRequestModel updateCandidate)
        {
            try
            {
                SqlDataReader reader;
                int statusCode, CandidatePresent;
                string errorMsg = "";
                bool errorFlag = false;
                UpdateCandidateResponseModel candidateResponse = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spCandidate", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@CandidateId", CandidateId);
                    sqlCommand.Parameters.AddWithValue("@Name", updateCandidate.Name);
                    sqlCommand.Parameters.AddWithValue("@ConstituencyId", -1);
                    sqlCommand.Parameters.AddWithValue("@PartyId", -1);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "Update");

                    SqlParameter CandidatePresentParameter = sqlCommand.Parameters.Add("@CandidateNamePresentCount", System.Data.SqlDbType.Int);
                    CandidatePresentParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                    SqlParameter cmdExecuteSuccess = sqlCommand.Parameters.Add("@return_value", System.Data.SqlDbType.Int);
                    cmdExecuteSuccess.Direction = System.Data.ParameterDirection.ReturnValue;

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();
                    CandidatePresent = Convert.ToInt32(sqlCommand.Parameters["@CandidateNamePresentCount"].Value);
                    statusCode = Convert.ToInt32(sqlCommand.Parameters["@return_Value"].Value);

                    if (CandidatePresent == -1)
                    {
                        errorFlag = true;
                        errorMsg = "This Candidate is Not Present";
                    }

                    if (errorFlag)
                    {
                        candidateResponse = new UpdateCandidateResponseModel
                        {
                            ErrorResponse = new ErrorResponseModel
                            {
                                ErrorStatus = errorFlag,
                                Message = errorMsg
                            }
                        };

                        return candidateResponse;
                    }

                    if (statusCode == 0)
                    {
                        if (reader.HasRows)
                        {
                            candidateResponse = new UpdateCandidateResponseModel();

                            while (reader.Read())
                            {
                                candidateResponse.CandidateUpdate = new CandidateUpdateResponseModel
                                {
                                    CandidateId = Convert.ToInt32(reader[0]),
                                    Name = reader[1].ToString(),
                                    ConstituencyId = Convert.ToInt32(reader[2]),
                                    PartyId = Convert.ToInt32(reader[3]),
                                    CreatedAt = Convert.ToDateTime(reader[4]),
                                    ModifiedAt = Convert.ToDateTime(reader[5])
                                };

                                candidateResponse.ErrorResponse = new ErrorResponseModel()
                                {
                                    ErrorStatus = false
                                };

                            }
                        }
                    }
                }

                return candidateResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Delete the Candidate
        /// </summary>
        /// <param name="CandidateId">Candidate Id</param>
        /// <returns>If Delete Successfully, it return true or else false</returns>
        public bool DeleteCandidate(int CandidateId)
        {
            try
            {
                int count;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spCandidate", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@CandidateId", CandidateId);
                    sqlCommand.Parameters.AddWithValue("@Name", "");
                    sqlCommand.Parameters.AddWithValue("@ConstituencyId", -1);
                    sqlCommand.Parameters.AddWithValue("@PartyId", -1);
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
