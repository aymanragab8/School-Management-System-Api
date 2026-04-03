using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Infrastruture.Configurations
{
    public class EnrollmentConfiguration : IEntityTypeConfiguration<Enrollment>
    {
        public void Configure(EntityTypeBuilder<Enrollment> builder)
        {
            builder.HasKey(e => e.Id);
            builder.HasQueryFilter(m => !m.IsDeleted);
            builder.HasIndex(e => new { e.StudentId, e.CourseId })
                .IsUnique();

            builder.Property(e => e.Status)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(e => e.Score)
                .IsRequired(false);

            builder.ToTable("Enrollments");
        }
    }
}
