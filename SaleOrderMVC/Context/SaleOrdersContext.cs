using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SaleOrderMVC.Context;

public partial class SaleOrdersContext : DbContext
{
    public SaleOrdersContext()
    {
    }

    public SaleOrdersContext(DbContextOptions<SaleOrdersContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<SaleOrder> SaleOrders { get; set; }

    public virtual DbSet<SaleOrderDetail> SaleOrderDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-0STBCR2\\SQLEXPRESS;Database=SaleOrders;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PK__Customer__CD65CB85E93E9CDA");

            entity.ToTable("Customer");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.CustomerAddress)
                .HasMaxLength(100)
                .HasColumnName("customer_address");
            entity.Property(e => e.CustomerContact)
                .HasMaxLength(100)
                .HasColumnName("customer_contact");
            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .HasColumnName("customer_name");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PK__Employee__C52E0BA896F1E8A7");

            entity.ToTable("Employee");

            entity.HasIndex(e => e.EmployeeEmail, "UQ__Employee__0A874BCF93D0A642").IsUnique();

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EmployeeEmail)
                .HasMaxLength(100)
                .HasColumnName("employee_email");
            entity.Property(e => e.EmployeeFirstname)
                .HasMaxLength(100)
                .HasColumnName("employee_firstname");
            entity.Property(e => e.EmployeeLastname)
                .HasMaxLength(100)
                .HasColumnName("employee_lastname");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.ItemId).HasName("PK__Item__52020FDD63AB1A08");

            entity.ToTable("Item");

            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.ItemDescription)
                .HasMaxLength(255)
                .HasColumnName("item_description");
            entity.Property(e => e.ItemName)
                .HasMaxLength(100)
                .HasColumnName("item_name");
            entity.Property(e => e.ItemPrice)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("item_price");
            entity.Property(e => e.ItemQuantity).HasColumnName("item_quantity");
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.OrderstatusId).HasName("PK__OrderSta__0057722D8DECF786");

            entity.ToTable("OrderStatus");

            entity.Property(e => e.OrderstatusId).HasColumnName("orderstatus_id");
            entity.Property(e => e.Isdeleted).HasColumnName("isdeleted");
            entity.Property(e => e.OrderstatusName)
                .HasMaxLength(20)
                .HasColumnName("orderstatus_name");
        });

        modelBuilder.Entity<SaleOrder>(entity =>
        {
            entity.HasKey(e => e.SaleorderId).HasName("PK__SaleOrde__B0679E08C3E5295F");

            entity.ToTable("SaleOrder");

            entity.Property(e => e.SaleorderId).HasColumnName("saleorder_id");
            entity.Property(e => e.Createdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("createdate");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.OrderstatusId).HasColumnName("orderstatus_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SaleOrder__custo__5812160E");

            entity.HasOne(d => d.Employee).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SaleOrder__emplo__59063A47");

            entity.HasOne(d => d.Orderstatus).WithMany(p => p.SaleOrders)
                .HasForeignKey(d => d.OrderstatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SaleOrder__order__59FA5E80");
        });

        modelBuilder.Entity<SaleOrderDetail>(entity =>
        {
            entity.HasKey(e => e.SaleorderdetailId).HasName("PK__SaleOrde__2FF91B98348C8D22");

            entity.ToTable("SaleOrderDetail");

            entity.Property(e => e.SaleorderdetailId).HasColumnName("saleorderdetail_id");
            entity.Property(e => e.ItemId).HasColumnName("item_id");
            entity.Property(e => e.ItemQty).HasColumnName("item_qty");
            entity.Property(e => e.SaleorderId).HasColumnName("saleorder_id");

            entity.HasOne(d => d.Item).WithMany(p => p.SaleOrderDetails)
                .HasForeignKey(d => d.ItemId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SaleOrder__item___5DCAEF64");

            entity.HasOne(d => d.Saleorder).WithMany(p => p.SaleOrderDetails)
                .HasForeignKey(d => d.SaleorderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SaleOrder__saleo__5CD6CB2B");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
