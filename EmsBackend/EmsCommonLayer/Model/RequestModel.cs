using System;
using System.Collections.Generic;
using System.Text;

namespace EmsCommonLayer.Model
{

    /// <summary>
    /// It's a model for PartyWise Election Result
    /// </summary>
    public class PartyWiseRequestModel
    {

        public string State { set; get; }

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

}
