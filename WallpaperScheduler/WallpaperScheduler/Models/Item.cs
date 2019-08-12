using System;

namespace WallpaperScheduler.Models
{
    public class Item
    {
        public string Id { get; set; }

        public string ImageName { get; set; }
        public TimeSpan Time { get; set; }
    }
}