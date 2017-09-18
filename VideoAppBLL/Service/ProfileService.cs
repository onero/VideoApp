using System.Collections.Generic;
using System.Linq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Converters;
using VideoAppBLL.Interfaces;
using VideoAppDAL.Interfaces;

namespace VideoAppBLL.Service
{
    public class ProfileService : IProfileService
    {
        private readonly IDALFacade _facade;
        private readonly ProfileConverter _converter;
        public ProfileService(IDALFacade dalFacade)
        {
            _facade = dalFacade;
            _converter = new ProfileConverter();
        }

        public ProfileBO Create(ProfileBO profile)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var createdProfile = unitOfWork.ProfileRepository.Create(_converter.Convert(profile));
                unitOfWork.Complete();
                return _converter.Convert(createdProfile);
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
                return unitOfWork.ProfileRepository.GetAll().Select(_converter.Convert).ToList();
            }
        }

        public ProfileBO GetById(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var profile = unitOfWork.ProfileRepository.GetById(id);
                return profile == null ? null : _converter.Convert(profile);
            }
        }

        public List<ProfileBO> GetAllByIds(List<int> ids)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                if (ids == null) return null;
                return unitOfWork.ProfileRepository.
                    GetAllById(ids).
                    Select(_converter.Convert).ToList();
            }
        }

        public bool Delete(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var profileFromDB = unitOfWork.ProfileRepository.GetById(id);
                if (profileFromDB == null) return false;
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
                return _converter.Convert(profileFromDB);
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