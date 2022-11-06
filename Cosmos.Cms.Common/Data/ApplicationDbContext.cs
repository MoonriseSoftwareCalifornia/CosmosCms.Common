using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Cosmos.Cms.Common.Data
{
    /// <summary>
    ///     Database Context for Cosmos CMS
    /// </summary>
    public class ApplicationDbContext : AspNetCore.Identity.CosmosDb.CosmosIdentityDbContext<IdentityUser, IdentityRole>
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        ///     Determine if this service is configured
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsConfigured()
        {
            return await base.Database.CanConnectAsync();
        }

        #region OVERRIDES

        /// <summary>
        ///     On model creating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("CosmosCms");

            // Need to make a convertion so article number can be used as a partition key
            modelBuilder.Entity<Article>()
                .Property(e => e.ArticleNumber)
                .HasConversion<string>();

            modelBuilder.Entity<Article>()
                .ToContainer(nameof(Article))
                .HasPartitionKey(a => a.ArticleNumber)
                .HasKey(article => article.Id);

            modelBuilder.Entity<PublishedPage>()
                .ToContainer("Pages")
                .HasPartitionKey(a => a.UrlPath)
                .HasKey(article => article.Id);

            modelBuilder.Entity<ArticleLog>()
                .ToContainer(nameof(ArticleLog))
                .HasPartitionKey(k => k.Id)
                .HasKey(log => log.Id);

            modelBuilder.Entity<NodeScript>()
                .ToContainer(nameof(NodeScript))
                .HasPartitionKey(k => k.Id)
                .HasKey(node => node.Id);

            base.OnModelCreating(modelBuilder);
        }

        #endregion

        #region DbContext

        /// <summary>
        ///     Articles
        /// </summary>
        public DbSet<Article> Articles { get; set; }

        /// <summary>
        /// Published pages viewable via the publisher.
        /// </summary>
        public DbSet<PublishedPage> Pages { get; set; }

        /// <summary>
        /// Catalog of Articles
        /// </summary>
        public DbSet<CatalogEntry> ArticleCatalog { get; set; }

        /// <summary>
        /// Article locks
        /// </summary>
        public DbSet<ArticleLock> ArticleLocks { get; set; }

        /// <summary>
        ///     Article activity logs
        /// </summary>
        public DbSet<ArticleLog> ArticleLogs { get; set; }

        /// <summary>
        ///     Website layouts
        /// </summary>
        public DbSet<Layout> Layouts { get; set; }

        /// <summary>
        /// Node Scripts
        /// </summary>
        public DbSet<NodeScript> NodeScripts { get; set; }

        /// <summary>
        ///     Web page templates
        /// </summary>
        public DbSet<Template> Templates { get; set; }

        /// <summary>
        /// Site settings.
        /// </summary>
        public DbSet<Setting> Settings { get; set; }

        #endregion
    }
}