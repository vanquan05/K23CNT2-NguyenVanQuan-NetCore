using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NguyenVanQuan_2310900084.Models;

public partial class NguyenVanQuan2310900084Context : DbContext
{
    public NguyenVanQuan2310900084Context()
    {
    }

    public NguyenVanQuan2310900084Context(DbContextOptions<NguyenVanQuan2310900084Context> options)
        : base(options)
    {
    }

    public virtual DbSet<NvqEmployee> NvqEmployees { get; set; }

    public virtual DbSet<TlKhoa> TlKhoas { get; set; }

    public virtual DbSet<TlSinhvien> TlSinhviens { get; set; }

    public virtual DbSet<VwNguyenVanQuan2310900084> VwNguyenVanQuan2310900084s { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Server=MSI\\MAY1;Database=NguyenVanQuan_2310900084;Trusted_Connection=True;MultipleActiveResultSets=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NvqEmployee>(entity =>
        {
            entity.HasKey(e => e.NvqEmpId).HasName("PK__NvqEmplo__1B15643ED832D265");

            entity.ToTable("NvqEmployee");

            entity.Property(e => e.NvqEmpId).HasColumnName("nvqEmpId");
            entity.Property(e => e.NvqEmpLevel)
                .HasMaxLength(50)
                .HasColumnName("nvqEmpLevel");
            entity.Property(e => e.NvqEmpName)
                .HasMaxLength(100)
                .HasColumnName("nvqEmpName");
            entity.Property(e => e.NvqEmpStartDate).HasColumnName("nvqEmpStartDate");
            entity.Property(e => e.NvqEmpStatus).HasColumnName("nvqEmpStatus");
        });

        modelBuilder.Entity<TlKhoa>(entity =>
        {
            entity.HasKey(e => e.MaKhoa).HasName("PK__tlKhoa__65390405E570BD00");

            entity.ToTable("tlKhoa");

            entity.Property(e => e.MaKhoa)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.TenKhoa).HasMaxLength(50);
        });

        modelBuilder.Entity<TlSinhvien>(entity =>
        {
            entity.HasKey(e => e.MaSv).HasName("PK__tlSinhvi__2725081AEA6DFDB3");

            entity.ToTable("tlSinhvien");

            entity.Property(e => e.MaSv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaSV");
            entity.Property(e => e.Hosv)
                .HasMaxLength(50)
                .HasColumnName("HOSV");
            entity.Property(e => e.MaKhoa)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Ngaysinh).HasColumnType("datetime");
            entity.Property(e => e.Phai)
                .HasMaxLength(3)
                .HasColumnName("PHAI");
            entity.Property(e => e.Ten).HasMaxLength(50);

            entity.HasOne(d => d.MaKhoaNavigation).WithMany(p => p.TlSinhviens)
                .HasForeignKey(d => d.MaKhoa)
                .HasConstraintName("FK__tlSinhvie__MaKho__3F466844");
        });

        modelBuilder.Entity<VwNguyenVanQuan2310900084>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_Nguyen_Van_Quan_2310900084");

            entity.Property(e => e.Hosv)
                .HasMaxLength(50)
                .HasColumnName("HOSV");
            entity.Property(e => e.MaKhoa)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.MaSv)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("MaSV");
            entity.Property(e => e.NgaySinh).HasColumnType("datetime");
            entity.Property(e => e.Phai)
                .HasMaxLength(3)
                .HasColumnName("PHAI");
            entity.Property(e => e.Ten).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
