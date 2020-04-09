using EmsBusinessLayer.Interface;
using EmsCommonLayer.Model;
using EmsRepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmsBusinessLayer.Services
{
    public class ConstituencyBusiness : IConstituencyBusiness
    {

        private readonly IConstituencyRepository _constituencyRepository;

        public ConstituencyBusiness(IConstituencyRepository constituencyRepository)
        {
            _constituencyRepository = constituencyRepository;
        }

        /// <summary>
        /// It add Constituency to the City in db
        /// </summary>
        /// <param name="addConstituency">Constituency Name and City Id</param>
        /// <returns>Add Constituency Response Model</returns>
        public AddConstituencyResponseModel AddConstituency(AddConstituencyRequestModel addConstituency)
        {
            try
            {
                if (addConstituency == null)
                    return null;
                else
                    return _constituencyRepository.AddConstituency(addConstituency);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It fetchs all the Constituency
        /// </summary>
        /// <returns>List of all Constituency</returns>
        public List<ConstituencyAddResponseModel> GetAllConstituency()
        {
            try
            {
                return _constituencyRepository.GetAllConstituency();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It fetch the single specified Constituency details
        /// </summary>
        /// <param name="ConstituencyId">Constituency Id</param>
        /// <returns>If Successfully fetch, It return ConstituencyAddResponseModel or else null</returns>
        public ConstituencyAddResponseModel GetConstituencyById(int ConstituencyId)
        {
            try
            {
                if (ConstituencyId <= 0)
                    return null;
                else
                    return _constituencyRepository.GetConstituencyById(ConstituencyId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It update the Constituency
        /// </summary>
        /// <param name="ConstituencyId">Constituency Id</param>
        /// <param name="updateConstituency">Update Constituency Name and City Id</param>
        /// <returns>UpdateConstituencyResponseModel</returns>
        public UpdateConstituencyResponseModel UpdateConstituency(int ConstituencyId, UpdateConstituencyRequestModel updateConstituency)
        {
            try
            {
                if (ConstituencyId <= 0 || updateConstituency == null)
                    return null;
                else
                    return _constituencyRepository.UpdateConstituency(ConstituencyId, updateConstituency);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// It Delete the Constituency
        /// </summary>
        /// <param name="ConstituencyId">Constituency Id</param>
        /// <returns>If Delete Successfully, It return true or else false</returns>
        public bool DeleteConstituency(int ConstituencyId)
        {
            try
            {
                if (ConstituencyId <= 0)
                    return false;
                else
                    return _constituencyRepository.DeleteConstituency(ConstituencyId);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
