using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VideoAppBLL.BusinessObjects
{
    public class VideoBO : IComparable<VideoBO>
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int GenreId { get; set; } = 1;
        public GenreBO Genre { get; set; }
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
            var genreIdComparison = GenreId.CompareTo(other.GenreId);
            if (genreIdComparison != 0) return genreIdComparison;
            var genreComparison = Comparer<GenreBO>.Default.Compare(Genre, other.Genre);
            if (genreComparison != 0) return genreComparison;
            return PricePerDay.CompareTo(other.PricePerDay);
        }
    }
}