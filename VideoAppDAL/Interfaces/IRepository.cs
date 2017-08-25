using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using VidepAppEntity;

namespace VideoAppDAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Create(TEntity entity);

        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        bool Delete(int id);

        void ClearAll();
    }
}