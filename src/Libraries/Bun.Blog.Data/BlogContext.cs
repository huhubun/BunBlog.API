using Bun.Blog.Core.Domain.Posts;
using Bun.Blog.Core.Domain.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;

namespace Bun.Blog.Data
{
    public class BlogContext : IdentityDbContext<User>
    {
        private readonly ILoggerFactory _loggerFactory;

        public BlogContext(DbContextOptions<BlogContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            ConfigIdentityEntityType(modelBuilder);
            RunEntityTypeConfigurations(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        public DbSet<Post> Posts { get; set; }

        #region Utility

        private void RunEntityTypeConfigurations(ModelBuilder modelBuilder)
        {
            var assembly = Assembly.Load(new AssemblyName("Bun.Blog.Data"));

            foreach (var type in assembly.DefinedTypes)
            {
                foreach (var i in type.GetInterfaces())
                {
                    // IEntityTypeConfiguration<TEntity>
                    if (i.IsConstructedGenericType && i.GetGenericTypeDefinition() == typeof(Data.Extensions.IEntityTypeConfiguration<>))
                    {
                        // TEntity
                        var entityType = i.GenericTypeArguments.Single();

                        // modelBuilder.Entity<TEntity>() returns EntityTypeBuilder<TEntity>
                        var entityMethod = typeof(ModelBuilder).GetMethods(BindingFlags.Instance | BindingFlags.Public)
                            .Single(m => m.Name == "Entity" && m.IsGenericMethod && m.GetParameters().Length == 0);
                        dynamic entityTypeBuilder = entityMethod.MakeGenericMethod(entityType).Invoke(modelBuilder, null);

                        // type is IEntityTypeConfiguration<TEntity> instance
                        dynamic configuration = Activator.CreateInstance(type.AsType());
                        configuration.Configure(entityTypeBuilder);
                    }
                }
            }
        }

        private void ConfigIdentityEntityType(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
        }

        #endregion
    }
}
