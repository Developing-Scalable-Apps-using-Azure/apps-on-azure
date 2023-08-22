using System;
using System.Collections.Generic;
using InternationalBakers.Models;
using Microsoft.EntityFrameworkCore;

namespace InternationalBakers.Data;

public partial class Sbazuresqldb221936360Context : DbContext
{
    public Sbazuresqldb221936360Context()
    {
    }

    public Sbazuresqldb221936360Context(DbContextOptions<Sbazuresqldb221936360Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<BuildVersion> BuildVersions { get; set; }

    public virtual DbSet<Cookie> Cookies { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderLine> OrderLines { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductCategory> ProductCategories { get; set; }

    public virtual DbSet<ProductDescription> ProductDescriptions { get; set; }

    public virtual DbSet<ProductModel> ProductModels { get; set; }

    public virtual DbSet<ProductModelProductDescription> ProductModelProductDescriptions { get; set; }

    public virtual DbSet<SalesOrderDetail> SalesOrderDetails { get; set; }

    public virtual DbSet<SalesOrderHeader> SalesOrderHeaders { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<VGetAllCategory> VGetAllCategories { get; set; }

    public virtual DbSet<VProductAndDescription> VProductAndDescriptions { get; set; }

    public virtual DbSet<VProductModelCatalogDescription> VProductModelCatalogDescriptions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = configuration.GetConnectionString("ibdb");
        optionsBuilder.UseSqlServer(connectionString);
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK_Address_AddressID");

            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Rowguid).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<BuildVersion>(entity =>
        {
            entity.HasKey(e => e.SystemInformationId).HasName("PK__BuildVer__35E58ECAC6E88039");

            entity.Property(e => e.SystemInformationId).ValueGeneratedOnAdd();
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Cookie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cookies__3214EC07E02CEF01");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK_Customer_CustomerID");

            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Rowguid).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.AddressId }).HasName("PK_CustomerAddress_CustomerID_AddressID");

            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Rowguid).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Address).WithMany(p => p.CustomerAddresses).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerAddresses).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.HasKey(e => e.ErrorLogId).HasName("PK_ErrorLog_ErrorLogID");

            entity.Property(e => e.ErrorTime).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Orders__3214EC0702D8B3A2");

            entity.HasOne(d => d.Store).WithMany(p => p.Orders).HasConstraintName("StoreForeignKey");
        });

        modelBuilder.Entity<OrderLine>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderLin__3214EC072AE81573");

            entity.HasOne(d => d.Cookie).WithMany(p => p.OrderLines).HasConstraintName("CookieForeignKey");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderLines)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("OrderForeignKey");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK_Product_ProductID");

            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Rowguid).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<ProductCategory>(entity =>
        {
            entity.HasKey(e => e.ProductCategoryId).HasName("PK_ProductCategory_ProductCategoryID");

            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Rowguid).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.ParentProductCategory).WithMany(p => p.InverseParentProductCategory).HasConstraintName("FK_ProductCategory_ProductCategory_ParentProductCategoryID_ProductCategoryID");
        });

        modelBuilder.Entity<ProductDescription>(entity =>
        {
            entity.HasKey(e => e.ProductDescriptionId).HasName("PK_ProductDescription_ProductDescriptionID");

            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Rowguid).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<ProductModel>(entity =>
        {
            entity.HasKey(e => e.ProductModelId).HasName("PK_ProductModel_ProductModelID");

            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Rowguid).HasDefaultValueSql("(newid())");
        });

        modelBuilder.Entity<ProductModelProductDescription>(entity =>
        {
            entity.HasKey(e => new { e.ProductModelId, e.ProductDescriptionId, e.Culture }).HasName("PK_ProductModelProductDescription_ProductModelID_ProductDescriptionID_Culture");

            entity.Property(e => e.Culture).IsFixedLength();
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Rowguid).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.ProductDescription).WithMany(p => p.ProductModelProductDescriptions).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ProductModel).WithMany(p => p.ProductModelProductDescriptions).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SalesOrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.SalesOrderId, e.SalesOrderDetailId }).HasName("PK_SalesOrderDetail_SalesOrderID_SalesOrderDetailID");

            entity.Property(e => e.SalesOrderDetailId).ValueGeneratedOnAdd();
            entity.Property(e => e.LineTotal).HasComputedColumnSql("(isnull(([UnitPrice]*((1.0)-[UnitPriceDiscount]))*[OrderQty],(0.0)))", false);
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Rowguid).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Product).WithMany(p => p.SalesOrderDetails).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<SalesOrderHeader>(entity =>
        {
            entity.HasKey(e => e.SalesOrderId).HasName("PK_SalesOrderHeader_SalesOrderID");

            entity.Property(e => e.SalesOrderId).HasDefaultValueSql("(NEXT VALUE FOR [SalesLT].[SalesOrderNumber])");
            entity.Property(e => e.Freight).HasDefaultValueSql("((0.00))");
            entity.Property(e => e.ModifiedDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.OnlineOrderFlag).HasDefaultValueSql("((1))");
            entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Rowguid).HasDefaultValueSql("(newid())");
            entity.Property(e => e.SalesOrderNumber).HasComputedColumnSql("(isnull(N'SO'+CONVERT([nvarchar](23),[SalesOrderID],(0)),N'*** ERROR ***'))", false);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.SubTotal).HasDefaultValueSql("((0.00))");
            entity.Property(e => e.TaxAmt).HasDefaultValueSql("((0.00))");
            entity.Property(e => e.TotalDue).HasComputedColumnSql("(isnull(([SubTotal]+[TaxAmt])+[Freight],(0)))", false);

            entity.HasOne(d => d.BillToAddress).WithMany(p => p.SalesOrderHeaderBillToAddresses).HasConstraintName("FK_SalesOrderHeader_Address_BillTo_AddressID");

            entity.HasOne(d => d.Customer).WithMany(p => p.SalesOrderHeaders).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.ShipToAddress).WithMany(p => p.SalesOrderHeaderShipToAddresses).HasConstraintName("FK_SalesOrderHeader_Address_ShipTo_AddressID");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Stores__3214EC07DA0FFA25");
        });

        modelBuilder.Entity<VGetAllCategory>(entity =>
        {
            entity.ToView("vGetAllCategories", "SalesLT");
        });

        modelBuilder.Entity<VProductAndDescription>(entity =>
        {
            entity.ToView("vProductAndDescription", "SalesLT");

            entity.Property(e => e.Culture).IsFixedLength();
        });

        modelBuilder.Entity<VProductModelCatalogDescription>(entity =>
        {
            entity.ToView("vProductModelCatalogDescription", "SalesLT");

            entity.Property(e => e.ProductModelId).ValueGeneratedOnAdd();
        });
        modelBuilder.HasSequence<int>("SalesOrderNumber", "SalesLT");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
