using System;
using System.Collections.Generic;
using System.Linq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Converters;
using VideoAppBLL.Interfaces;
using VideoAppDAL.Interfaces;
using static VideoAppBLL.Converters.VideoConverter;

namespace VideoAppBLL.Service
{
    internal class VideoService : IService<VideoBO>
    {
        private readonly IDALFacade _facade;

        public VideoService(IDALFacade facade)
        {
            _facade = facade;
        }

        public VideoBO Create(VideoBO video)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var createdVideo = unitOfWork.VideoRepository.Create(Convert(video));
                unitOfWork.Complete();
                return Convert(createdVideo);
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
                var videos = unitOfWork.VideoRepository.GetAll().Select(Convert).ToList();
                return videos;
            }
        }

        public VideoBO GetById(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                return Convert(unitOfWork.VideoRepository.GetById(id));
            }
        }

        public bool Delete(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
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
                videoFromRepo.Genre = entityToUpdate.Genre;
                var updatedVideo = unitOfWork.VideoRepository.Update(videoFromRepo);
                unitOfWork.Complete();
                return Convert(updatedVideo);
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