﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QS.DataLayer.Entities.Configuration
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
