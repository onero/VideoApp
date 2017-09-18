using System;
using System.Collections.Generic;

namespace VideoAppDAL.Entities
{
    public class Video
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public List<VideoGenre> Genres { get; set; }

        public double PricePerDay { get; set; } = 10;


        public List<Rental> Rentals { get; set; } = new List<Rental>();
        
    }
}