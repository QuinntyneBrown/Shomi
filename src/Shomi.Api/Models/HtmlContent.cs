using System;

namespace Shomi.Api.Models
{
    public class HtmlContent
    {        
        public Guid HtmlContentId { get; private set; }
        public Guid ContentId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Body { get; set; }
    }
}
