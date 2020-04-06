using EmsCommonLayer.Model;
using EmsRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace EmsRepositoryLayer.Services
{
    public class ElectionRepository : IElectionRepository
    {

        SqlConnection sqlConnection = new SqlConnection("Data Source=.;Initial Catalog=ElectionDB;Integrated Security=True");

        public List<PartyResponseModel> PartyWise(PartyWiseRequestModel partyRequest)
        {



            return null;
        }
    }
}
