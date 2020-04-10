using EmsCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Interface
{
    public interface IVoteBusiness
    {

        AddVoteResponseModel AddVote(AddVoteRequestModel addVote);

        bool DeleteVote(int VotesId);

    }
}
