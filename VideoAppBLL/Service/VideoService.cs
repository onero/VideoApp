using System.Collections.Generic;
using VideoAppDAL;
using VidepAppEntity;

namespace VideoAppBLL.Service
{
    public class VideoService : IService<Video>
    {
        private DALFacade _facade;

        public VideoService(DALFacade facade)
        {
            _facade = facade;
        }

        public Video Create(Video entityToCreate)
        {
            throw new System.NotImplementedException();
        }

        public IList<Video> CreateAll(IList<Video> customers)
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

        public Video Update(Video entityToUpdate)
        {
            throw new System.NotImplementedException();
        }
    }
}