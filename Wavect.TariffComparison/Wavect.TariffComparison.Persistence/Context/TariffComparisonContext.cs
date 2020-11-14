using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Wavect.TariffComparison.Entities.Persisted;
using Wavect.TariffComparison.Interfaces.Entities;

namespace Wavect.TariffComparison.Persistence.Context
{
    public class TariffComparisonContext : DbContext
    {
        #region ctor
        public TariffComparisonContext(DbContextOptions<TariffComparisonContext> options) : base(options)
        {
        }
        #endregion

        #region db_sets
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<TariffEntity> Tariffs { get; set; }
        #endregion

        #region methods_lifecycle
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEntity>().ToTable("Product");
            modelBuilder.Entity<TariffEntity>().ToTable("Tariff");
        }
        #endregion
    }
}
