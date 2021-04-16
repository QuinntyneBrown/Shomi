using System;
using System.Collections.Generic;

namespace Shomi.Api.Models
{
    public class Content
    {
        public Guid ContentId { get; set; }
        public int Name { get; set; }
        public List<ImageContent> Images { get; set; } = new();
        public List<HtmlContent> Html { get; set; } = new();
    }
}
