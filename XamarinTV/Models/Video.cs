using System;

namespace XamarinTV.Models
{
    public class Video
    {
        public int VideoId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public string ListName { get; set; }
        public int ViewCount { get; set; }
        public int Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Description { get; internal set; }
    }
}