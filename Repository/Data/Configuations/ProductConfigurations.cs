﻿using Core.Entities.Product_Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Data.Configuations
{
    public class ProductConfigurations : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Description)
                .IsRequired();

            builder.Property(p => p.ImageCover)
                .IsRequired();

            builder.Property(e => e.Images)
            .HasConversion(
                v => string.Join(',', v),
                v => v.Split(',', StringSplitOptions.RemoveEmptyEntries)
            );

            builder.Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Quantity)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.RatingsAverage)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(P => P.Brand)
                .WithMany()
                .HasForeignKey(FK => FK.BrandId);

            builder.HasOne(P => P.Category)
               .WithMany()
               .HasForeignKey(FK => FK.CategoryId);
        }
    }
}
