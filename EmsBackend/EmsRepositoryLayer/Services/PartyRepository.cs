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
                using(SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spCreateParty", connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@Party", createPartyRequest.Name);

                    connection.Open();

                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    CreatePartyResponseModel partyResponse = null;

                    while(reader.Read())
                    {
                        partyResponse = new CreatePartyResponseModel
                        {
                            PartyId = Convert.ToInt32(reader[0]),
                            Name = reader[1].ToString(),
                            CreatedAt = Convert.ToDateTime(reader[2]),
                            ModifiedAt = Convert.ToDateTime(reader[3])
                        };
                    }

                    return partyResponse;

                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}
