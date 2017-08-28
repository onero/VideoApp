using System;
using VideoAppDAL.Entities;

namespace VideoAppBLL.BusinessObjects
{
    public class VideoBO : IComparable<VideoBO>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Genre Genre { get; set; } = Genre.Action;

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