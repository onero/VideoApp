﻿using System.Collections.Generic;
using System.Linq;
using VideoAppBLL.BusinessObjects;
using VideoAppBLL.Converters;
using VideoAppBLL.Interfaces;
using VideoAppDAL.Interfaces;

namespace VideoAppBLL.Service
{
    public class RentalService : IRentalService
    {
        private readonly IDALFacade _facade;
        private readonly RentalConverter _converter;
        public RentalService(IDALFacade dalFacade)
        {
            _facade = dalFacade;
            _converter = new RentalConverter();
        }
        public RentalBO Create(RentalBO rental)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var createdRental = unitOfWork.RentalRepository.Create(_converter.Convert(rental));
                unitOfWork.Complete();
                return _converter.Convert(createdRental);
            }
        }

        public IList<RentalBO> CreateAll(IList<RentalBO> customers)
        {
            throw new System.NotImplementedException();
        }

        public IList<RentalBO> GetAll()
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                return unitOfWork.RentalRepository.GetAll().Select(_converter.Convert).ToList();
            }
        }

        public RentalBO GetById(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                // Get rental
                var rental = unitOfWork.RentalRepository.GetById(id);
                if (rental == null) return null;
                // Get video from rental
                var video = unitOfWork.VideoRepository.GetById(rental.VideoId);
                if (video == null) return _converter.Convert(rental);

                rental.Video = video;
                return _converter.Convert(rental);
            }
        }

        public bool Delete(int id)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var rental = unitOfWork.RentalRepository.GetById(id);
                if (rental == null) return false;
                var deleted = unitOfWork.RentalRepository.Delete(id);
                unitOfWork.Complete();
                return deleted;
            }
        }

        public RentalBO Update(RentalBO rentalToUpdate)
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                var rental = unitOfWork.RentalRepository.GetById(rentalToUpdate.Id);
                if (rental == null) return null;

                rental.From = rentalToUpdate.From;
                rental.To = rentalToUpdate.To;
                unitOfWork.RentalRepository.Update(rental);
                return _converter.Convert(rental);
            }
        }

        public void ClearAll()
        {
            using (var unitOfWork = _facade.UnitOfWork)
            {
                unitOfWork.RentalRepository.ClearAll();
                unitOfWork.Complete();
            }
        }
    }
}