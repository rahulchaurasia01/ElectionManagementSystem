using EmsCommonLayer.Model;
using EmsRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmsRepositoryLayer.Services
{
    public class PartyRepository : IPartyRepository
    {

        public static string sqlConnection = "Data Source=.;Initial Catalog=ElectionDB;Integrated Security=True";

        /// <summary>
        /// It Create a new Party
        /// </summary>
        /// <param name="createPartyRequest">Party Name</param>
        /// <returns>It Return the Reponse Model after creating the new party</returns>
        public CreatePartyResponseModel CreateParty(CreatePartyRequestModel createPartyRequest)
        {
            try
            {
                SqlDataReader reader;
                int statusCode, partyCreatedSuccess ;
                CreatePartyResponseModel partyResponse = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spParty", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@PartyId", -1);
                    sqlCommand.Parameters.AddWithValue("@Name", createPartyRequest.Name);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "Create");

                    SqlParameter PartyPresent = sqlCommand.Parameters.Add("@PartyPresentCount", System.Data.SqlDbType.Int);
                    PartyPresent.Direction = System.Data.ParameterDirection.ReturnValue;

                    SqlParameter cmdExecuteSuccess = sqlCommand.Parameters.Add("@return_value", System.Data.SqlDbType.Int);
                    cmdExecuteSuccess.Direction = System.Data.ParameterDirection.ReturnValue;

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();
                    partyCreatedSuccess = Convert.ToInt32(sqlCommand.Parameters["@PartyPresentCount"].Value);
                    statusCode = Convert.ToInt32(sqlCommand.Parameters["@return_Value"].Value);

                    if(partyCreatedSuccess > 0)
                    {
                        partyResponse = new CreatePartyResponseModel
                        {
                            ErrorResponse = new ErrorResponseModel
                            {
                                ErrorStatus = true,
                                Message = "Party Name Is already Present"
                            }
                        };

                        return partyResponse;
                    }

                    if (statusCode == 0)
                    {

                        while (reader.Read())
                        {
                            partyResponse = new CreatePartyResponseModel
                            {
                                PartyCreated = new PartyCreatedResponseModel()
                                {
                                    PartyId = Convert.ToInt32(reader[0]),
                                    Name = reader[1].ToString(),
                                    CreatedAt = Convert.ToDateTime(reader[2]),
                                    ModifiedAt = Convert.ToDateTime(reader[3])
                                },

                                ErrorResponse = new ErrorResponseModel()
                                {
                                    ErrorStatus = false
                                }
                                
                            };
                        }
                    }
                }
               
                return partyResponse;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It return all the party.
        /// </summary>
        /// <returns>Return Party</returns>
        public List<PartyCreatedResponseModel> GetAllParty()
        {
            try
            {
                int statusCode;
                SqlDataReader reader;
                List<PartyCreatedResponseModel> createParties = null;

                using(SqlConnection connection = new SqlConnection(sqlConnection))
                {

                    SqlCommand sqlCommand = new SqlCommand("spParty", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@PartyId", -1);
                    sqlCommand.Parameters.AddWithValue("@Name", "");
                    sqlCommand.Parameters.AddWithValue("@ActionType", "Get");

                    SqlParameter cmdExecuteSuccess = sqlCommand.Parameters.Add("@return_value", System.Data.SqlDbType.Int);
                    cmdExecuteSuccess.Direction = System.Data.ParameterDirection.ReturnValue;

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();
                    statusCode = Convert.ToInt32(sqlCommand.Parameters["@return_Value"].Value);

                    if(statusCode == 0)
                    {
                        createParties = new List<PartyCreatedResponseModel>();
                        while(reader.Read())
                        {
                            PartyCreatedResponseModel createParty = new PartyCreatedResponseModel
                            {
                                PartyId = Convert.ToInt32(reader[0]),
                                Name = reader[1].ToString(),
                                CreatedAt = Convert.ToDateTime(reader[2]),
                                ModifiedAt = Convert.ToDateTime(reader[3])
                            };
                            createParties.Add(createParty);
                        }
                    }
                }

                return createParties;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It update the party Name
        /// </summary>
        /// <param name="PartyId">Party Id</param>
        /// <param name="updateParty">Party Name</param>
        /// <returns>Update Party Response Model</returns>
        public UpdatepartyResponseModel UpdateParty(int PartyId, UpdatePartyRequestModel updateParty)
        {
            try
            {
                SqlDataReader reader;
                int statusCode, partyUpdatedSuccess;
                UpdatepartyResponseModel updateparty = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spParty", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@PartyId", PartyId);
                    sqlCommand.Parameters.AddWithValue("@Name", updateParty.Name);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "Update");

                    SqlParameter PartyPresent = sqlCommand.Parameters.Add("@PartyPresentCount", System.Data.SqlDbType.Int);
                    PartyPresent.Direction = System.Data.ParameterDirection.ReturnValue;

                    SqlParameter cmdExecuteSuccess = sqlCommand.Parameters.Add("@return_value", System.Data.SqlDbType.Int);
                    cmdExecuteSuccess.Direction = System.Data.ParameterDirection.ReturnValue;

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();
                    partyUpdatedSuccess = Convert.ToInt32(sqlCommand.Parameters["@PartyPresentCount"].Value);
                    statusCode = Convert.ToInt32(sqlCommand.Parameters["@return_Value"].Value);

                    if (partyUpdatedSuccess > 0)
                    {
                        updateparty = new UpdatepartyResponseModel
                        {
                            ErrorResponse = new ErrorResponseModel
                            {
                                ErrorStatus = true,
                                Message = "This Name Is already Present"
                            }
                        };

                        return updateparty;
                    }

                    if (statusCode == 0)
                    {
                        while (reader.Read())
                        {
                            updateparty = new UpdatepartyResponseModel
                            {
                                PartyUpdated = new PartyUpdatedResponseModel()
                                {
                                    PartyId = Convert.ToInt32(reader[0]),
                                    Name = reader[1].ToString(),
                                    CreatedAt = Convert.ToDateTime(reader[2]),
                                    ModifiedAt = Convert.ToDateTime(reader[3])
                                },

                                ErrorResponse = new ErrorResponseModel()
                                {
                                    ErrorStatus = false
                                }
                            };
                        }
                    }
                }

                return updateparty;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Delete the Party
        /// </summary>
        /// <param name="PartyId">Party Id</param>
        /// <returns>It return true, if party delete successfully or else false</returns>
        public bool DeleteParty(int PartyId)
        {
            try
            {
                int count;
                
                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spParty", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@PartyId", PartyId);
                    sqlCommand.Parameters.AddWithValue("@Name", "");
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
