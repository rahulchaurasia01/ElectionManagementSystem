using EmsBusinessLayer.Interface;
using EmsCommonLayer.Model;
using EmsRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Services
{
    public class StateBusiness : IStateBusiness
    {

        private readonly IStateRepository _stateRepository;

        public StateBusiness(IStateRepository stateRepository)
        {
            _stateRepository = stateRepository;
        }

        /// <summary>
        /// It Add a State to db
        /// </summary>
        /// <param name="stateRequest">State Name</param>
        /// <returns>Add State Response Model</returns>
        public AddStateResponseModel AddState(AddStateRequestModel stateRequest)
        {
            try
            {
                if (stateRequest == null)
                    return null;
                else
                    return _stateRepository.AddState(stateRequest);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It fetchs the list of all State
        /// </summary>
        /// <returns>Return List of All State</returns>
        public List<StateAddResponseModel> GetAllState()
        {
            try
            {
                return _stateRepository.GetAllState();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Fetchs the specific state by its stateId
        /// </summary>
        /// <param name="StateId">StateId</param>
        /// <returns>State Add Response Model</returns>
        public StateAddResponseModel GetStateById(int StateId)
        {
            try
            {
                if (StateId <= 0)
                    return null;
                else
                    return _stateRepository.GetStateById(StateId);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Update the State Name
        /// </summary>
        /// <param name="StateId">StateId</param>
        /// <param name="stateRequest">State Name</param>
        /// <returns>Update State Response Model</returns>
        public UpdateStateResponseModel UpdateState(int StateId, UpdateStateRequestModel stateRequest)
        {
            try
            {
                if (StateId <= 0 || stateRequest == null)
                    return null;
                else
                    return _stateRepository.UpdateState(StateId, stateRequest);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Delete the state
        /// </summary>
        /// <param name="StateId">StateId</param>
        /// <returns>It return true, if Deleted is successfull or else false</returns>
        public bool DeleteState(int StateId)
        {
            try
            {
                if (StateId <= 0)
                    return false;
                else
                    return _stateRepository.DeleteState(StateId);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
