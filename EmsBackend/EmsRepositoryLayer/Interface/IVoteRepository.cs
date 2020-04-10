using EmsCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsRepositoryLayer.Interface
{
    public interface IVoteRepository
    {

        AddVoteResponseModel AddVote(AddVoteRequestModel addVote);

        bool DeleteVote(int VotesId);

    }
}
