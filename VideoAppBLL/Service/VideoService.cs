using System;
using System.Collections.Generic;
using System.Linq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Converters;
using VideoAppBLL.Interfaces;
using VideoAppDAL.Interfaces;

namespace VideoAppBLL.Service
{
    internal class VideoService : IVideoService
    {
        private readonly IDALFacade _facade;
        private readonly VideoConverter _converter;

        public VideoService(IDALFacade facade)
        {
            _facade = facade;
            _converter = new VideoConverter();
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
                videoFromDB.Genre = unitOfWork.GenreRepository.GetById(videoFromDB.GenreId);
                return _converter.Convert(videoFromDB);
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

        public void ClearAll()
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                unitOfWork.VideoRepository.ClearAll();
                unitOfWork.Complete();
            }
        }
    }
}