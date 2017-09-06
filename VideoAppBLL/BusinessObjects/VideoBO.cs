using System;
using System.ComponentModel.DataAnnotations;

namespace VideoAppBLL.BusinessObjects
{
    public class VideoBO : IComparable<VideoBO>
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        
        public GenreBO Genre { get; set; } = GenreBO.Action;

        public int CompareTo(VideoBO other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var idComparison = Id.CompareTo(other.Id);
            if (idComparison != 0) return idComparison;
            var titleComparison = string.Compare(Title, other.Title, StringComparison.Ordinal);
            if (titleComparison != 0) return titleComparison;
            return Genre.CompareTo(other.Genre);
        }
    }
}