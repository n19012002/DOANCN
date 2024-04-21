using System;
using System.Collections.Generic;
using DOANCN.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace DOANCN.Models;

public partial class RenluyenContext : DbContext
{
    public RenluyenContext()
    {
    }

    public RenluyenContext(DbContextOptions<RenluyenContext> options)
        : base(options)
    {
    }

	public DbSet<AdminMenu> AdminMenus { get; set; }

	public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<TblCanBo> TblCanBos { get; set; }

    public virtual DbSet<TblChitietPhieuRl> TblChitietPhieuRls { get; set; }

    public virtual DbSet<TblChucvu> TblChucvus { get; set; }

    public virtual DbSet<TblDiemrenluyen> TblDiemrenluyens { get; set; }

    public virtual DbSet<TblHocKy> TblHocKies { get; set; }

    public virtual DbSet<TblKhieunai> TblKhieunais { get; set; }

    public virtual DbSet<TblKhoa> TblKhoas { get; set; }

    public virtual DbSet<TblLop> TblLops { get; set; }

    public virtual DbSet<TblMinhChung> TblMinhChungs { get; set; }

    public virtual DbSet<TblMucTieuChi> TblMucTieuChis { get; set; }

    public virtual DbSet<TblNganh> TblNganhs { get; set; }

    public virtual DbSet<TblPhieurenluyen> TblPhieurenluyens { get; set; }

    public virtual DbSet<TblSinhvien> TblSinhviens { get; set; }

    public virtual DbSet<TblTrangthai> TblTrangthais { get; set; }

