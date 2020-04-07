using EmsBusinessLayer.Interface;
using EmsCommonLayer.Model;
using EmsRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Services
{
    public class PartyBusiness : IPartyBusiness
    {

        private readonly IPartyRepository _partyRepository;

        public PartyBusiness(IPartyRepository partyRepository)
        {
            _partyRepository = partyRepository;
        }

        /// <summary>
        /// It create a new Party 
        /// </summary>
        /// <param name="createPartyRequest">Party Name</param>
        /// <returns>If Party is created Successfully it return Party response model else null</returns>
        public CreatePartyResponseModel CreateParty(CreatePartyRequestModel createPartyRequest)
        {
            try
            {
                if (createPartyRequest == null)
                    return null;
                else
                    return _partyRepository.CreateParty(createPartyRequest);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Return All the Party
        /// </summary>
        /// <returns>It Return All the Party</returns>
        public List<PartyCreatedResponseModel> GetAllParty()
        {
            try
            {
                return _partyRepository.GetAllParty();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Return the specific party data
        /// </summary>
        /// <param name="PartyId">Party If</param>
        /// <returns>Party Created Response Model</returns>
        public PartyCreatedResponseModel GetPartyById(int PartyId)
        {
            try
            {
                if (PartyId <= 0)
                    return null;
                else
                    return _partyRepository.GetPartyById(PartyId);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Update the Party Name
        /// </summary>
        /// <param name="PartyId">Party Id</param>
        /// <param name="updateParty">Update Party Name</param>
        /// <returns>return updatepartyResponseModel if successfull or else null</returns>
        public UpdatepartyResponseModel UpdateParty(int PartyId, UpdatePartyRequestModel updateParty)
        {
            try
            {
                if (PartyId <= 0 || updateParty == null)
                    return null;
                else
                    return _partyRepository.UpdateParty(PartyId, updateParty);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Delete the Party
        /// </summary>
        /// <param name="PartyId">PartyId</param>
        /// <returns>It return true, if party is delete successfully or else false</returns>
        public bool DeleteParty(int PartyId)
        {
            try
            {
                if (PartyId <= 0)
                    return false;
                else
                    return _partyRepository.DeleteParty(PartyId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
