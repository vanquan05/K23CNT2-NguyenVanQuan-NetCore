	-- Tạo cơ sở dữ liệu
CREATE DATABASE TRANGQUANAO;
GO

USE TRANGQUANAO;
GO

-- Bảng Loai
CREATE TABLE [dbo].[Loai](
    [MaLoai] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [TenLoai] NVARCHAR(50) NOT NULL,
    [TenLoaiAlias] NVARCHAR(50) NULL,
    [MoTa] NVARCHAR(MAX) NULL,
    [Hinh] NVARCHAR(50) NULL
);

-- Bảng NhaCungCap
CREATE TABLE [dbo].[NhaCungCap](
    [MaNCC] NVARCHAR(50) NOT NULL PRIMARY KEY,
    [TenCongTy] NVARCHAR(50) NOT NULL,
    [Logo] NVARCHAR(50) NOT NULL,
    [NguoiLienLac] NVARCHAR(50) NULL,
    [Email] NVARCHAR(50) NOT NULL,
    [DienThoai] NVARCHAR(50) NOT NULL,
    [DiaChi] NVARCHAR(50) NULL,
    [MoTa] NVARCHAR(MAX) NULL
);

-- Bảng HangHoa
CREATE TABLE [dbo].[HangHoa](
    [MaHH] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [TenHH] NVARCHAR(50) NOT NULL,
    [TenAlias] NVARCHAR(50) NULL,
    [MaLoai] INT NOT NULL,
    [MoTaDonVi] NVARCHAR(50) NULL,
    [DonGia] FLOAT NULL,
    [Hinh] NVARCHAR(50) NULL,
    [NgaySX] DATETIME NOT NULL,
    [GiamGia] FLOAT NOT NULL,
    [SoLanXem] INT NOT NULL,
    [MoTa] NVARCHAR(MAX) NULL,
    [MaNCC] NVARCHAR(50) NOT NULL,
    CONSTRAINT FK_HangHoa_Loai FOREIGN KEY (MaLoai) REFERENCES Loai(MaLoai),
    CONSTRAINT FK_HangHoa_NCC FOREIGN KEY (MaNCC) REFERENCES NhaCungCap(MaNCC)
);

-- Bảng KhachHang
CREATE TABLE [dbo].[KhachHang](
    [MaKH] NVARCHAR(20) NOT NULL PRIMARY KEY,
    [MatKhau] NVARCHAR(50) NULL,
    [HoTen] NVARCHAR(50) NOT NULL,
    [GioiTinh] BIT NOT NULL,
    [NgaySinh] DATETIME NOT NULL,
    [DiaChi] NVARCHAR(60) NULL,
    [DienThoai] NVARCHAR(24) NULL,
    [Email] NVARCHAR(50) NOT NULL,
    [Hinh] NVARCHAR(50) NULL,
    [Hieuluc] BIT NOT NULL,
    [VaiTro] INT NOT NULL,
    [RandomKey] VARCHAR(50) NULL
);

-- Bảng NhanVien
CREATE TABLE [dbo].[NhanVien](
    [MaNV] NVARCHAR(50) NOT NULL PRIMARY KEY,
    [HoTen] NVARCHAR(50) NOT NULL,
    [Email] NVARCHAR(50) NOT NULL,
    [MatKhau] NVARCHAR(50) NULL
);

-- Bảng HoaDon
CREATE TABLE [dbo].[HoaDon](
    [MaHD] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [MaKH] NVARCHAR(20) NOT NULL,
    [NgayDat] DATETIME NOT NULL,
    [NgayCan] DATETIME NULL,
    [NgayGiao] DATETIME NULL,
    [HoTen] NVARCHAR(50) NULL,
    [DiaChi] NVARCHAR(60) NULL,

    [CachThanhToan] NVARCHAR(50) NOT NULL,
    [CachVanChuyen] NVARCHAR(50) NOT NULL,
    [PhiVanChuyen] FLOAT NOT NULL,
    [MaTrangThai] INT NOT NULL,
    [MaNV] NVARCHAR(50) NULL,
    [GhiChu] NVARCHAR(50) NULL,
    CONSTRAINT FK_HoaDon_KhachHang FOREIGN KEY (MaKH) REFERENCES KhachHang(MaKH),
    CONSTRAINT FK_HoaDon_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);