    public virtual DbSet<TblTroLySv> TblTroLySvs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ASUSFX517\\SQLEXPRESS;Initial Catalog=Renluyen;Integrated Security=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.AdminMenuId).HasName("PK_AdminMenu");

            entity.ToTable("Menu");

            entity.Property(e => e.AdminMenuId).HasColumnName("AdminMenuID");
            entity.Property(e => e.ActionName).HasMaxLength(50);
            entity.Property(e => e.AreaName).HasMaxLength(50);
            entity.Property(e => e.ControllerName).HasMaxLength(50);
            entity.Property(e => e.Icon).HasMaxLength(50);
            entity.Property(e => e.IdName).HasMaxLength(50);
            entity.Property(e => e.ItemName).HasMaxLength(50);
            entity.Property(e => e.ItemTarget).HasMaxLength(50);
        });

        modelBuilder.Entity<TblCanBo>(entity =>
        {
            entity.HasKey(e => e.IdcanBo);

            entity.ToTable("tbl_CanBo");

            entity.Property(e => e.IdcanBo)
                .ValueGeneratedNever()
                .HasColumnName("IDCanBo");
            entity.Property(e => e.Idsinhvien).HasColumnName("IDSinhvien");
            entity.Property(e => e.MoTa).HasMaxLength(255);

            entity.HasOne(d => d.IdsinhvienNavigation).WithMany(p => p.TblCanBos)
                .HasForeignKey(d => d.Idsinhvien)
                .HasConstraintName("FK_tbl_CanBo_tbl_Sinhvien");
        });

        modelBuilder.Entity<TblChitietPhieuRl>(entity =>
        {
            entity.HasKey(e => e.IdchitietPhieuRl);

            entity.ToTable("tbl_ChitietPhieuRL");

            entity.Property(e => e.IdchitietPhieuRl).HasColumnName("IDChitietPhieuRL");
            entity.Property(e => e.IdminhChung).HasColumnName("IDMinhCHung");
            entity.Property(e => e.IdmucTieuChi).HasColumnName("IDMucTieuChi");
            entity.Property(e => e.IdphieuRl).HasColumnName("IDPhieuRL");
            entity.Property(e => e.NgayCapnhat).HasColumnType("datetime");

            entity.HasOne(d => d.IdmucTieuChiNavigation).WithMany(p => p.TblChitietPhieuRls)
                .HasForeignKey(d => d.IdmucTieuChi)
                .HasConstraintName("FK_tbl_ChitietPhieuRL_tbl_MucTieuChi");

            entity.HasOne(d => d.IdphieuRlNavigation).WithMany(p => p.TblChitietPhieuRls)
                .HasForeignKey(d => d.IdphieuRl)
                .HasConstraintName("FK_tbl_ChitietPhieuRL_tbl_Phieurenluyen");
        });

        modelBuilder.Entity<TblChucvu>(entity =>
        {
            entity.HasKey(e => e.Machucvu);

            entity.ToTable("tbl_Chucvu");

            entity.Property(e => e.Machucvu).ValueGeneratedNever();
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.Tenchucvu).HasMaxLength(50);
        });

        modelBuilder.Entity<TblDiemrenluyen>(entity =>
        {
            entity.HasKey(e => new { e.Idsinhvien, e.Idkyhoc });

            entity.ToTable("tbl_Diemrenluyen");

            entity.Property(e => e.Idsinhvien).HasColumnName("IDSinhvien");
            entity.Property(e => e.Idkyhoc).HasColumnName("IDKyhoc");
            entity.Property(e => e.DiemRl).HasColumnName("DiemRL");
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.NgayCapNhat).HasColumnType("datetime");

            entity.HasOne(d => d.IdkyhocNavigation).WithMany(p => p.TblDiemrenluyens)
                .HasForeignKey(d => d.Idkyhoc)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_Diemrenluyen_tbl_HocKy");

            entity.HasOne(d => d.IdsinhvienNavigation).WithMany(p => p.TblDiemrenluyens)
                .HasForeignKey(d => d.Idsinhvien)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_tbl_Diemrenluyen_tbl_Sinhvien");
        });

        modelBuilder.Entity<TblHocKy>(entity =>
        {
            entity.HasKey(e => e.Idkyhoc);

            entity.ToTable("tbl_HocKy");

            entity.Property(e => e.Idkyhoc)
                .ValueGeneratedNever()
                .HasColumnName("IDKyhoc");
            entity.Property(e => e.Mota).HasMaxLength(255);
            entity.Property(e => e.Tenhocky).HasMaxLength(50);
        });

        modelBuilder.Entity<TblKhieunai>(entity =>
        {
            entity.HasKey(e => e.Idkhieunai);

            entity.ToTable("tbl_Khieunai");

            entity.Property(e => e.Idkhieunai)
                .ValueGeneratedNever()
                .HasColumnName("IDKhieunai");
            entity.Property(e => e.Idsinhvien).HasColumnName("IDSinhvien");
            entity.Property(e => e.LoaiKhieunai).HasMaxLength(50);
            entity.Property(e => e.Mota).HasMaxLength(255);
            entity.Property(e => e.Nguoixuly).HasMaxLength(50);
            entity.Property(e => e.Noidung).HasMaxLength(255);
            entity.Property(e => e.Thoigiankhieunai).HasColumnType("datetime");
            entity.Property(e => e.TrangThai).HasMaxLength(30);

            entity.HasOne(d => d.IdsinhvienNavigation).WithMany(p => p.TblKhieunais)
                .HasForeignKey(d => d.Idsinhvien)
                .HasConstraintName("FK_tbl_Khieunai_tbl_Sinhvien");
        });

        modelBuilder.Entity<TblKhoa>(entity =>
        {
            entity.HasKey(e => e.MaKhoa);

            entity.ToTable("tbl_Khoa");

            entity.Property(e => e.MaKhoa).ValueGeneratedNever();
            entity.Property(e => e.TenKhoa).HasMaxLength(255);
        });

        modelBuilder.Entity<TblLop>(entity =>
        {
            entity.HasKey(e => e.MaLop);

            entity.ToTable("tbl_Lop");

            entity.Property(e => e.MaLop).ValueGeneratedNever();
            entity.Property(e => e.TenLop).HasMaxLength(255);
        });

        modelBuilder.Entity<TblMinhChung>(entity =>
        {
            entity.HasKey(e => e.IdminhChung);

            entity.ToTable("tbl_MinhChung");

            entity.Property(e => e.IdminhChung)
                .ValueGeneratedNever()
                .HasColumnName("IDMinhChung");
            entity.Property(e => e.IdchitietPhieuRl).HasColumnName("IDChitietPhieuRL");
            entity.Property(e => e.Link).HasMaxLength(255);
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.NgayDuyet).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");

            entity.HasOne(d => d.IdchitietPhieuRlNavigation).WithMany(p => p.TblMinhChungs)
                .HasForeignKey(d => d.IdchitietPhieuRl)
                .HasConstraintName("FK_tbl_MinhChung_tbl_ChitietPhieuRL");
        });

        modelBuilder.Entity<TblMucTieuChi>(entity =>
        {
            entity.HasKey(e => e.IdmucTieuChi);

            entity.ToTable("tbl_MucTieuChi");

            entity.Property(e => e.IdmucTieuChi).HasColumnName("IDMucTieuChi");
            entity.Property(e => e.Loai)
                .HasMaxLength(20)
                .IsFixedLength();
            entity.Property(e => e.Mota).HasMaxLength(255);
            entity.Property(e => e.NgaySua).HasColumnType("datetime");
            entity.Property(e => e.NgayTao).HasColumnType("datetime");
            entity.Property(e => e.NguoiSua).HasMaxLength(50);
            entity.Property(e => e.NguoiTao).HasMaxLength(50);
        });

        modelBuilder.Entity<TblNganh>(entity =>
        {
            entity.HasKey(e => e.MaNganh);

            entity.ToTable("tbl_Nganh");

            entity.Property(e => e.MaNganh).HasMaxLength(10);
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.TenNganh).HasMaxLength(255);

            entity.HasOne(d => d.MaKhoaNavigation).WithMany(p => p.TblNganhs)
                .HasForeignKey(d => d.MaKhoa)
                .HasConstraintName("FK_tbl_Nganh_tbl_Khoa");
        });

        modelBuilder.Entity<TblPhieurenluyen>(entity =>
        {
            entity.HasKey(e => e.IdphieuRl);

            entity.ToTable("tbl_Phieurenluyen");

            entity.Property(e => e.IdphieuRl).HasColumnName("IDPhieuRL");
            entity.Property(e => e.Idkyhoc).HasColumnName("IDKyhoc");
            entity.Property(e => e.Idsinhvien).HasColumnName("IDSinhvien");
            entity.Property(e => e.Mota).HasMaxLength(255);
            entity.Property(e => e.Nguoicham).HasMaxLength(30);
            entity.Property(e => e.Thoigiannap).HasColumnType("datetime");

            entity.HasOne(d => d.IdkyhocNavigation).WithMany(p => p.TblPhieurenluyens)
                .HasForeignKey(d => d.Idkyhoc)
                .HasConstraintName("FK_tbl_Phieurenluyen_tbl_HocKy");

            entity.HasOne(d => d.IdsinhvienNavigation).WithMany(p => p.TblPhieurenluyens)
                .HasForeignKey(d => d.Idsinhvien)
                .HasConstraintName("FK_tbl_Phieurenluyen_tbl_Sinhvien");
        });

        modelBuilder.Entity<TblSinhvien>(entity =>
        {
            entity.HasKey(e => e.Idsinhvien);

            entity.ToTable("tbl_Sinhvien");

            entity.Property(e => e.Idsinhvien)
                .ValueGeneratedNever()
                .HasColumnName("IDSinhvien");
            entity.Property(e => e.AnhDaiDien).HasMaxLength(255);
            entity.Property(e => e.Cccd)
                .HasMaxLength(12)
                .HasColumnName("CCCD");
            entity.Property(e => e.Diachi).HasMaxLength(255);
            entity.Property(e => e.Ho).HasMaxLength(50);
            entity.Property(e => e.MaNganh).HasMaxLength(10);
            entity.Property(e => e.MatKhau).HasMaxLength(255);
            entity.Property(e => e.Msv)
                .HasMaxLength(16)
                .HasColumnName("MSV");
            entity.Property(e => e.NgayNhapHoc).HasColumnType("datetime");
            entity.Property(e => e.Sodienthoai).HasMaxLength(10);
            entity.Property(e => e.Ten).HasMaxLength(50);
            entity.Property(e => e.TenDem).HasMaxLength(50);

            entity.HasOne(d => d.MaLopNavigation).WithMany(p => p.TblSinhviens)
                .HasForeignKey(d => d.MaLop)
                .HasConstraintName("FK_tbl_Sinhvien_tbl_Lop");

            entity.HasOne(d => d.MaNganhNavigation).WithMany(p => p.TblSinhviens)
                .HasForeignKey(d => d.MaNganh)
                .HasConstraintName("FK_tbl_Sinhvien_tbl_Nganh");

            entity.HasOne(d => d.MachucvuNavigation).WithMany(p => p.TblSinhviens)
                .HasForeignKey(d => d.Machucvu)
                .HasConstraintName("FK_tbl_Sinhvien_tbl_Chucvu");

            entity.HasOne(d => d.MatrangthaiNavigation).WithMany(p => p.TblSinhviens)
                .HasForeignKey(d => d.Matrangthai)
                .HasConstraintName("FK_tbl_Sinhvien_tbl_Trangthai");
        });

        modelBuilder.Entity<TblTrangthai>(entity =>
        {
            entity.HasKey(e => e.Matrangthai);

            entity.ToTable("tbl_Trangthai");

            entity.Property(e => e.Matrangthai).ValueGeneratedNever();
            entity.Property(e => e.MoTa).HasMaxLength(255);
            entity.Property(e => e.Tentrangthai).HasMaxLength(50);
        });

        modelBuilder.Entity<TblTroLySv>(entity =>
        {
            entity.HasKey(e => e.IdtroLySv);

            entity.ToTable("tbl_TroLySV");

            entity.Property(e => e.IdtroLySv)
                .ValueGeneratedNever()
                .HasColumnName("IDTroLySV");
            entity.Property(e => e.TenTlsv)
                .HasMaxLength(255)
                .HasColumnName("TenTLSV");

            entity.HasOne(d => d.MaKhoaNavigation).WithMany(p => p.TblTroLySvs)
                .HasForeignKey(d => d.MaKhoa)
                .HasConstraintName("FK_tbl_TroLySV_tbl_Khoa");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
