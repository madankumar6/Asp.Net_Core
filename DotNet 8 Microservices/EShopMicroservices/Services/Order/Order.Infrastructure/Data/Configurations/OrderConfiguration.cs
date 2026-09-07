using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Enums;
using Order.Domain.Models;
using Order.Domain.ValueObjects;

namespace Order.Infrastructure.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Domain.Models.Order>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion
                (
                    orderId => orderId.Value,
                    dbId => OrderId.Of(dbId)
                );

            builder.HasOne<Customer>()
                .WithMany()
                .HasForeignKey(x => x.CustomerId)
                .IsRequired();

            builder.HasMany(x => x.OrderItems)
                .WithOne()
                .HasForeignKey(x => x.OrderId);

            builder.ComplexProperty(
                    x => x.OrderName, nameBuilder =>
                    {
                        nameBuilder.Property(n => n.Value)
                        .HasColumnName(nameof(Domain.Models.Order.OrderName))
                        .HasMaxLength(100)
                        .IsRequired();
                    }
                );

            builder.ComplexProperty(
                    x => x.ShippingAddress, addressBuilder =>
                    {
                        addressBuilder.Property(add => add.FirstName)
                        .HasMaxLength(100)
                        .IsRequired();

                        addressBuilder.Property(add => add.LastName)
                        .HasMaxLength(100)
                        .IsRequired();

                        addressBuilder.Property(add => add.Email)
                        .HasMaxLength(100)
                        .IsRequired();

                        addressBuilder.Property(add => add.AddressLine)
                        .HasMaxLength(100)
                        .IsRequired();

                        addressBuilder.Property(add => add.Country)
                        .HasMaxLength(50);

                        addressBuilder.Property(add => add.State)
                        .HasMaxLength(50);

                        addressBuilder.Property(add => add.ZipCode)
                        .HasMaxLength(10)
                        .IsRequired();
                    }
            );

            builder.ComplexProperty(
                    x => x.BillingAddress, addressBuilder =>
                    {
                        addressBuilder.Property(add => add.FirstName)
                        .HasMaxLength(100)
                        .IsRequired();

                        addressBuilder.Property(add => add.LastName)
                        .HasMaxLength(100)
                        .IsRequired();

                        addressBuilder.Property(add => add.Email)
                        .HasMaxLength(100)
                        .IsRequired();

                        addressBuilder.Property(add => add.AddressLine)
                        .HasMaxLength(100)
                        .IsRequired();

                        addressBuilder.Property(add => add.Country)
                        .HasMaxLength(50);

                        addressBuilder.Property(add => add.State)
                        .HasMaxLength(50);

                        addressBuilder.Property(add => add.ZipCode)
                        .HasMaxLength(10)
                        .IsRequired();
                    }
            );

            builder.ComplexProperty(
                    x => x.Payment, paymentBuilder =>
                    {
                        paymentBuilder.Property(p => p.CardName)
                        .HasMaxLength(100)
                        .IsRequired();

                        paymentBuilder.Property(p => p.CardNumber)
                        .HasMaxLength(20)
                        .IsRequired();

                        paymentBuilder.Property(p => p.Expiration)
                        .HasMaxLength(10)
                        .IsRequired();

                        paymentBuilder.Property(p => p.CVV)
                        .HasMaxLength(3)
                        .IsRequired();

                        paymentBuilder.Property(p => p.PaymentMethod);
                    }
            );

            builder.Property(x => x.Status)
                .HasDefaultValue(OrderStatus.Draft)
                .HasConversion(
                    s => s.ToString(),
                    dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus)
                );

            builder.Property(o => o.TotalPrice);
        }
    }
}
