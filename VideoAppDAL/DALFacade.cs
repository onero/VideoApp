using VideoAppDAL.Context;
using VideoAppDAL.Interfaces;

namespace VideoAppDAL
{
    public class DALFacade : IDALFacade
    {
        public IUnitOfWork UnitOfWork => new UnitOfWork.UnitOfWork(new InMemoryContext());
    }
}