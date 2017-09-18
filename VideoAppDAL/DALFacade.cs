using VideoAppDAL.Context;
using VideoAppDAL.Interfaces;

namespace VideoAppDAL
{
    public class DALFacade
    {
        public IUnitOfWork UnitOfWork => new UnitOfWork.UnitOfWork(new SQLContext());
    }
}