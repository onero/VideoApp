using System;
using System.Collections.Generic;

namespace VideoAppDAL.Entities
{
    public class Video
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int GenreId { get; set; } = 1;
        public Genre Genre { get; set; }
        public double PricePerDay { get; set; } = 10;


        public List<Rental> Rentals { get; set; } = new List<Rental>();
    }
}