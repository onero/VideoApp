using VideoAppBLL.BusinessObjects;
using VideoAppDAL.Entities;

namespace VideoAppBLL.Converters
{
    internal static class GenreConverter
    {
        public static Genre Convert(GenreBO genre)
        {
            return new Genre()
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }

        public static GenreBO Convert(Genre genre)
        {
            return new GenreBO()
            {
                Id = genre.Id,
                Name = genre.Name
            };
        }
    }
}