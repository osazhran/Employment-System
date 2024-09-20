using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Identity.Configurations;
public class VacancyConfigurations : IEntityTypeConfiguration<Vacancy>
{
    public void Configure(EntityTypeBuilder<Vacancy> builder)
    {
        builder.Property(v => v.Title)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.Description)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(v => v.Requirements)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(v => v.Responsibilities)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(v => v.ApplicationCount)
            .IsRequired();

        builder.Property(v => v.MaxApplications)
            .IsRequired();

        builder.Property(v => v.WorkType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(v => v.EmploymentType)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(v => v.ExperienceYears)
            .IsRequired();

        builder.Property(v => v.PositionLevel)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(v => v.CompanyName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(v => v.CompanyAddress)
            .IsRequired()
            .HasMaxLength(100);
    }
}