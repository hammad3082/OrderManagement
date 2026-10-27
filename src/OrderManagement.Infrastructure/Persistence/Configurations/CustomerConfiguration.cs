using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Infrastructure.Persistence.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100); 
            builder.Property(c => c.Email).IsRequired().HasMaxLength(150);

            // 1-to-1 relationship setup with CustomerProfile
            builder.HasOne(c => c.Profile)
                   .WithOne(p => p.Customer)
                   .HasForeignKey<CustomerProfile>(p => p.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);

            // 1-to-Many relationship setup with Orders
            builder.HasMany(c => c.Orders)
                   .WithOne(o => o.Customer)
                   .HasForeignKey(o => o.CustomerId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
