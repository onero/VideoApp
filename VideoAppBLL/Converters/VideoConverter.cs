using VideoAppBLL.BusinessObjects;
using VideoAppDAL.Entities;

namespace VideoAppBLL.Converters
{
    internal static class VideoConverter
    {
        /// <summary>
        /// Convert Video to VideoBO
        /// </summary>
        /// <param name="video"></param>
        /// <returns>VideoBO</returns>
        public static VideoBO Convert(Video video)
        {
            return new VideoBO()
            {
                Id = video.Id,
                Title = video.Title,
                Genre = video.Genre
            };
        }

        /// <summary>
        /// Convert VideoBO to VIDEO
        /// </summary>
        /// <param name="video"></param>
        /// <returns>Video</returns>
        public static Video Convert(VideoBO video)
        {
            return new Video()
            {
                Id = video.Id,
                Title = video.Title,
                Genre = video.Genre
            };
        }
    }
}