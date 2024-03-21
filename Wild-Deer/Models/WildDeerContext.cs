using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Wild_Deer.Models;

public partial class WildDeerContext : DbContext
{
    public WildDeerContext()
    {
    }

    public WildDeerContext(DbContextOptions<WildDeerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<ExtraImage> ExtraImages { get; set; }

    public virtual DbSet<Hr> Hrs { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Seller> Sellers { get; set; }

    public virtual DbSet<Sold> Solds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("name=ConnectionString");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId);
            entity.Property(e => e.CommentId)
                .HasColumnName("CommentID").ValueGeneratedOnAdd();
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.Username).HasMaxLength(20);
            entity.Property(e => e.WriterId).HasColumnName("WriterID");

        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("UserID");
            entity.Property(e => e.Adress).HasMaxLength(75);
            entity.Property(e => e.Birthday).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.FirstName).HasMaxLength(15);
            entity.Property(e => e.JoinDate).HasColumnType("datetime");
            entity.Property(e => e.LastName).HasMaxLength(15);
            entity.Property(e => e.Phone).HasMaxLength(11);
            entity.Property(e => e.Username).HasMaxLength(20);
        });

        modelBuilder.Entity<ExtraImage>(entity =>
        {
            entity.HasKey(e => e.ProductId);

            entity.Property(e => e.ProductId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ProductID");
            entity.Property(e => e.Img1).HasColumnName("IMG1");
            entity.Property(e => e.Img2).HasColumnName("IMG2");
            entity.Property(e => e.Img3).HasColumnName("IMG3");
            entity.Property(e => e.Img4).HasColumnName("IMG4");
            entity.Property(e => e.Img5).HasColumnName("IMG5");
            entity.Property(e => e.Img6).HasColumnName("IMG6");
        });

        modelBuilder.Entity<Hr>(entity =>
        {
            entity.ToTable("HR");

            entity.Property(e => e.Hrid)
                .ValueGeneratedOnAdd()
                .HasColumnName("HRID");
            entity.Property(e => e.Username).HasMaxLength(20);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProductId)
                .ValueGeneratedOnAdd()
                .HasColumnName("ProductID");
            entity.Property(e => e.Category).HasMaxLength(15);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.SellerId).HasColumnName("SellerID");
            entity.Property(e => e.Title).HasMaxLength(50);

        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.Property(e => e.SellerId)
                .ValueGeneratedOnAdd()
                .HasColumnName("SellerID");
            entity.Property(e => e.Adress).HasMaxLength(75);
            entity.Property(e => e.CompanyName).HasMaxLength(30);
            entity.Property(e => e.Email).HasMaxLength(30);
            entity.Property(e => e.JoinedDate).HasColumnType("datetime");
            entity.Property(e => e.MostProductsEra).HasMaxLength(15);
            entity.Property(e => e.OwnerName).HasMaxLength(25);
            entity.Property(e => e.Phone).HasMaxLength(11);
            entity.Property(e => e.Username).HasMaxLength(20);
        });

        modelBuilder.Entity<Sold>(entity =>
        {
            entity.HasKey(e => e.SellInfoId);

            entity.ToTable("Sold");

            entity.Property(e => e.SellInfoId)
                .HasMaxLength(20)
                .HasColumnName("SellInfoID").ValueGeneratedOnAdd();
            entity.Property(e => e.BuyerId).HasColumnName("BuyerID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.SellerId).HasColumnName("SellerID");

        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
