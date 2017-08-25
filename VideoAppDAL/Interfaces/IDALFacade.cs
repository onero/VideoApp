namespace VideoAppDAL.Interfaces
{
    public interface IDALFacade
    {
        IUnitOfWork UnitOfWork { get; }
    }
}