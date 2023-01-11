using ConfigurationBag.Core.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace ConfigurationBag.Infrastructure.Data.SqlServer.Configurations;

public class ValueConfiguration : IEntityTypeConfiguration<Value>
{
    public void Configure(EntityTypeBuilder<Value> builder)
    {
        builder.ToTable(nameof(Value), Schemas.Base);

        builder.HasMany(x => x.Labels)
            .WithMany(x => x.Values)
            .UsingEntity(x => x.ToTable("ValueLabels"));
    }
}