using System.Collections.Generic;

namespace VideoAppDAL.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<VideoGenre> Videos { get; set; }
    }
}