-- Bảng ChiTietHD
CREATE TABLE [dbo].[ChiTietHD](
    [MaCT] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [MaHD] INT NOT NULL,
    [MaHH] INT NOT NULL,
    [DonGia] FLOAT NOT NULL,
    [SoLuong] INT NOT NULL,
    [GiamGia] FLOAT NOT NULL,
    CONSTRAINT FK_ChiTietHD_HoaDon FOREIGN KEY (MaHD) REFERENCES HoaDon(MaHD),
    CONSTRAINT FK_ChiTietHD_HangHoa FOREIGN KEY (MaHH) REFERENCES HangHoa(MaHH)
);





-- ========== 1. Bảng Loai ==========
INSERT INTO Loai (TenLoai, TenLoaiAlias, MoTa, Hinh) VALUES
(N'Áo thun', N'ao-thun', N'Các loại áo thun nam nữ', N'aothun.png'),
(N'Áo sơ mi', N'ao-so-mi', N'Áo sơ mi công sở, đi chơi', N'aosomi.png'),
(N'Quần jean', N'quan-jean', N'Quần jean nam nữ', N'jean.png'),
(N'Quần tây', N'quan-tay', N'Quần tây công sở', N'quantay.png'),
(N'Váy', N'vay', N'Váy đầm dạ hội, công sở', N'vay.png'),
(N'Giày thể thao', N'giay-the-thao', N'Giày sneaker, chạy bộ', N'giaythethao.png'),
(N'Giày da', N'giay-da', N'Giày da công sở', N'giayda.png'),
(N'Túi xách', N'tui-xach', N'Túi xách thời trang', N'tuixach.png'),
(N'Phụ kiện', N'phu-kien', N'Nón, thắt lưng, ví', N'phukien.png'),
(N'Áo khoác', N'ao-khoac', N'Áo khoác mùa đông', N'aokhoac.png');

-- ========== 2. Bảng NhaCungCap ==========
INSERT INTO NhaCungCap (MaNCC, TenCongTy, Logo, NguoiLienLac, Email, DienThoai, DiaChi, MoTa) VALUES
(N'NCC01', N'Công ty May 10', N'may10.png', N'Nguyễn A', N'may10@mail.com', N'0909000001', N'Hà Nội', N'Nhà sản xuất quần áo'),
(N'NCC02', N'Canifa', N'canifa.png', N'Lê B', N'canifa@mail.com', N'0909000002', N'Hồ Chí Minh', N'Nhà cung cấp thời trang'),
(N'NCC03', N'Routine', N'routine.png', N'Phạm C', N'routine@mail.com', N'0909000003', N'Hà Nội', N'Thời trang nam nữ'),
(N'NCC04', N'IVY Moda', N'ivy.png', N'Trần D', N'ivy@mail.com', N'0909000004', N'Hà Nội', N'Thời trang công sở'),
(N'NCC05', N'Coolmate', N'coolmate.png', N'Võ E', N'coolmate@mail.com', N'0909000005', N'HCM', N'Áo thun, basic wear'),
(N'NCC06', N'Yame', N'yame.png', N'Lý F', N'yame@mail.com', N'0909000006', N'HCM', N'Streetwear'),
(N'NCC07', N'Ananas', N'ananas.png', N'Ngô G', N'ananas@mail.com', N'0909000007', N'HCM', N'Giày sneaker Việt Nam'),
(N'NCC08', N'Biti''s', N'bitis.png', N'Đỗ H', N'bitis@mail.com', N'0909000008', N'HN', N'Giày dép thể thao'),
(N'NCC09', N'Pedro', N'pedro.png', N'Vũ I', N'pedro@mail.com', N'0909000009', N'HCM', N'Túi xách, giày da'),
(N'NCC10', N'Belluni', N'belluni.png', N'Phan J', N'belluni@mail.com', N'0909000010', N'HN', N'Quần tây, sơ mi cao cấp');

