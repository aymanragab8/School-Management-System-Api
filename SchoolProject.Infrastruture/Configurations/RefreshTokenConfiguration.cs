using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolProject.Domain.Entities;
using SchoolProject.Infrastruture.Identity;

namespace SchoolProject.Infrastruture.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Token)
                .IsRequired()
                .HasMaxLength(512);

            builder.HasIndex(r => r.Token)
                .IsUnique();

            builder.HasIndex(r => r.ApplicationUserId);

            builder.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(x => x.ApplicationUserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.ToTable("RefreshTokens");
        }
    }
}
