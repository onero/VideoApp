using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;
using System.Linq;

namespace VideoAppDAL.Repository
{
    internal class RentalRepository : ARepository<Rental>
    {
        public RentalRepository(DbContext context) : base(context)
        {
        }

        public override List<Rental> GetAllByIds(List<int> ids)
        {
            return ids == null ?
                null :
                Context.Set<Rental>().Where(g => ids.Contains(g.Id)).ToList();
        }

        public override Rental Update(Rental entity)
        {
            var entityFromDb = GetById(entity.Id);
            if (entityFromDb == null) return null;
            return Context.Update(entity).Entity;
        }
    }
}