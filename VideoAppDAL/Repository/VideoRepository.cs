using System.Collections.Generic;
using VideoAppDAL.Context;
using VidepAppEntity;

namespace VideoAppDAL.Repository
{
    public class VideoRepository : IRepository<Video>
    {
        private InMemoryContext _context;

        public VideoRepository()
        {
            _context = new InMemoryContext();
        }
        public Video Create(Video customerToCreate)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Video> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Video GetById(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}