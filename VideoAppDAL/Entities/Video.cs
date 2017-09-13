using System;
using System.Collections.Generic;

namespace VideoAppDAL.Entities
{
    public class Video
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<Rental> Rentals { get; set; } = new List<Rental>();

        public int GenreId { get; set; }
        public Genre Genre { get; set; }
    }
}