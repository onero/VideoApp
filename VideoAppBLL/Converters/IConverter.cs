namespace VideoAppBLL.Converters
{
    public interface IConverter<TEntity, BOEntity> where TEntity : class
                                               where BOEntity : class
    {
        TEntity Convert(BOEntity entity);
        BOEntity Convert(TEntity entity);
    }
}