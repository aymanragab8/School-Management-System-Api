using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Domain.Entities;
using SchoolProject.Infrastruture.Identity;

namespace SchoolProject.Infrastruture.Configurations
{
    public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasQueryFilter(m => !m.IsDeleted);
            builder.Property(t => t.FullName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(t => t.Specialization)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(t => t.Salary)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.Gender)
                .HasConversion<string>();

            builder.Property(t => t.ApplicationUserId)
                .IsRequired(false);

            builder.HasMany(t => t.Courses)
                .WithOne(c => c.Teacher)
                .HasForeignKey(c => c.TeacherId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ApplicationUser>()
                   .WithMany()
                   .HasForeignKey(m => m.ApplicationUserId);

            builder.ToTable("Teachers");
        }
    }
}
