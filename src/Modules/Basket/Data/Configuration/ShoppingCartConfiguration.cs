using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Basket.Data.Configuration;

public class ShoppingCartConfiguration : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.HasKey(s => s.Id);

        builder.HasIndex(s => s.UserName)
            .IsUnique();

        builder.Property(s => s.UserName)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasMany(s => s.Items)
            .WithOne()
            .HasForeignKey(s => s.ShoppingCartId);
    }
}
