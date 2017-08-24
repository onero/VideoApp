using System.Collections.Generic;

namespace VideoAppDAL
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Create(TEntity customerToCreate);

        IEnumerable<TEntity> GetAll();

        TEntity GetById(int id);

        bool Delete(int id);
    }
}