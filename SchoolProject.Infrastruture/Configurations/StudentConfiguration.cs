using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Domain.Entities;
using SchoolProject.Infrastruture.Identity;

namespace SchoolProject.Infrastruture.Configurations
{
    internal class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasQueryFilter(m => !m.IsDeleted);
            builder.Property(s => s.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.NationalId)
                .IsRequired()
                .HasMaxLength(14);

            builder.HasIndex(s => s.NationalId)
                .IsUnique();

            builder.Property(t => t.ApplicationUserId)
                  .IsRequired(false);

            builder.Property(s => s.Gender)
                .HasConversion<string>();

            builder.HasOne(s => s.Grade)
                .WithMany(g => g.Students)
                .HasForeignKey(s => s.GradeId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasMany(s => s.Enrollments)
                .WithOne(e => e.Student)
                .HasForeignKey(e => e.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ApplicationUser>()
                   .WithMany()
                   .HasForeignKey(m => m.ApplicationUserId);

            builder.ToTable("Students");
        }
    }
}
