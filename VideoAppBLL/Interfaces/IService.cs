using System.Collections.Generic;

namespace VideoAppBLL.Interfaces
{
    public interface IService<TEntity>
    {
        TEntity Create(TEntity entityToCreate);

        IList<TEntity> GetAll();

        TEntity GetById(int id);

        List<TEntity> GetAllByIds(List<int> ids);

        bool Delete(int id);

        TEntity Update(TEntity entityToUpdate);
    }
}