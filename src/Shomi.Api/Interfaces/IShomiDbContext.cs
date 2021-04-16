using Shomi.Api.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Threading;

namespace Shomi.Api.Interfaces
{
    public interface IShomiDbContext
    {
        DbSet<DigitalAsset> DigitalAssets { get; }
        DbSet<Content> Contents { get; }
        DbSet<HtmlContent> HtmlContents { get; }
        DbSet<ImageContent> ImageContents { get; }
        DbSet<Video> Videos { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
    }
}
