using System.Collections.Generic;
using System.Linq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Interfaces;
using VideoAppDAL.Interfaces;
using static VideoAppBLL.Converters.ProfileConverter;

namespace VideoAppBLL.Service
{
    public class ProfileService : IService<ProfileBO>
    {
        private readonly IDALFacade _facade;
        public ProfileService(IDALFacade dalFacade)
        {
            _facade = dalFacade;
        }

        public ProfileBO Create(ProfileBO entityToCreate)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var createdProfile = unitOfWork.ProfileRepository.Create(Convert(entityToCreate));
                unitOfWork.Complete();
                return Convert(createdProfile);
            }
        }


        public IList<ProfileBO> CreateAll(IList<ProfileBO> customers)
        {
            throw new System.NotImplementedException();
        }

        public IList<ProfileBO> GetAll()
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                return unitOfWork.ProfileRepository.GetAll().Select(Convert).ToList();
            }
        }

        public ProfileBO GetById(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var profile = unitOfWork.ProfileRepository.GetById(id);
                return profile == null ? null : Convert(profile);
            }
        }

        public bool Delete(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var deleted = unitOfWork.ProfileRepository.Delete(id);
                unitOfWork.Complete();
                return deleted;
            }
        }

        public ProfileBO Update(ProfileBO entityToUpdate)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var profileFromDB = unitOfWork.ProfileRepository.GetById(entityToUpdate.Id);
                if (profileFromDB == null) return null;

                profileFromDB.FirstName = entityToUpdate.FirstName;
                profileFromDB.LastName = entityToUpdate.LastName;
                profileFromDB.Address = entityToUpdate.Address;
                unitOfWork.ProfileRepository.Update(profileFromDB);
                unitOfWork.Complete();
                return Convert(profileFromDB);
            }
        }

        public void ClearAll()
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                unitOfWork.ProfileRepository.ClearAll();
                unitOfWork.Complete();
            }
        }
    }
}