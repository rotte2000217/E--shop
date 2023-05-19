using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Models;

namespace WebshopServer.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Quantity).IsRequired();

            builder.Property(x => x.Comment).HasMaxLength(60);

            builder.Property(x => x.Address).IsRequired().HasMaxLength(30);

            builder
                .HasOne(x => x.Article)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.ArticleId);

            builder
                .HasOne(x => x.Buyer)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.OrderStatus).HasConversion<string>();
        }
    }
}
