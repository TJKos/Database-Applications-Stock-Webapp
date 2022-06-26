using APBDProjekt.Server.Models;
using APBDProjekt.Shared.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace APBDProjekt.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public DbSet<StockInfoDB> StockInfo { get; set; }
        public DbSet<ArticleDB> Article { get; set; }
        public DbSet<StockChartDataDB> StockChartData { get; set; }
        public DbSet<StockInfo_Article> StockInfo_Article { get; set; }

        public override DatabaseFacade Database => base.Database;

        public override ChangeTracker ChangeTracker => base.ChangeTracker;

        public override IModel Model => base.Model;

        public override DbContextId ContextId => base.ContextId;

        public override DbSet<ApplicationUser> Users { get => base.Users; set => base.Users = value; }
        public override DbSet<IdentityUserClaim<string>> UserClaims { get => base.UserClaims; set => base.UserClaims = value; }
        public override DbSet<IdentityUserLogin<string>> UserLogins { get => base.UserLogins; set => base.UserLogins = value; }
        public override DbSet<IdentityUserToken<string>> UserTokens { get => base.UserTokens; set => base.UserTokens = value; }
        public override DbSet<IdentityUserRole<string>> UserRoles { get => base.UserRoles; set => base.UserRoles = value; }
        public override DbSet<IdentityRole> Roles { get => base.Roles; set => base.Roles = value; }
        public override DbSet<IdentityRoleClaim<string>> RoleClaims { get => base.RoleClaims; set => base.RoleClaims = value; }

        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StockInfoDB>(e =>
            {
                e.ToTable("StockInfo");
                e.HasKey(e => e.IdStockInfo);

                e.Property(e => e.Name).IsRequired();
                e.Property(e => e.Locale).IsRequired();
                e.Property(e => e.Ticker).IsRequired();


            });

            modelBuilder.Entity<ArticleDB>(e =>
            {
                e.ToTable("Article");
                e.HasKey(e => e.IdArticle);

                e.Property(e => e.Article_Url).IsRequired();
                e.Property(e => e.Author).IsRequired();
                e.Property(e => e.Published_Utc).IsRequired();
                e.Property(e => e.Name).IsRequired();
                e.Property(e => e.Title).IsRequired();

            });

            modelBuilder.Entity<StockChartDataDB>(e =>
            {
                e.ToTable("StockChartData");
                e.HasKey(e => e.IdStockChartData);

                e.Property(e => e.IdStockChartData).IsRequired();
                e.HasOne(e => e.StockInfo).WithMany(e => e.StockChartData).HasForeignKey(e => e.IdStockInfo).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<StockInfo_Article>(e =>
            {
                e.ToTable("StockInfo_Article");    
                e.HasKey(e => new {e.IdStockInfo, e.IdArticle});

                e.HasOne(e => e.StockInfo).WithMany(e => e.StockInfo_Article).HasForeignKey(e => e.IdStockInfo).OnDelete(DeleteBehavior.ClientSetNull);
                e.HasOne(e => e.Article).WithMany(e => e.StockInfo_Article).HasForeignKey(e => e.IdArticle).OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
