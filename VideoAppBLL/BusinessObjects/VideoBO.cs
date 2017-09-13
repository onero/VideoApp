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

        public int GenreId { get; set; }
        public GenreBO Genre { get; set; }

        public List<RentalBO> Rentals { get; set; } = new List<RentalBO>();

        public int CompareTo(VideoBO other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var idComparison = Id.CompareTo(other.Id);
            if (idComparison != 0) return idComparison;
            return string.Compare(Title, other.Title, StringComparison.Ordinal);
        }
    }
}