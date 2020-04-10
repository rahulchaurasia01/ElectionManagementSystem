using System;
using System.Collections.Generic;
using System.Text;

namespace EmsCommonLayer.Model
{

    /// <summary>
    /// It's Model For adding State.
    /// </summary>
    public class AddStateRequestModel
    {
        public string Name { set; get; }
    }

    /// <summary>
    /// It's Model For Updating the State
    /// </summary>
    public class UpdateStateRequestModel
    {
        public string Name { set; get; }
    }

    /// <summary>
    /// It's a Model For Adding City to the State
    /// </summary>
    public class AddCityRequestModel
    {

        public string Name { set; get; }

        public int StateId { set; get; }

    }

    /// <summary>
    /// It's a model for updating city to the state
    /// </summary>
    public class UpdateCityRequestModel
    {
        public string Name { set; get; }

        public int StateId { set; get; }
    }

    /// <summary>
    /// It's a model for Creating a new Party
    /// </summary>
    public class CreatePartyRequestModel
    {

        public string Name { set; get; }

    }

    /// <summary>
    /// It's a Model for updating a party
    /// </summary>
    public class UpdatePartyRequestModel
    {
        public string Name { set; get; }
    }

    /// <summary>
    /// It's a Model for Adding Constituency to the City
    /// </summary>
    public class AddConstituencyRequestModel
    {
        public string Name { set; get; }

        public int CityId { set; get; }

    }

    /// <summary>
    /// It's a Model for Updating Constituency to the City
    /// </summary>
    public class UpdateConstituencyRequestModel
    {
        public string Name { set; get; }

        public int CityId { set; get; }
    }

    /// <summary>
    /// It's a Model for Adding Candidate
    /// </summary>
    public class AddCandidateRequestModel
    {
        public string Name { set; get; }

        public int ConstituencyId { set; get; }

        public int PartyId { set; get; }
    }

    /// <summary>
    /// It's a Model for updating Candidate
    /// </summary>
    public class UpdateCandidateRequestModel
    {
        public string Name { set; get; }

    }

    /// <summary>
    /// It's a model for PartyWise Election Result
    /// </summary>
    public class PartyWiseRequestModel
    {

        public string State { set; get; }

    }

}
