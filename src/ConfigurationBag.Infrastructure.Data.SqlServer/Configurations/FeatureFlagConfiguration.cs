using ConfigurationBag.Core.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConfigurationBag.Infrastructure.Data.SqlServer.Configurations;

public class FeatureFlagConfiguration : IEntityTypeConfiguration<FeatureFlag>
{
    public void Configure(EntityTypeBuilder<FeatureFlag> builder)
    {
        builder.ToTable(nameof(FeatureFlag), Schemas.Base);

        builder.HasMany(x => x.Labels)
            .WithMany(x => x.FeatureFlags)
            .UsingEntity(x => x.ToTable("FeatureFlagLabels"));
    }
}