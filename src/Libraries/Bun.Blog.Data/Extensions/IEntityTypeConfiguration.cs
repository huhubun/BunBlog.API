using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Bun.Blog.Data.Extensions
{
    // TODO Entity Framework Core 将在 2.0 支持 IEntityTypeConfiguration<T>，这里直接采用了 EFCore 的写法
    // 参见 https://github.com/aspnet/EntityFramework/issues/2805
    public interface IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        void Configure(EntityTypeBuilder<TEntity> builder);
    }
}
