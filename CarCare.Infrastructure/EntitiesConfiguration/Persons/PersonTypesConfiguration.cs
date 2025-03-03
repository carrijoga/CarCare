using CarCare.Domain.Entities.Persons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarCare.Infrastructure.EntitiesConfiguration.Persons;

public class PersonTypesConfiguration : IEntityTypeConfiguration<PersonType>
{
    public void Configure(EntityTypeBuilder<PersonType> builder)
    {
        builder.ToTable(nameof(PersonType));
        builder.HasKey(x => x.PersonTypeId);

        builder.Property(x => x.Type)
            .HasColumnName(nameof(PersonType.Type))
            .IsRequired();

        builder.HasOne(x => x.Person)
            .WithMany(x => x.Types)
            .HasForeignKey(x => x.PersonId);
    }
}