using VideoAppDAL.Context;
using VideoAppDAL.Interfaces;
using VideoAppDAL.UnitOfWork;

namespace VideoAppDAL
{
    public class DALFacadeMem : IDALFacade
    {
        public IUnitOfWork UnitOfWork => new UnitOfWorkMem(new InMemoryContext());
    }
}