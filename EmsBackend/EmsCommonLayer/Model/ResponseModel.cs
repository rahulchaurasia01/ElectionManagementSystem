using System;
using System.Collections.Generic;
using System.Text;

namespace EmsCommonLayer.Model
{
    /// <summary>
    /// It's a response Model when a State is Added
    /// </summary>
    public class AddStateResponseModel
    {
        public StateAddResponseModel StateAdd { set; get; }

        public ErrorResponseModel ErrorResponse { set; get; }
    }

    /// <summary>
    /// It's a response Model when a state is Updated.
    /// </summary>
    public class UpdateStateResponseModel
    {
        public StateUpdateResponseModel StateUpdate { set; get; }

        public ErrorResponseModel ErrorResponse { set; get; }
    }

    /// <summary>
    /// Added State Response Model
    /// </summary>
    public class StateAddResponseModel
    {
        public int StateId { set; get; }

        public string Name { set; get; }

        public DateTime CreatedAt { set; get; }

        public DateTime ModifiedAt { set; get; }
    }

    /// <summary>
    /// Updated State Response Model
    /// </summary>
    public class StateUpdateResponseModel
    {
        public int StateId { set; get; }

        public string Name { set; get; }

        public DateTime CreatedAt { set; get; }

        public DateTime ModifiedAt { set; get; }
    }

    /// <summary>
    /// It's a response Model When City is Added
    /// </summary>
    public class AddCityResponseModel
    {
        public CityAddResponseModel CityAdd { set; get; }

        public ErrorResponseModel ErrorResponse { set; get; }
    }

    /// <summary>
    /// It's a response Model when city is Updated
    /// </summary>
    public class UpdateCityResponseModel
    {

        public CityUpdateResponseModel CityUpdate { set; get; }

        public ErrorResponseModel ErrorResponse { set; get; }

    }

    /// <summary>
    /// Added City Response Model
    /// </summary>
    public class CityAddResponseModel
    {

        public int CityId { set; get; }

        public string Name { set; get; }

        public int StateId { set; get; }

        public DateTime CreatedAt { set; get; }

        public DateTime ModifiedAt { set; get; }

    }

    /// <summary>
    /// Updated City Response Model
    /// </summary>
    public class CityUpdateResponseModel
    {
        public int CityId { set; get; }

        public string Name { set; get; }

        public int StateId { set; get; }

        public DateTime CreatedAt { set; get; }

        public DateTime ModifiedAt { set; get; }
    }

    /// <summary>
    /// It's the Response Model When a new party is created.
    /// </summary>
    public class CreatePartyResponseModel
    {

        public PartyCreatedResponseModel PartyCreated { set; get; }

        public ErrorResponseModel ErrorResponse { set; get; }

    }

    /// <summary>
    /// It's the response model when party Name is updated
    /// </summary>
    public class UpdatepartyResponseModel
    {
        public PartyUpdatedResponseModel PartyUpdated { set; get; }

        public ErrorResponseModel ErrorResponse { set; get; }
    }

    /// <summary>
    /// It's the Response Model For viewing party wise election result.
    /// </summary>
    public class PartyResponseModel
    {

        public string Name { set; get; }

        public int Won { set; get; }

        public int Leading { set; get; }

    }

    /// <summary>
    /// new Party Created Response Model
    /// </summary>
    public class PartyCreatedResponseModel
    {
        public int PartyId { set; get; }

        public string Name { set; get; }

        public DateTime CreatedAt { set; get; }

        public DateTime ModifiedAt { set; get; }
    }

    /// <summary>
    /// updated Party Response Model
    /// </summary>
    public class PartyUpdatedResponseModel
    {
        public int PartyId { set; get; }

        public string Name { set; get; }

        public DateTime CreatedAt { set; get; }

        public DateTime ModifiedAt { set; get; }
    }

    /// <summary>
    /// It's a response Model When Constituency is Added
    /// </summary>
    public class AddConstituencyResponseModel
    {
        public ConstituencyAddResponseModel ConstituencyAdd { set; get; }

        public ErrorResponseModel ErrorResponse { set; get; }
    }

    /// <summary>
    /// It's a response Model when Constituency is Updated
    /// </summary>
    public class UpdateConstituencyResponseModel
    {

        public ConstituencyUpdateResponseModel ConstituencyUpdate { set; get; }

        public ErrorResponseModel ErrorResponse { set; get; }

    }

    /// <summary>
    /// Added Constituency Response Model
    /// </summary>
    public class ConstituencyAddResponseModel
    {

        public int ConstituencyId { set; get; }

        public string Name { set; get; }

        public int CityId { set; get; }

        public DateTime CreatedAt { set; get; }

        public DateTime ModifiedAt { set; get; }

    }

    /// <summary>
    /// Updated Constituency Response Model
    /// </summary>
    public class ConstituencyUpdateResponseModel
    {
        public int ConstituencyId { set; get; }

        public string Name { set; get; }

        public int CityId { set; get; }

        public DateTime CreatedAt { set; get; }

        public DateTime ModifiedAt { set; get; }
    }

    /// <summary>
    /// It's a response Model when Candidae is added
    /// </summary>
    public class AddCandidateResponseModel
    {
        public CandidateAddResponseModel CandidateAdd { set; get; }

        public ErrorResponseModel ErrorResponse { set; get; }
    }

    /// <summary>
    /// It's a response Model When Candidate is Updated
    /// </summary>
    public class UpdateCandidateResponseModel
    {
        public CandidateUpdateResponseModel CandidateUpdate { set; get; }

        public ErrorResponseModel ErrorResponse { set; get; }
    }

    /// <summary>
    /// Added Candidate Response Model
    /// </summary>
    public class CandidateAddResponseModel
    {
        public int CandidateId { set; get; }

        public string Name { set; get; }

        public int ConstituencyId { set; get; }

        public int PartyId { set; get; }

        public DateTime CreatedAt { set; get; }

        public DateTime ModifiedAt { set; get; }
    }

    /// <summary>
    /// Updated Candidate Response Model
    /// </summary>
    public class CandidateUpdateResponseModel
    {
        public int CandidateId { set; get; }

        public string Name { set; get; }

        public int ConstituencyId { set; get; }

        public int PartyId { set; get; }

        public DateTime CreatedAt { set; get; }

        public DateTime ModifiedAt { set; get; }
    }

    /// <summary>
    /// It's a response Model for when Vote is Added
    /// </summary>
    public class AddVoteResponseModel
    {
        public VoteAddResponseModel VoteAdd { set; get; }

        public ErrorResponseModel ErrorResponse { set; get; }
    }

    /// <summary>
    /// Vote Add Response Model
    /// </summary>
    public class VoteAddResponseModel
    {
        public int VotesId { set; get; }

        public int CandidateId { set; get; }

        public bool EvmVote { set; get; }

        public bool PostalVote { set; get; }

        public DateTime CreatedAt { set; get; }

        public DateTime ModifiedAt { set; get; }
    }

    /// <summary>
    /// Error Model
    /// </summary>
    public class ErrorResponseModel
    {

        public bool ErrorStatus { set; get; }

        public string Message { set; get; }
    }



}
