using System;
using System.Collections.Generic;
using System.Text;

namespace EmsCommonLayer.Model
{

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
    /// It's the Response Model When a new party is created.
    /// </summary>
    public class CreatePartyResponseModel
    {

        public int PartyId { set; get; }

        public string Name { set; get; }

        public DateTime CreatedAt { set; get; }

        public DateTime ModifiedAt { set; get; }

    }

}
