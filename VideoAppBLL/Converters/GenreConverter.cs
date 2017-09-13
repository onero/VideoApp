using VideoAppBLL.BusinessObjects;
using VideoAppDAL.Entities;

namespace VideoAppBLL.Converters
{
    public class GenreConverter : IConverter<Genre, GenreBO>
    {
        public Genre Convert(GenreBO entity)
        {
            return new Genre()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public GenreBO Convert(Genre entity)
        {
            return new GenreBO()
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }
    }
}