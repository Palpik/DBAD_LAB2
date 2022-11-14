using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Domain
{
    public partial class AdAgencyContext : DbContext
    {
        public AdAgencyContext()
        {
        }

        public AdAgencyContext(DbContextOptions<AdAgencyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AdPlace> AdPlaces { get; set; } = null!;
        public virtual DbSet<AdType> AdTypes { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Departament> Departaments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<MyView> MyViews { get; set; } = null!;
        public virtual DbSet<OptionalService> OptionalServices { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrdersOptional> OrdersOptionals { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdPlace>(entity =>
            {
                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Place).HasMaxLength(50);

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.AdPlaces)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AdPlaces_AdTypes");
            });

            modelBuilder.Entity<AdType>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(25);

                entity.Property(e => e.LastName).HasMaxLength(25);

                entity.Property(e => e.MiddleName).HasMaxLength(25);

                entity.Property(e => e.PhoneNumber).HasMaxLength(16);
            });

            modelBuilder.Entity<Departament>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(35);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.FirstName).HasMaxLength(25);

                entity.Property(e => e.LastName).HasMaxLength(25);

                entity.Property(e => e.MiddleName).HasMaxLength(25);

                entity.HasOne(d => d.Position)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PositionId)
                    .HasConstraintName("FK_Employees_Departaments");
            });

            modelBuilder.Entity<MyView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("MyView");

                entity.Property(e => e.НомерЗаказа).HasColumnName("Номер заказа");

                entity.Property(e => e.Описание).HasMaxLength(100);

                entity.Property(e => e.ОписаниеМеста)
                    .HasMaxLength(100)
                    .HasColumnName("Описание места");

                entity.Property(e => e.Расположение).HasMaxLength(50);

                entity.Property(e => e.Стоимость).HasColumnType("money");

                entity.Property(e => e.ТипРекламы)
                    .HasMaxLength(50)
                    .HasColumnName("Тип рекламы");
            });

            modelBuilder.Entity<OptionalService>(entity =>
            {
                entity.Property(e => e.Cost).HasColumnType("money");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.EndDate).HasColumnType("date");

                entity.Property(e => e.OrderDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_Orders_Customers");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK_Orders_Employees");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PlaceId)
                    .HasConstraintName("FK_Orders_AdPlaces");
            });

            modelBuilder.Entity<OrdersOptional>(entity =>
            {
                entity.HasOne(d => d.Option)
                    .WithMany(p => p.OrdersOptionals)
                    .HasForeignKey(d => d.OptionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdersOptionals_OptionalServices");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrdersOptionals)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrdersOptionals_Orders");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(35);

                entity.HasOne(d => d.Dep)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.DepId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Posts_Departaments");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
