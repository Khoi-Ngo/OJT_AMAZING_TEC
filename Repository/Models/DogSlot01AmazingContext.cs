using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repository.Models;

public partial class DogSlot01AmazingContext : DbContext
{
    public DogSlot01AmazingContext()
    {
    }

    public DogSlot01AmazingContext(DbContextOptions<DogSlot01AmazingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dog> Dogs { get; set; }

    public virtual DbSet<DogType> DogTypes { get; set; }

    public virtual DbSet<Owner> Owners { get; set; }

    public virtual DbSet<Owning> Ownings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =DESKTOP-FMGJ86C\\KHOINGO; database=DogSlot01_Amazing;uid=sa;pwd=12345; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dog>(entity =>
        {
            entity.HasKey(e => e.DogId).HasName("PK__Dogs__46F46EE99A0CAD43");

            entity.Property(e => e.DogId).ValueGeneratedNever();

            entity.HasOne(d => d.DogType).WithMany(p => p.Dogs)
                .HasForeignKey(d => d.DogTypeId)
                .HasConstraintName("FK__Dogs__DogTypeId__4BAC3F29");
        });

        modelBuilder.Entity<DogType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DogTypes__3214EC07458DE5DF");

            entity.Property(e => e.Id).ValueGeneratedNever();
        });

        modelBuilder.Entity<Owner>(entity =>
        {
            entity.HasKey(e => e.OwnerId).HasName("PK__Owners__819385B86C93955A");

            entity.Property(e => e.OwnerId).ValueGeneratedNever();
        });

        modelBuilder.Entity<Owning>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Ownings__3214EC070435EE14");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.Dog).WithMany(p => p.Ownings)
                .HasForeignKey(d => d.DogId)
                .HasConstraintName("FK__Ownings__DogId__5165187F");

            entity.HasOne(d => d.Owner).WithMany(p => p.Ownings)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("FK__Ownings__OwnerId__5070F446");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__users__CB9A1CFFDC5987C4");

            entity.ToTable("users");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("userId");
            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .HasColumnName("userName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
