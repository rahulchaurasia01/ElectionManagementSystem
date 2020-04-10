using EmsCommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Interface
{
    public interface ICandidateBusiness
    {

        AddCandidateResponseModel AddCandidate(AddCandidateRequestModel addCandidate);

        List<CandidateAddResponseModel> GetAllCandidate();

        CandidateAddResponseModel GetCandidateById(int CandidateId);

        UpdateCandidateResponseModel UpdateCandidate(int CandidateId, UpdateCandidateRequestModel updateCandidate);

        bool DeleteCandidate(int CandidateId);

    }
}
