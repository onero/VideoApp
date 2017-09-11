using System.Collections.Generic;
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
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public RentalBO Update(RentalBO entityToUpdate)
        {
            throw new System.NotImplementedException();
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