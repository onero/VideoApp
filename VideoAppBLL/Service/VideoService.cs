using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Converters;
using VideoAppBLL.Interfaces;
using VideoAppDAL.Interfaces;

[assembly: InternalsVisibleTo("VideoAppBLLTests")]

namespace VideoAppBLL.Service
{
    internal class VideoService : IVideoService
    {
        private readonly VideoConverter _converter;
        private readonly IDALFacade _facade;
        private readonly GenreConverter _genreConverter;

        public VideoService(IDALFacade facade)
        {
            _facade = facade;
            _converter = new VideoConverter();
            _genreConverter = new GenreConverter();
        }

        public VideoBO Create(VideoBO video)
        {
            if (video == null) return null;
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var createdVideo = unitOfWork.VideoRepository.Create(_converter.Convert(video));
                unitOfWork.Complete();
                return _converter.Convert(createdVideo);
            }
        }

        public IList<VideoBO> CreateAll(IList<VideoBO> customers)
        {
            throw new NotImplementedException();
        }

        public IList<VideoBO> GetAll()
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                return unitOfWork.VideoRepository.GetAll().Select(_converter.Convert).ToList();
            }
        }

        public VideoBO GetById(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var videoFromDB = unitOfWork.VideoRepository.GetById(id);
                if (videoFromDB == null) return null;
                var convertedVideo = _converter.Convert(videoFromDB);

                // Get all genres for video
                convertedVideo.Genres = unitOfWork.GenreRepository.GetAllByIds(convertedVideo.GenreIds)
                    .Select(g => _genreConverter.Convert(g))
                    .ToList();
                return convertedVideo;
            }
        }

        public List<VideoBO> GetAllByIds(List<int> ids)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                return unitOfWork.VideoRepository.GetAllByIds(ids).Select(_converter.Convert).ToList();
            }
        }

        public bool Delete(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var videoFromDB = unitOfWork.VideoRepository.GetById(id);
                if (videoFromDB == null) return false;
                var videoDeleted = unitOfWork.VideoRepository.Delete(id);
                unitOfWork.Complete();
                return videoDeleted;
            }
        }

        public VideoBO Update(VideoBO entityToUpdate)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var videoFromRepo = unitOfWork.VideoRepository.GetById(entityToUpdate.Id);

                if (videoFromRepo == null) return null;

                videoFromRepo.Title = entityToUpdate.Title;
                var updatedVideo = unitOfWork.VideoRepository.Update(videoFromRepo);
                unitOfWork.Complete();
                return _converter.Convert(updatedVideo);
            }
        }
    }
}