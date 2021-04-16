using System;

namespace Shomi.Api.Models
{
    public class ImageContent
    {
        public Guid ImageContentId { get; set; }
        public Guid ContentId { get; private set; }
        public Guid DigitalAssetId { get; set; }
        public string Name { get; set; }
    }
}
