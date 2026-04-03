using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Domain.Entities;

namespace SchoolProject.Infrastruture.Configurations
{
    public class GradeConfiguration : IEntityTypeConfiguration<Grade>
    {
        public void Configure(EntityTypeBuilder<Grade> builder)
        {
            builder.HasKey(g => g.Id);
            builder.HasQueryFilter(m => !m.IsDeleted);
            builder.Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(g => g.Level)
                .IsUnique();

            builder.ToTable("Grades");
        }
    }
}
