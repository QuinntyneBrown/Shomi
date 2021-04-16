using System;
using Shomi.Api.Models;

namespace Shomi.Api.Features
{
    public static class ContentExtensions
    {
        public static ContentDto ToDto(this Content content)
        {
            return new ()
            {
                ContentId = content.ContentId
            };
        }
        
    }
}
