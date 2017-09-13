using VideoAppBLL.BusinessObjects;
using VideoAppDAL.Entities;

namespace VideoAppBLL.Converters
{
    public class RentalConverter : IConverter<Rental, RentalBO>
    {
        public Rental Convert(RentalBO rental)
        {
            if (rental == null) return null;
            return new Rental()
            {
                Id = rental.Id,
                From = rental.From,
                To = rental.To,
                VideoId = rental.VideoId,
                UserId = rental.UserId
            };
        }

        public RentalBO Convert(Rental rental)
        {
            if (rental == null) return null;

            return new RentalBO()
            {
                Id = rental.Id,
                From = rental.From,
                To = rental.To,
                VideoId = rental.VideoId,
                Video = new VideoConverter().Convert(rental.Video),
                UserId = rental.UserId,
                User = new UserConverter().Convert(rental.User)
            };
        }
    }
}