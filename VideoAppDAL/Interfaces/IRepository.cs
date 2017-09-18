using System.Collections.Generic;

namespace VideoAppDAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Create(TEntity entity);

        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        List<TEntity> GetAllById(List<int> ids);

        bool Delete(int id);

        TEntity Update(TEntity entity);

        void ClearAll();
    }
}