using EssentialConnection.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EssentialConnection.Areas.Identity.Data;

public class IdentityContext : IdentityDbContext<EssentialConnectionUser>
{
    public IdentityContext(DbContextOptions<IdentityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.ApplyConfiguration(new EssentialConnectionUserConfiguration());
    }
}

public class EssentialConnectionUserConfiguration : IEntityTypeConfiguration<EssentialConnectionUser>
{
    public void Configure(EntityTypeBuilder<EssentialConnectionUser> builder)
    {
        builder.Property(x => x.NomeCompleto).HasMaxLength(255);
    }
}