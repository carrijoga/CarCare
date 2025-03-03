using CarCare.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCare.Infrastructure.EntitiesConfiguration.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(Users));
        builder.HasKey(x => x.UserId);

        builder.Property(x => x.Id)
            .HasColumnName(nameof(User.Id))
            .IsRequired();

        builder.Property(x => x.PersonId)
            .HasColumnName(nameof(User.PersonId))
            .IsRequired();
        

        builder.Property(x => x.Email)
            .HasColumnName(nameof(User.Email))
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Username)
            .HasColumnName(nameof(User.Username))
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Password)
            .HasColumnName(nameof(User.Password))
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasColumnName(nameof(User.IsActive))
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .HasColumnName(nameof(User.CreatedAt))
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");

        builder.Property(x => x.UpdatedAt)
            .HasColumnName(nameof(User.UpdatedAt));

        builder.HasOne(x => x.Person)
            .WithMany()
            .HasForeignKey(x => x.PersonId);
    }
}