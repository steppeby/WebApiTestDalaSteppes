using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models;

namespace DataAccess.Data.ModelsConfiguration
{
    public class WeightingConfiguration : IEntityTypeConfiguration<Weighting>
    {
        public void Configure(EntityTypeBuilder<Weighting> builder)
        {
            builder.HasIndex(w => new { w.AnimalId, w.WeightDate }).IsUnique();
        }
    }
}