-- ========== 3. Bảng HangHoa ==========
INSERT INTO HangHoa (TenHH, TenAlias, MaLoai, MoTaDonVi, DonGia, Hinh, NgaySX, GiamGia, SoLanXem, MoTa, MaNCC) VALUES
(N'Áo thun basic nam', N'ao-thun-basic-nam', 1, N'Cái', 150000, N'aothun1.png', '2023-05-01', 0.1, 120, N'Áo thun cotton 100%', N'NCC05'),
(N'Áo thun oversize nữ', N'ao-thun-oversize-nu', 1, N'Cái', 170000, N'aothun2.png', '2023-05-01', 0, 80, N'Form rộng, trẻ trung', N'NCC06'),
(N'Áo sơ mi trắng nam', N'ao-so-mi-trang-nam', 2, N'Cái', 300000, N'aosomi1.png', '2023-04-20', 0, 150, N'Áo sơ mi công sở', N'NCC10'),
(N'Quần jean slimfit', N'quan-jean-slimfit', 3, N'Cái', 450000, N'jean1.png', '2023-04-10', 0.05, 90, N'Jean co giãn', N'NCC03'),
(N'Quần tây nam đen', N'quan-tay-nam-den', 4, N'Cái', 500000, N'quantay1.png', '2023-03-01', 0, 60, N'Quần tây cao cấp', N'NCC10'),
(N'Đầm dự tiệc đỏ', N'dam-du-tiec-do', 5, N'Cái', 700000, N'vay1.png', '2023-02-10', 0.15, 40, N'Đầm dạ hội sang trọng', N'NCC04'),
(N'Giày sneaker classic', N'giay-sneaker-classic', 6, N'Đôi', 850000, N'sneaker1.png', '2023-03-15', 0, 200, N'Giày unisex', N'NCC07'),
(N'Giày da công sở', N'giay-da-cong-so', 7, N'Đôi', 1200000, N'giayda1.png', '2023-02-20', 0.05, 70, N'Giày da thật', N'NCC09'),
(N'Túi xách mini nữ', N'tui-xach-mini-nu', 8, N'Cái', 900000, N'tuixach1.png', '2023-04-25', 0, 50, N'Túi nhỏ xinh xắn', N'NCC09'),
(N'Áo khoác bomber', N'ao-khoac-bomber', 10, N'Cái', 650000, N'aokhoac1.png', '2023-01-15', 0.1, 100, N'Áo khoác bomber nam nữ', N'NCC06');

-- ========== 4. Bảng KhachHang ==========
INSERT INTO KhachHang (MaKH, MatKhau, HoTen, GioiTinh, NgaySinh, DiaChi, DienThoai, Email, Hinh, Hieuluc, VaiTro, RandomKey) VALUES
(N'KH001', N'123456', N'Nguyễn Minh', 1, '1995-05-20', N'Hà Nội', N'0901111111', N'minh@mail.com', NULL, 1, 0, NULL),
(N'KH002', N'123456', N'Lê Lan', 0, '1996-06-15', N'HCM', N'0902222222', N'lan@mail.com', NULL, 1, 0, NULL),
(N'KH003', N'123456', N'Trần Duy', 1, '1990-03-10', N'Hải Phòng', N'0903333333', N'duy@mail.com', NULL, 1, 0, NULL),
(N'KH004', N'123456', N'Phạm Hoa', 0, '1998-12-01', N'Đà Nẵng', N'0904444444', N'hoa@mail.com', NULL, 1, 0, NULL),
(N'KH005', N'123456', N'Ngô Quang', 1, '1997-09-09', N'Huế', N'0905555555', N'quang@mail.com', NULL, 1, 0, NULL),
(N'KH006', N'123456', N'Hoàng Mai', 0, '1999-07-07', N'Nghệ An', N'0906666666', N'mai@mail.com', NULL, 1, 0, NULL),
(N'KH007', N'123456', N'Vũ Nam', 1, '1993-08-08', N'Hà Nội', N'0907777777', N'nam@mail.com', NULL, 1, 0, NULL),
(N'KH008', N'123456', N'Phan Hà', 0, '1992-11-11', N'Đà Nẵng', N'0908888888', N'ha@mail.com', NULL, 1, 0, NULL),
(N'KH009', N'123456', N'Lý Bình', 1, '1994-10-10', N'HCM', N'0909999999', N'binh@mail.com', NULL, 1, 0, NULL),
(N'KH010', N'123456', N'Đỗ Thảo', 0, '1991-01-01', N'Hà Nội', N'0910000000', N'thao@mail.com', NULL, 1, 0, NULL);

