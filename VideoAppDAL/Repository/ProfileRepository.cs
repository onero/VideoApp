using System.Collections.Generic;
using System.Linq;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;

namespace VideoAppDAL.Repository
{
    public class ProfileRepository : IRepository<Profile>
    {
        private readonly IContext _context;

        public ProfileRepository(IContext context)
        {
            _context = context;
        }

        public Profile Create(Profile entity)
        {
            return _context.Profiles.Add(entity).Entity;
        }

        public IEnumerable<Profile> GetAll()
        {
            return new List<Profile>(_context.Profiles);
        }

        public Profile GetById(int id)
        {
            return _context.Profiles.FirstOrDefault(p => p.Id == id);
        }

        public bool Delete(int id)
        {
            var profile = GetById(id);
            if (profile == null) return false;
            _context.Profiles.Remove(profile);
            return true;
        }

        public Profile Update(Profile entity)
        {
            return _context.Profiles.Update(entity).Entity;
        }

        public void ClearAll()
        {
            foreach (var profile in _context.Profiles)
            {
                _context.Profiles.Remove(profile);
            }
        }
    }
}