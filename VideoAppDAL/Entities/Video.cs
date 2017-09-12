using System;
using System.Collections.Generic;

namespace VideoAppDAL.Entities
{
    public class Video
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Genre Genre { get; set; } = Genre.Action;

        public List<Rental> Rentals { get; set; }
    }
}