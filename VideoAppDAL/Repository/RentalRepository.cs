using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;
using System.Linq;

namespace VideoAppDAL.Repository
{
    public class RentalRepository : ARepository<Rental>
    {
        public RentalRepository(DbContext context) : base(context)
        {
        }

        public override List<Rental> GetAllById(List<int> ids)
        {
            return ids == null ?
                null :
                Context.Set<Rental>().Where(g => ids.Contains(g.Id)).ToList();
        }
    }
}