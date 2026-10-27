using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderManagement.Domain.Entities;

namespace OrderManagement.Infrastructure.Persistence.Configurations
{
    public class CustomerProfileConfiguration : IEntityTypeConfiguration<CustomerProfile>
    {
        public void Configure(EntityTypeBuilder<CustomerProfile> builder)
        {
            builder.HasKey(p => p.CustomerId);
            builder.Property(p => p.Address).IsRequired().HasMaxLength(250);
            builder.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(20);
        }
    }
}
