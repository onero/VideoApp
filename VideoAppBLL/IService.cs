using System.Collections.Generic;

namespace VideoAppBLL
{
    public interface IService<TEntity> where TEntity : class
    {
        TEntity Create(TEntity entityToCreate);

        IList<TEntity> CreateAll(IList<TEntity> customers);

        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        bool Delete(int id);

        TEntity Update(TEntity entityToUpdate);
    }
}