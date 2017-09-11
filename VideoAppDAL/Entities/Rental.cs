using System;

namespace VideoAppDAL.Entities
{
    public class Rental
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }

    }
}