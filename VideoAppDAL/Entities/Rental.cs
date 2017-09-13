using System;

namespace VideoAppDAL.Entities
{
    public class Rental
    {
        public int Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int VideoId { get; set; }
        public Video Video { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}