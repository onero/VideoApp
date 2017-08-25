namespace VideoAppDAL
{
    public class DALFacade
    {
        public IUnitOfWork UnitOfWork => new UnitOfWork.UnitOfWorkMem();
    }
}