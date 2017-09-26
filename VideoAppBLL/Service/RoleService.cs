using System.Collections.Generic;
using System.Linq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Converters;
using VideoAppBLL.Interfaces;
using VideoAppDAL.Interfaces;

namespace VideoAppBLL.Service
{
    internal class RoleService : IRoleService
    {
        private readonly IDALFacade _facade;
        private readonly RoleConverter _converter;

        public RoleService(IDALFacade facade)
        {
            _facade = facade;
            _converter = new RoleConverter();
        }

        public RoleBO Create(RoleBO entityToCreate)
        {
            if (entityToCreate == null) return null;
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var createdRole = unitOfWork.RoleRepository.Create(_converter.Convert(entityToCreate));
                unitOfWork.Complete();
                return _converter.Convert(createdRole);
            }
        }

        public IList<RoleBO> GetAll()
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                return unitOfWork.RoleRepository.GetAll().Select(new RoleConverter().Convert).ToList();
            }
        }

        public RoleBO GetById(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var roleFromDB = unitOfWork.RoleRepository.GetById(id);
                if (roleFromDB == null) return null;
                return _converter.Convert(roleFromDB);
            }
        }

        public List<RoleBO> GetAllByIds(List<int> ids)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                return unitOfWork.RoleRepository.
                    GetAllByIds(ids).
                    Select(_converter.Convert).ToList();
            }
        }

        public bool Delete(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var roleFromDB = unitOfWork.RoleRepository.GetById(id);
                if (roleFromDB == null) return false;
                return unitOfWork.RoleRepository.Delete(id);
            }
        }

        public RoleBO Update(RoleBO entityToUpdate)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var roleFromDB = unitOfWork.RoleRepository.GetById(entityToUpdate.Id);
                if (roleFromDB == null) return null;
                roleFromDB.Name = entityToUpdate.Name;
                unitOfWork.RoleRepository.Update(roleFromDB);
                unitOfWork.Complete();
                return _converter.Convert(roleFromDB);
            }
        }
    }
}