-- ========== 5. Bảng NhanVien ==========
INSERT INTO NhanVien (MaNV, HoTen, Email, MatKhau) VALUES
(N'NV01', N'Nguyễn Admin', N'admin@mail.com', N'123456'),
(N'NV02', N'Lê Thu', N'nv2@mail.com', N'123456'),
(N'NV03', N'Phạm Hùng', N'nv3@mail.com', N'123456'),
(N'NV04', N'Trần Nga', N'nv4@mail.com', N'123456'),
(N'NV05', N'Vũ Kiên', N'nv5@mail.com', N'123456'),
(N'NV06', N'Hoàng Oanh', N'nv6@mail.com', N'123456'),
(N'NV07', N'Lý Phong', N'nv7@mail.com', N'123456'),
(N'NV08', N'Ngô Hiền', N'nv8@mail.com', N'123456'),
(N'NV09', N'Đỗ Dũng', N'nv9@mail.com', N'123456'),
(N'NV10', N'Phan Khoa', N'nv10@mail.com', N'123456');

-- ========== 6. Bảng HoaDon ==========
INSERT INTO HoaDon (MaKH, NgayDat, NgayCan, NgayGiao, HoTen, DiaChi, CachThanhToan, CachVanChuyen, PhiVanChuyen, MaTrangThai, MaNV, GhiChu) VALUES
(N'KH001', GETDATE(), NULL, NULL, N'Nguyễn Minh', N'Hà Nội', N'COD', N'Giao nhanh', 30000, 1, N'NV01', NULL),
(N'KH002', GETDATE(), NULL, NULL, N'Lê Lan', N'HCM', N'COD', N'Giao nhanh', 30000, 1, N'NV02', NULL),
(N'KH003', GETDATE(), NULL, NULL, N'Trần Duy', N'Hải Phòng', N'Banking', N'Giao tiêu chuẩn', 20000, 1, N'NV03', NULL),
(N'KH004', GETDATE(), NULL, NULL, N'Phạm Hoa', N'Đà Nẵng', N'COD', N'Giao tiêu chuẩn', 20000, 1, N'NV04', NULL),
(N'KH005', GETDATE(), NULL, NULL, N'Ngô Quang', N'Huế', N'COD', N'Giao nhanh', 30000, 1, N'NV05', NULL),
(N'KH006', GETDATE(), NULL, NULL, N'Hoàng Mai', N'Nghệ An', N'Banking', N'Giao tiêu chuẩn', 20000, 1, N'NV06', NULL),
(N'KH007', GETDATE(), NULL, NULL, N'Vũ Nam', N'Hà Nội', N'COD', N'Giao nhanh', 30000, 1, N'NV07', NULL),
(N'KH008', GETDATE(), NULL, NULL, N'Phan Hà', N'Đà Nẵng', N'COD', N'Giao nhanh', 30000, 1, N'NV08', NULL),
(N'KH009', GETDATE(), NULL, NULL, N'Lý Bình', N'HCM', N'Banking', N'Giao tiêu chuẩn', 20000, 1, N'NV09', NULL),
(N'KH010', GETDATE(), NULL, NULL, N'Đỗ Thảo', N'Hà Nội', N'COD', N'Giao nhanh', 30000, 1, N'NV10', NULL);

-- ========== 7. Bảng ChiTietHD ==========
INSERT INTO ChiTietHD (MaHD, MaHH, DonGia, SoLuong, GiamGia) VALUES
(1, 1, 150000, 2, 0.1),
(1, 7, 850000, 1, 0),
(2, 2, 170000, 1, 0),
(3, 3, 300000, 2, 0),
(3, 4, 450000, 1, 0.05),
(4, 5, 500000, 1, 0),
(5, 6, 700000, 1, 0.15),
(6, 8, 1200000, 1, 0.05),
(7, 9, 900000, 1, 0),
(8, 10, 650000, 1, 0.1);
