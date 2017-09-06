using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;

namespace VideoAppDAL.Repository
{
    public class ProfileRepository : Repository<Profile>
    {
        public ProfileRepository(DbContext context) : base(context)
        {
        }
    }
}