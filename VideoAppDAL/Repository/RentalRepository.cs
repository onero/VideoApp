using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;

namespace VideoAppDAL.Repository
{
    public class RentalRepository : Repository<Rental>
    {
        public RentalRepository(DbContext context) : base(context)
        {
        }
    }
}