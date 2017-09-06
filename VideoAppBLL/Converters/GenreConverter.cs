using VideoAppBLL.BusinessObjects;
using VideoAppDAL.Entities;

namespace VideoAppBLL.Converters
{
    internal static class GenreConverter
    {
        public static Genre Convert(GenreBO genre)
        {
            return (Genre) genre;
        }

        public static GenreBO Convert(Genre genre)
        {
            return (GenreBO)genre;
        }
    }
}