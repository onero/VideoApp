using System.Linq;
using VideoAppBLL.BusinessObjects;
using VideoAppDAL.Entities;

namespace VideoAppBLL.Converters
{
    internal class VideoConverter : IConverter<Video, VideoBO>
    {
        /// <summary>
        /// Convert Video to VideoBO
        /// </summary>
        /// <param name="video"></param>
        /// <returns>VideoBO</returns>
        public VideoBO Convert(Video video)
        {
            if (video == null) return null;
            return new VideoBO()
            {
                Id = video.Id,
                Title = video.Title,
                GenreIds = video.Genres?.Select(g => g.GenreId).ToList(),
                PricePerDay = video.PricePerDay
            };
        }

        /// <summary>
        /// Convert VideoBO to VIDEO
        /// </summary>
        /// <param name="video"></param>
        /// <returns>Video</returns>
        public Video Convert(VideoBO video)
        {
            if (video == null) return null;

            return new Video()
            {
                Id = video.Id,
                Title = video.Title,
                Genres = video.GenreIds?.Select(gId => new VideoGenre()
                {
                    VideoId = video.Id,
                    GenreId = gId
                }).ToList(),
                PricePerDay = video.PricePerDay
            };
        }
    }
}