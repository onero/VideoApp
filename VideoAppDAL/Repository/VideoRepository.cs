using System;
using System.Collections.Generic;
using System.Linq;
using VideoAppDAL.Context;
using VideoAppDAL.Entities;
using VideoAppDAL.Interfaces;

namespace VideoAppDAL.Repository
{
    internal class VideoRepository : IRepository<Video>
    {
        private readonly InMemoryContext _context;

        public VideoRepository(InMemoryContext context)
        {
            _context = context;
        }

        public Video Create(Video video)
        {
            var createdVideo = _context.Videos.Add(video);
            return createdVideo.Entity;
        }

        public Video Update(Video videoToUpdate)
        {
            var updatedVideo = _context.Videos.Update(videoToUpdate);
            return updatedVideo.Entity;
        }

        public void ClearAll()
        {
            foreach (var contextVideo in _context.Videos)
                _context.Videos.Remove(contextVideo);
        }

        public IEnumerable<Video> GetAll()
        {
            return new List<Video>(_context.Videos);
            ;
        }

        public Video GetById(int id)
        {
            var video = _context.Videos.FirstOrDefault(v => v.Id == id);
            return video;
        }

        public bool Delete(int id)
        {
            var video = GetById(id);
            if (video == null) return false;
            _context.Videos.Remove(video);
            return true;
        }
    }
}