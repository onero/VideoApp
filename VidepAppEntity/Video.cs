﻿namespace VidepAppEntity
{
    public class Video
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public Genre Genre { get; set; } = Genre.Action;
    }
}