using VideoAppBLL.BusinessObjects;
using VideoAppDAL.Entities;

namespace VideoAppBLL.Converters
{
    public class RentalConverter : IConverter<Rental, RentalBO>
    {
        public Rental Convert(RentalBO entity)
        {
            return new Rental()
            {
                Id = entity.Id,
                From = entity.From,
                To = entity.To
            };
        }

        public RentalBO Convert(Rental entity)
        {
            return new RentalBO()
            {
                Id = entity.Id,
                From = entity.From,
                To = entity.To
            };
        }
    }
}