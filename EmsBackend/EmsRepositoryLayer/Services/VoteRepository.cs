using EmsCommonLayer.Model;
using EmsRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmsRepositoryLayer.Services
{
    public class VoteRepository : IVoteRepository
    {

        public static string sqlConnection = "Data Source=.;Initial Catalog=ElectionDB;Integrated Security=True";

        /// <summary>
        /// It Add a vote to the candidate.
        /// </summary>
        /// <param name="addVote">CandidateId and EvmVote or PostalVote</param>
        /// <returns>Add Vote Response Model</returns>
        public AddVoteResponseModel AddVote(AddVoteRequestModel addVote)
        {
            try
            {
                SqlDataReader reader;
                int statusCode, CandidatePresent;
                string errorMsg = "";
                bool errorFlag = false;
                AddVoteResponseModel VoteResponse = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spVotes", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@VotesId", -1);
                    sqlCommand.Parameters.AddWithValue("@CandidateId", addVote.CandidateId);
                    sqlCommand.Parameters.AddWithValue("@EvmVote", addVote.EvmVote);
                    sqlCommand.Parameters.AddWithValue("@PostalVote", addVote.PostalVote);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "Add");

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
                        errorMsg = "This Candidate is Not Present";
                    }

                    if (errorFlag)
                    {
                        VoteResponse = new AddVoteResponseModel
                        {
                            ErrorResponse = new ErrorResponseModel
                            {
                                ErrorStatus = errorFlag,
                                Message = errorMsg
                            }
                        };

                        return VoteResponse;
                    }

                    if (statusCode == 0)
                    {
                        if (reader.HasRows)
                        {
                            VoteResponse = new AddVoteResponseModel();

                            while (reader.Read())
                            {
                                VoteResponse.VoteAdd = new VoteAddResponseModel
                                {
                                    VotesId = Convert.ToInt32(reader[0]),
                                    CandidateId = Convert.ToInt32(reader[1]),
                                    EvmVote = Convert.ToBoolean(reader[2]),
                                    PostalVote = Convert.ToBoolean(reader[3]),
                                    CreatedAt = Convert.ToDateTime(reader[4]),
                                    ModifiedAt = Convert.ToDateTime(reader[5])

                                };

                                VoteResponse.ErrorResponse = new ErrorResponseModel()
                                {
                                    ErrorStatus = false
                                };

                            }
                        }
                    }
                }

                return VoteResponse;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Delete the vote
        /// </summary>
        /// <param name="VotesId">Vote Id</param>
        /// <returns>If Deleted Successfully, it return true or else false</returns>
        public bool DeleteVote(int VotesId)
        {
            try
            {
                int count;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spVotes", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@VotesId", VotesId);
                    sqlCommand.Parameters.AddWithValue("@CandidateId", -1);
                    sqlCommand.Parameters.AddWithValue("@EvmVote", false);
                    sqlCommand.Parameters.AddWithValue("@PostalVote", false);
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
