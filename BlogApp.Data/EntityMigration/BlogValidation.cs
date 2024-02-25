
using BlogApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogApp.Data.EntityMigration
{
    public class BlogValidation : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasIndex(e => e.Id);
            builder.Property(e => e.Title).IsRequired();
            builder.Property(e => e.Description).IsRequired();
        }
    }
}
