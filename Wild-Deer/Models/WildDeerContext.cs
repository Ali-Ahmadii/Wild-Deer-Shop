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

            entity.HasOne(d => d.Product).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Products");

            entity.HasOne(d => d.Writer).WithMany(p => p.Comments)
                .HasForeignKey(d => d.WriterId)
                .HasConstraintName("FK_Comments_Customers");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
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
                .ValueGeneratedNever()
                .HasColumnName("ProductID");
            entity.Property(e => e.Img1).HasColumnName("IMG1");
            entity.Property(e => e.Img10).HasColumnName("IMG10");
            entity.Property(e => e.Img2).HasColumnName("IMG2");
            entity.Property(e => e.Img3).HasColumnName("IMG3");
            entity.Property(e => e.Img4).HasColumnName("IMG4");
            entity.Property(e => e.Img5).HasColumnName("IMG5");
            entity.Property(e => e.Img6).HasColumnName("IMG6");
            entity.Property(e => e.Img7).HasColumnName("IMG7");
            entity.Property(e => e.Img8).HasColumnName("IMG8");
            entity.Property(e => e.Img9).HasColumnName("IMG9");

            entity.HasOne(d => d.Product).WithOne(p => p.ExtraImage)
                .HasForeignKey<ExtraImage>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ExtraImages_Products");
        });

        modelBuilder.Entity<Hr>(entity =>
        {
            entity.ToTable("HR");

            entity.Property(e => e.Hrid)
                .ValueGeneratedNever()
                .HasColumnName("HRID");
            entity.Property(e => e.Username).HasMaxLength(20);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.ProductId)
                .ValueGeneratedNever()
                .HasColumnName("ProductID");
            entity.Property(e => e.Category).HasMaxLength(15);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.SellerId).HasColumnName("SellerID");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.ProductNavigation).WithOne(p => p.Product)
                .HasForeignKey<Product>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Sellers");
        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.Property(e => e.SellerId)
                .ValueGeneratedNever()
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
                .HasColumnName("SellInfoID");
            entity.Property(e => e.BuyerId).HasColumnName("BuyerID");
            entity.Property(e => e.PaymentMethod).HasMaxLength(20);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.SellerId).HasColumnName("SellerID");

            entity.HasOne(d => d.Buyer).WithMany(p => p.Solds)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sold_Customers");

            entity.HasOne(d => d.Product).WithMany(p => p.Solds)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sold_Products");

            entity.HasOne(d => d.Seller).WithMany(p => p.Solds)
                .HasForeignKey(d => d.SellerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sold_Sellers");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
