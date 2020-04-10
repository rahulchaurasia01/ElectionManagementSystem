using EmsBusinessLayer.Interface;
using EmsCommonLayer.Model;
using EmsRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Services
{
    public class VoteBusiness : IVoteBusiness
    {

        private readonly IVoteRepository _voteRepository;

        public VoteBusiness(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        /// <summary>
        /// It Add Vote to the candidate
        /// </summary>
        /// <param name="addVote">Candidate Id and Evmvote or postalvote</param>
        /// <returns>Add Vote Response Model</returns>
        public AddVoteResponseModel AddVote(AddVoteRequestModel addVote)
        {
            try
            {
                if (addVote == null || (addVote.EvmVote == addVote.PostalVote))
                    return null;
                else
                    return _voteRepository.AddVote(addVote);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Delete the vote
        /// </summary>
        /// <param name="VotesId">Votes Id</param>
        /// <returns>If deleted successfull, It return true or else false</returns>
        public bool DeleteVote(int VotesId)
        {
            try
            {
                if (VotesId <= 0)
                    return false;
                else
                    return _voteRepository.DeleteVote(VotesId);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
