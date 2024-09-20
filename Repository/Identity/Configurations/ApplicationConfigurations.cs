using Core.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Repository.Identity.Configurations;
public class ApplicationConfigurations : IEntityTypeConfiguration<Application>
{
    public void Configure(EntityTypeBuilder<Application> builder)
    {   //PK To Inform EF That Is Represent Relation Betwen Vacancy And Applicant
        
        builder.HasKey(e => new { e.ApplicantId, e.VacancyId });

        //one to many  (Vacancy-Applications)
        //I Need this in App Only
        builder.HasOne(a => a.Vacancy)
       .WithMany(v => v.Applications)
       .HasForeignKey(a => a.VacancyId)
       .OnDelete(DeleteBehavior.NoAction);

        

        ///public enum ApplicationStatus {Pendig,Accepted,Rejected }
        ///Stor Data IN DB As Value Instanc Of 0,1,2
        builder.Property(o => o.Status)
            .HasConversion(
                OStatus => OStatus.ToString(),
                OStatus => (ApplicationStatus)Enum.Parse(typeof(ApplicationStatus), OStatus)
        );



        builder.Property(e => e.ResumeUrl)
            .HasMaxLength(255)
            .IsRequired();
    }
}