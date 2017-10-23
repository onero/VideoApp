using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VideoAppBLL.BusinessObjects
{
    public class VideoBO : IComparable<VideoBO>
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Title { get; set; }

        public string Img { get; set; }

        public List<int> GenreIds { get; set; }

        public List<GenreBO> Genres { get; set; }
        public double PricePerDay { get; set; } = 10;

        public List<RentalBO> Rentals { get; set; } = new List<RentalBO>();

        public int CompareTo(VideoBO other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var idComparison = Id.CompareTo(other.Id);
            if (idComparison != 0) return idComparison;
            var titleComparison = string.Compare(Title, other.Title, StringComparison.Ordinal);
            if (titleComparison != 0) return titleComparison;
            return PricePerDay.CompareTo(other.PricePerDay);
        }
    }
}