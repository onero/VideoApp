using System.Collections.Generic;
using System.Linq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Converters;
using VideoAppBLL.Interfaces;
using VideoAppDAL.Interfaces;

namespace VideoAppBLL.Service
{
    public class RoleService : IRoleService
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
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var createdRole = unitOfWork.RoleRepository.Create(_converter.Convert(entityToCreate));
                unitOfWork.Complete();
                return _converter.Convert(createdRole);
            }
        }

        public IList<RoleBO> CreateAll(IList<RoleBO> customers)
        {
            throw new System.NotImplementedException();
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
                if (ids == null) return null;
                return unitOfWork.RoleRepository.
                    GetAllById(ids).
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
                var updatedRole = _converter.Convert(roleFromDB);
                updatedRole.Name = entityToUpdate.Name;
                return updatedRole;
            }
        }

        public void ClearAll()
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                unitOfWork.RoleRepository.ClearAll();
                unitOfWork.Complete();
            }
        }
    }
}