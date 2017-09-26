using System.Collections.Generic;
using System.Linq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Converters;
using VideoAppBLL.Interfaces;
using VideoAppDAL;
using VideoAppDAL.Interfaces;

namespace VideoAppBLL.Service
{
    internal class GenreService : IGenreService
    {
        private readonly GenreConverter _converter;
        private readonly IDALFacade _facade;

        public GenreService(IDALFacade facade)
        {
            _converter = new GenreConverter();
            _facade = facade;
        }

        public GenreBO Create(GenreBO genre)
        {
            if (genre == null) return null;
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var createdGenre = unitOfWork.GenreRepository.Create(_converter.Convert(genre));
                unitOfWork.Complete();
                return _converter.Convert(createdGenre);
            }
        }

        public IList<GenreBO> GetAll()
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                return unitOfWork.GenreRepository.GetAll().Select(_converter.Convert).ToList();
            }
        }

        public GenreBO GetById(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var videoFromDB = unitOfWork.GenreRepository.GetById(id);
                if (videoFromDB == null) return null;
                return _converter.Convert(videoFromDB);
            }
        }

        public List<GenreBO> GetAllByIds(List<int> ids)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                return unitOfWork.GenreRepository.GetAllByIds(ids).Select(_converter.Convert).ToList();
            }
        }

        public bool Delete(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var genreFromDB = unitOfWork.GenreRepository.GetById(id);
                if (genreFromDB == null) return false;
                var videoDeleted = unitOfWork.GenreRepository.Delete(id);
                unitOfWork.Complete();
                return videoDeleted;
            }
        }

        public GenreBO Update(GenreBO entityToUpdate)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var genreFromRepo = unitOfWork.GenreRepository.GetById(entityToUpdate.Id);

                if (genreFromRepo == null) return null;

                genreFromRepo.Name = entityToUpdate.Name;
                var updatedVideo = unitOfWork.GenreRepository.Update(genreFromRepo);
                unitOfWork.Complete();
                return _converter.Convert(updatedVideo);
            }
        }
    }
}