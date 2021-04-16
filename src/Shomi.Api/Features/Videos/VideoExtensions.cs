using System;
using Shomi.Api.Models;

namespace Shomi.Api.Features
{
    public static class VideoExtensions
    {
        public static VideoDto ToDto(this Video video)
        {
            return new ()
            {
                VideoId = video.VideoId
            };
        }
        
    }
}
