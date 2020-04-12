using EmsCommonLayer.Model;
using EmsRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmsRepositoryLayer.Services
{
    public class UserRepository : IUserRepository
    {

        public static string sqlConnection = "Data Source=.;Initial Catalog=ElectionDB;Integrated Security=True";

        /// <summary>
        /// It fetch the result of election ConstituencyWise
        /// </summary>
        /// <param name="constituencyWise">State Name and ConstituencyId</param>
        /// <returns>ResultConstituencyWiseResponseModel</returns>
        public ResultConstituencyWiseResponseModel ConstituencyWise(ConstituencyWiseRequestModel constituencyWise)
        {
            try
            {
                SqlDataReader reader;
                int statusCode, ConstituencyPresent;
                string errorMsg = "";
                bool errorFlag = false;
                ResultConstituencyWiseResponseModel ConstituencyWise = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spUser", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@StateName", constituencyWise.State);
                    sqlCommand.Parameters.AddWithValue("@ConstituencyId", constituencyWise.ConstituencyId);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "ConstituencyWise");

                    SqlParameter CandidatePresentParameter = sqlCommand.Parameters.Add("@ConstituencyPresent", System.Data.SqlDbType.Int);
                    CandidatePresentParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                    SqlParameter cmdExecuteSuccess = sqlCommand.Parameters.Add("@return_value", System.Data.SqlDbType.Int);
                    cmdExecuteSuccess.Direction = System.Data.ParameterDirection.ReturnValue;

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();
                    ConstituencyPresent = Convert.ToInt32(sqlCommand.Parameters["@ConstituencyPresent"].Value);
                    statusCode = Convert.ToInt32(sqlCommand.Parameters["@return_Value"].Value);

                    if (ConstituencyPresent == -1)
                    {
                        errorFlag = true;
                        errorMsg = "This Constituency is Not Present";
                    }

                    if (errorFlag)
                    {
                        ConstituencyWise = new ResultConstituencyWiseResponseModel
                        {
                            ErrorResponse = new ErrorResponseModel
                            {
                                ErrorStatus = errorFlag,
                                Message = errorMsg
                            }
                        };

                        return ConstituencyWise;
                    }

                    if (statusCode == 0)
                    {
                        if (reader.HasRows)
                        {
                            ConstituencyWise = new ResultConstituencyWiseResponseModel();

                            ConstituencyWise.ConstituencyWises = new List<ConstituencyWiseResponseModel>();

                            while (reader.Read())
                            {
                                ConstituencyWiseResponseModel constituency = new ConstituencyWiseResponseModel
                                {
                                    CandidateName = reader[0].ToString(),
                                    PartyName = reader[1].ToString(),
                                    EvmVote = Convert.ToInt32(reader[2]),
                                    PostalVote = Convert.ToInt32(reader[3])
                                };

                                ConstituencyWise.ConstituencyWises.Add(constituency);

                            }

                            ConstituencyWise.ErrorResponse = new ErrorResponseModel
                            {
                                ErrorStatus = false
                            };
                        }
                    }
                }

                return ConstituencyWise;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ResultPartyWiseResponseModel PartyWise(PartyWiseRequestModel partyWise)
        {
            throw new NotImplementedException();
        }
    }
}
