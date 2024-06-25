using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configuration;

public class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.HasKey(k => k.Id);

        builder.Property(p => p.Id).HasColumnName("id").IsRequired();
        builder.Property(p => p.Name).HasColumnName("name").IsRequired();
        builder.Property(p => p.Lastname).HasColumnName("lastname").IsRequired();
        builder.Property(p => p.Email).HasColumnName("email").IsRequired();
        builder.Property(p => p.Password).HasColumnName("password").IsRequired();

        //builder.HasData(

        //    new User()
        //    {
        //        Id = ,
        //        Name = "Artsiom",
        //        Lastname = "Kryvanos",
        //        Email = "artsiom@gmail.com",
        //        Password = "artsiom123"
        //    },
            
        //    new User()
        //    {
        //        Id = Guid.NewGuid(),
        //        Name = "Lera",
        //        Lastname = "Kryvanos",
        //        Email = "lera@gmail.com",
        //        Password = "lera123"
        //    }


        //    );
        
    }
}

