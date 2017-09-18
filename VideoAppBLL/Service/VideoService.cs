using System;
using System.Collections.Generic;
using System.Linq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Converters;
using VideoAppBLL.Interfaces;
using VideoAppDAL;
using VideoAppDAL.Interfaces;

namespace VideoAppBLL.Service
{
    internal class VideoService : IVideoService
    {
        private readonly DALFacade _facade;
        private readonly VideoConverter _converter;
        private readonly GenreConverter _genreConverter;

        public VideoService(DALFacade facade)
        {
            _facade = facade;
            _converter = new VideoConverter();
            _genreConverter = new GenreConverter();
        }

        public VideoBO Create(VideoBO video)
        {
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
                if (convertedVideo.GenreIds == null) return convertedVideo;

                // Get all genres for video
                convertedVideo.Genres = unitOfWork.GenreRepository.
                    GetAllByIds(convertedVideo.GenreIds)
                    .Select(g => _genreConverter.Convert(g))
                    .ToList();
                return convertedVideo;
            }
        }

        public List<VideoBO> GetAllByIds(List<int> ids)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                if (ids == null) return null;
                return unitOfWork.VideoRepository?.
                    GetAllByIds(ids).
                    Select(_converter.Convert).ToList();
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