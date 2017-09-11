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
            return new VideoBO()
            {
                Id = video.Id,
                Title = video.Title,
                Genre = GenreConverter.Convert(video.Genre)
            };
        }

        /// <summary>
        /// Convert VideoBO to VIDEO
        /// </summary>
        /// <param name="video"></param>
        /// <returns>Video</returns>
        public Video Convert(VideoBO video)
        {
            return new Video()
            {
                Id = video.Id,
                Title = video.Title,
                Genre = GenreConverter.Convert(video.Genre)
            };
        }
    }
}