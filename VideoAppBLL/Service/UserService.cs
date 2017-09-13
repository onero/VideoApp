using System.Collections.Generic;
using System.Linq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Converters;
using VideoAppBLL.Interfaces;
using VideoAppDAL.Interfaces;

namespace VideoAppBLL.Service
{
    public class UserService : IUserService
    {
        private readonly IDALFacade _facade;
        private readonly UserConverter _converter;

        public UserService(IDALFacade facade)
        {
            _facade = facade;
            _converter = new UserConverter();
        }

        public UserBO Create(UserBO user)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var createdUser = unitOfWork.UserRepository.Create(_converter.Convert(user));
                unitOfWork.Complete();
                return _converter.Convert(createdUser);
            }
        }

        public IList<UserBO> CreateAll(IList<UserBO> customers)
        {
            throw new System.NotImplementedException();
        }

        public IList<UserBO> GetAll()
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                return unitOfWork.UserRepository.GetAll().Select(_converter.Convert).ToList();
            }
        }

        public UserBO GetById(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var videoFromDB = unitOfWork.UserRepository.GetById(id);
                if (videoFromDB == null) return null;
                return _converter.Convert(videoFromDB);
            }
        }

        public bool Delete(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var userFromDB = unitOfWork.UserRepository.GetById(id);
                if (userFromDB == null) return false;
                var deleted = unitOfWork.UserRepository.Delete(id);
                unitOfWork.Complete();
                return deleted;
            }
        }

        public UserBO Update(UserBO entityToUpdate)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var userFromRepo = unitOfWork.UserRepository.GetById(entityToUpdate.Id);

                if (userFromRepo == null) return null;

                userFromRepo.Username = entityToUpdate.Username;
                userFromRepo.Password = entityToUpdate.Password;
                var updatedVideo = unitOfWork.UserRepository.Update(userFromRepo);
                unitOfWork.Complete();
                return _converter.Convert(updatedVideo);
            }
        }

        public void ClearAll()
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                unitOfWork.UserRepository.ClearAll();
                unitOfWork.Complete();
            }
        }
    }
}