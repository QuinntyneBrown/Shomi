using System;

namespace Shomi.Api.Models
{
    public class Video
    {
        public Guid VideoId { get; set; }
        public string YouTubeId { get; private set; }
        public Guid? DigitalAssetId { get; private set; }
    }
}
