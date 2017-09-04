using System.Collections.Generic;

namespace VideoAppBLL.Interfaces
{
    public interface IService<TEntity> where TEntity : class
    {
        TEntity Create(TEntity entityToCreate);

        IList<TEntity> CreateAll(IList<TEntity> customers);

        IList<TEntity> GetAll();

        TEntity GetById(int id);

        bool Delete(int id);

        TEntity Update(TEntity entityToUpdate);

        void ClearAll();
    }
}