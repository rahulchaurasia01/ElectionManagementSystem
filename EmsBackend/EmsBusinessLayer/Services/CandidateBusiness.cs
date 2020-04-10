using EmsBusinessLayer.Interface;
using EmsCommonLayer.Model;
using EmsRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Services
{
    public class CandidateBusiness : ICandidateBusiness
    {

        private readonly ICandidateRepository _candidateRepository;

        public CandidateBusiness(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }

        /// <summary>
        /// It add Candidate
        /// </summary>
        /// <param name="addCandidate">Candidate Name and Constituency Id and PartyId</param>
        /// <returns>AddCandidateResponseModel</returns>
        public AddCandidateResponseModel AddCandidate(AddCandidateRequestModel addCandidate)
        {
            try
            {
                if (addCandidate == null)
                    return null;
                else
                    return _candidateRepository.AddCandidate(addCandidate);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It fetchs all the Candidates
        /// </summary>
        /// <returns>List of all Candidates</returns>
        public List<CandidateAddResponseModel> GetAllCandidate()
        {
            try
            {
                return _candidateRepository.GetAllCandidate();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It fetch the single specified Candidate details
        /// </summary>
        /// <param name="CandidateId">Candidate Id</param>
        /// <returns>Candidate Add Response Model</returns>
        public CandidateAddResponseModel GetCandidateById(int CandidateId)
        {
            try
            {
                if (CandidateId <= 0)
                    return null;
                else
                    return _candidateRepository.GetCandidateById(CandidateId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It update the Candidate
        /// </summary>
        /// <param name="CandidateId">Candidate Id</param>
        /// <param name="updateCandidate">Update Candidate Name and ConstituecyId and PartyId</param>
        /// <returns>Update Candidate Response Model</returns>
        public UpdateCandidateResponseModel UpdateCandidate(int CandidateId, UpdateCandidateRequestModel updateCandidate)
        {
            try
            {
                if (CandidateId <= 0 || updateCandidate == null)
                    return null;
                else
                    return _candidateRepository.UpdateCandidate(CandidateId, updateCandidate);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Delete the Candidate
        /// </summary>
        /// <param name="CandidateId">Candidate</param>
        /// <returns>If Delete Successfully, It return true or else false</returns>
        public bool DeleteCandidate(int CandidateId)
        {
            try
            {
                if (CandidateId <= 0)
                    return false;
                else
                    return _candidateRepository.DeleteCandidate(CandidateId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
