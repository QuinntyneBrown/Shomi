using System;
using Shomi.Api.Models;

namespace Shomi.Api.Features
{
    public static class ImageContentExtensions
    {
        public static ImageContentDto ToDto(this ImageContent imageContent)
        {
            return new ()
            {
                ImageContentId = imageContent.ImageContentId
            };
        }
        
    }
}
