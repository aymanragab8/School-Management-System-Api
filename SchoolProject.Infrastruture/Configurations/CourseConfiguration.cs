using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Infrastruture.Configurations
{
    public class CourseConfiguration : IEntityTypeConfiguration<Course>
    {
        public void Configure(EntityTypeBuilder<Course> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasQueryFilter(m => !m.IsDeleted);
            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasOne(c => c.Teacher)
                .WithMany(t => t.Courses)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.Grade)
                .WithMany(g => g.Courses)
                .HasForeignKey(c => c.GradeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(c => c.Enrollments)
                .WithOne(e => e.Course)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("Courses");
        }
    }
}
