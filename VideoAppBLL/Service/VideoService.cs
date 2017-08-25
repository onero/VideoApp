using System;
using System.Collections.Generic;
using VideoAppDAL;
using VideoAppDAL.Interfaces;
using VidepAppEntity;

namespace VideoAppBLL.Service
{
    internal class VideoService : IService<Video>
    {
        private readonly IDALFacade _facade;

        public VideoService(IDALFacade facade)
        {
            _facade = facade;
        }

        public Video Create(Video video)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var createdVideo = unitOfWork.VideoRepository.Create(video);
                unitOfWork.Complete();
                return createdVideo;
            }
        }

        public IList<Video> CreateAll(IList<Video> customers)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Video> GetAll()
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var videos = unitOfWork.VideoRepository.GetAll();
                return videos;
            }
        }

        public Video GetById(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                return unitOfWork.VideoRepository.GetById(id);
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

        public Video Update(Video entityToUpdate)
        {
            throw new NotImplementedException();
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