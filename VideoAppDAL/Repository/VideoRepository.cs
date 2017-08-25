using System.Collections.Generic;
using VideoAppDAL.Context;
using VideoAppDAL.Interfaces;
using VidepAppEntity;

namespace VideoAppDAL.Repository
{
    public class VideoRepository : IRepository<Video>
    {
        private readonly InMemoryContext _context;

        public VideoRepository(InMemoryContext context)
        {
            _context = context;
        }
        public Video Create(Video video)
        {
            _context.Videos.Add(video);
            return video;
        }

        public void ClearAll()
        {
            foreach (var contextVideo in _context.Videos)
            {
                _context.Videos.Remove(contextVideo);
            }
        }

        public IEnumerable<Video> GetAll()
        {
            var videos = new List<Video>(_context.Videos);
            return videos;
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