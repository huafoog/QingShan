using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace QingShan.DataLayer.Entities.Configuration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //builder.HasKey(x => x.Id);
            //builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            //builder.Property(x => x.Price).HasColumnType("decimal(8,2)");
        }
    }
}
