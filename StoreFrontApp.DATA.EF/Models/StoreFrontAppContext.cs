using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StoreFrontApp.DATA.EF.Models
{
    public partial class StoreFrontAppContext : DbContext
    {
        public StoreFrontAppContext()
        {
        }

        public StoreFrontAppContext(DbContextOptions<StoreFrontAppContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Inventory> Inventories { get; set; } = null!;
        public virtual DbSet<InventoryToProduct> InventoryToProducts { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<Season> Seasons { get; set; } = null!;
        public virtual DbSet<Supplier> Suppliers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\sqlexpress;database=StoreFrontApp;trusted_connection=true;multipleactiveresultsets=true; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.ToTable("Inventory");

                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");

                entity.Property(e => e.InventoryDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.InventoryName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.UnitPrice).HasColumnType("money");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Inventories)
                    .HasForeignKey(d => d.SupplierId)
                    .HasConstraintName("FK_Inventory_Suppliers");
            });

            modelBuilder.Entity<InventoryToProduct>(entity =>
            {
                entity.HasKey(e => e.InvToProdId);

                entity.ToTable("InventoryToProduct");

                entity.Property(e => e.InvToProdId).HasColumnName("InvToProdID");

                entity.Property(e => e.InventoryId).HasColumnName("InventoryID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Inventory)
                    .WithMany(p => p.InventoryToProducts)
                    .HasForeignKey(d => d.InventoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InventoryToProduct_Inventory");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.InventoryToProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InventoryToProduct_Products");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.ProductDescription)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ProductImage).HasMaxLength(128);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ProductPrice).HasColumnType("money");

                entity.Property(e => e.SeasonId).HasColumnName("SeasonID");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Categories");

                entity.HasOne(d => d.Season)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SeasonId)
                    .HasConstraintName("FK_Products_Seasons");
            });

            modelBuilder.Entity<Season>(entity =>
            {
                entity.Property(e => e.SeasonId).HasColumnName("SeasonID");

                entity.Property(e => e.SeasonDescription)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.Property(e => e.SupplierId).HasColumnName("SupplierID");

                entity.Property(e => e.Address)
                    .HasMaxLength(70)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyContact)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Zip)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
