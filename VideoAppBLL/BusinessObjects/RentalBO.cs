using System;
using System.ComponentModel.DataAnnotations;

namespace VideoAppBLL.BusinessObjects
{
    public class RentalBO : IComparable<RentalBO>
    {
        public int Id { get; set; }
        public DateTime From { get; set; } = DateTime.Now;
        public DateTime To { get; set; } = DateTime.Now.AddDays(7);
        [Required]
        public int VideoId { get; set; }
        public VideoBO Video { get; set; }
        [Required]
        public int UserId { get; set; }
        public UserBO User { get; set; }

        public int CompareTo(RentalBO other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var idComparison = Id.CompareTo(other.Id);
            if (idComparison != 0) return idComparison;
            var fromComparison = From.CompareTo(other.From);
            if (fromComparison != 0) return fromComparison;
            return To.CompareTo(other.To);
        }
    }
}