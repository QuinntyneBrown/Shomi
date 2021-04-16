using Shomi.Api.Models;
using Shomi.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Shomi.Api.Data
{
    public class ShomiDbContext: DbContext, IShomiDbContext
    {
        public DbSet<DigitalAsset> DigitalAssets { get; private set; }
        public DbSet<Content> Contents { get; private set; }
        public DbSet<HtmlContent> HtmlContents { get; private set; }
        public DbSet<ImageContent> ImageContents { get; private set; }
        public DbSet<Video> Videos { get; private set; }
        public ShomiDbContext(DbContextOptions options)
            :base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShomiDbContext).Assembly);
        }
        
    }
}
