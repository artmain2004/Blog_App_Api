using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class PostEntityConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("posts");

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasColumnName("id").IsRequired();
        builder.Property(p => p.Title).HasColumnName("title").IsRequired();
        builder.Property(p => p.Body).HasColumnName("body").IsRequired();
        builder.Property(p => p.CreatedAt).HasColumnName("createdAt").IsRequired();
        builder.Property(p => p.UpdatedAt).HasColumnName("updatedAt");
        builder.Property(p => p.UserId).HasColumnName("user_id").IsRequired();
        builder.Property(p => p.PostImage).HasColumnName("post_image");

        builder
            .HasOne<User>( p => p.User)
            .WithMany()
            .HasForeignKey(p => p.UserId);

        builder
            .HasMany<Comment>(p => p.Comments)
            .WithOne(c => c.Post)
            .HasForeignKey(c => c.PostId);


    }
}