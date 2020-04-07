using System;
using System.Collections.Generic;
using System.Text;

namespace EmsCommonLayer.Model
{

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
    /// Error Model
    /// </summary>
    public class ErrorResponseModel
    {

        public bool ErrorStatus { set; get; }

        public string Message { set; get; }
    }



}
