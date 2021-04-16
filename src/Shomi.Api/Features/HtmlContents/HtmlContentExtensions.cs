using System;
using Shomi.Api.Models;

namespace Shomi.Api.Features
{
    public static class HtmlContentExtensions
    {
        public static HtmlContentDto ToDto(this HtmlContent htmlContent)
        {
            return new ()
            {
                HtmlContentId = htmlContent.HtmlContentId
            };
        }
        
    }
}
