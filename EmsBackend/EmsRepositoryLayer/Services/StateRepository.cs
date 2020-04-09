using EmsCommonLayer.Model;
using EmsRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmsRepositoryLayer.Services
{
    public class StateRepository : IStateRepository
    {

        public static string sqlConnection = "Data Source=.;Initial Catalog=ElectionDB;Integrated Security=True";

        /// <summary>
        /// It Add State to db
        /// </summary>
        /// <param name="stateRequest">State name</param>
        /// <returns>Add State Response Model</returns>
        public AddStateResponseModel AddState(AddStateRequestModel stateRequest)
        {
            try
            {
                SqlDataReader reader;
                int statusCode, StatePresent;
                AddStateResponseModel addState = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spState", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@StateId", -1);
                    sqlCommand.Parameters.AddWithValue("@Name", stateRequest.Name);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "Add");

                    SqlParameter StatePresentParameter = sqlCommand.Parameters.Add("@StatePresentCount", System.Data.SqlDbType.Int);
                    StatePresentParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                    SqlParameter cmdExecuteSuccess = sqlCommand.Parameters.Add("@return_value", System.Data.SqlDbType.Int);
                    cmdExecuteSuccess.Direction = System.Data.ParameterDirection.ReturnValue;

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();
                    StatePresent = Convert.ToInt32(sqlCommand.Parameters["@StatePresentCount"].Value);
                    statusCode = Convert.ToInt32(sqlCommand.Parameters["@return_Value"].Value);

                    if (StatePresent > 0)
                    {
                        addState = new AddStateResponseModel
                        {
                            ErrorResponse = new ErrorResponseModel
                            {
                                ErrorStatus = true,
                                Message = "State Name Is already Present"
                            }
                        };

                        return addState;
                    }

                    if (statusCode == 0)
                    {
                        while (reader.Read())
                        {
                            addState = new AddStateResponseModel
                            {
                                StateAdd = new StateAddResponseModel()
                                {
                                    StateId = Convert.ToInt32(reader[0]),
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

                return addState;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It fetch all the State available in the db
        /// </summary>
        /// <returns>list of all the state</returns>
        public List<StateAddResponseModel> GetAllState()
        {
            try
            {
                int statusCode;
                SqlDataReader reader;
                List<StateAddResponseModel> states = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {

                    SqlCommand sqlCommand = new SqlCommand("spState", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@StateId", -1);
                    sqlCommand.Parameters.AddWithValue("@Name", "");
                    sqlCommand.Parameters.AddWithValue("@ActionType", "GetAll");

                    SqlParameter cmdExecuteSuccess = sqlCommand.Parameters.Add("@return_value", System.Data.SqlDbType.Int);
                    cmdExecuteSuccess.Direction = System.Data.ParameterDirection.ReturnValue;

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();
                    statusCode = Convert.ToInt32(sqlCommand.Parameters["@return_Value"].Value);

                    if (statusCode == 0)
                    {
                        states = new List<StateAddResponseModel>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                StateAddResponseModel createParty = new StateAddResponseModel
                                {
                                    StateId = Convert.ToInt32(reader[0]),
                                    Name = reader[1].ToString(),
                                    CreatedAt = Convert.ToDateTime(reader[2]),
                                    ModifiedAt = Convert.ToDateTime(reader[3])
                                };
                                states.Add(createParty);
                            }
                        }
                    }
                }

                return states;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It fetches the specific state by its id
        /// </summary>
        /// <param name="StateId">State Id</param>
        /// <returns>State Add Response Model</returns>
        public StateAddResponseModel GetStateById(int StateId)
        {
            try
            {
                SqlDataReader reader;
                StateAddResponseModel State = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {

                    SqlCommand sqlCommand = new SqlCommand("spState", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@StateId", StateId);
                    sqlCommand.Parameters.AddWithValue("@Name", "");
                    sqlCommand.Parameters.AddWithValue("@ActionType", "GetById");

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();

                    if(reader.HasRows)
                    {
                        State = new StateAddResponseModel();

                        while (reader.Read())
                        {
                            State.StateId = Convert.ToInt32(reader[0]);
                            State.Name = reader[1].ToString();
                            State.CreatedAt = Convert.ToDateTime(reader[2]);
                            State.ModifiedAt = Convert.ToDateTime(reader[3]);
                        }
                    }
                }

                return State;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Update the State Name.
        /// </summary>
        /// <param name="StateId">State Id</param>
        /// <param name="stateRequest">State Name</param>
        /// <returns>Update State Response Model</returns>
        public UpdateStateResponseModel UpdateState(int StateId, UpdateStateRequestModel stateRequest)
        {
            try
            {
                SqlDataReader reader;
                int statusCode, StateUpdatedPresent;
                UpdateStateResponseModel updateState = null;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spState", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@StateId", StateId);
                    sqlCommand.Parameters.AddWithValue("@Name", stateRequest.Name);
                    sqlCommand.Parameters.AddWithValue("@ActionType", "Update");

                    SqlParameter StatePresentParameter = sqlCommand.Parameters.Add("@StateNamePresentCount", System.Data.SqlDbType.Int);
                    StatePresentParameter.Direction = System.Data.ParameterDirection.ReturnValue;

                    SqlParameter cmdExecuteSuccess = sqlCommand.Parameters.Add("@return_value", System.Data.SqlDbType.Int);
                    cmdExecuteSuccess.Direction = System.Data.ParameterDirection.ReturnValue;

                    connection.Open();

                    reader = sqlCommand.ExecuteReader();
                    StateUpdatedPresent = Convert.ToInt32(sqlCommand.Parameters["@StateNamePresentCount"].Value);
                    statusCode = Convert.ToInt32(sqlCommand.Parameters["@return_Value"].Value);

                    if (StateUpdatedPresent > 0)
                    {
                        updateState = new UpdateStateResponseModel
                        {
                            ErrorResponse = new ErrorResponseModel
                            {
                                ErrorStatus = true,
                                Message = "This Name Is already Present"
                            }
                        };

                        return updateState;
                    }

                    if (statusCode == 0)
                    {
                        while (reader.Read())
                        {
                            updateState = new UpdateStateResponseModel
                            {
                                StateUpdate = new StateUpdateResponseModel()
                                {
                                    StateId = Convert.ToInt32(reader[0]),
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

                return updateState;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Delete the State
        /// </summary>
        /// <param name="StateId">State Id</param>
        /// <returns>It return true, if State delete successfully or else false</returns>
        public bool DeleteState(int StateId)
        {
            try
            {
                int count;

                using (SqlConnection connection = new SqlConnection(sqlConnection))
                {
                    SqlCommand sqlCommand = new SqlCommand("spState", connection)
                    {
                        CommandType = System.Data.CommandType.StoredProcedure
                    };
                    sqlCommand.Parameters.AddWithValue("@StateId", StateId);
                    sqlCommand.Parameters.AddWithValue("@Name", "");
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
