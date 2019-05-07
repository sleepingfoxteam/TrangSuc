-- tao database --
create database TRANGSUC;
use TRANGSUC;

-- Tao tables --

-- Table Loai Trang Suc --
create table LoaiTrangSuc(
	ID nvarchar(50),
	TenLoai nvarchar(200),
	constraint PK_LOAITRANGSUC primary key (ID)
);

-- Table Trang Suc --
create table TrangSuc(
	ID nvarchar(50),
	TenTrangSuc nvarchar(200),
	LoaiTrangSuc nvarchar(50),
	GiaCong int,
	KhoiLuongTinh float,
	SoHat int,
	GiaHat int,
	HinhAnh text,
	constraint PK_TRANGSUC primary key (ID),
	constraint FK_TRANGSUC_LOAITRANGSUC foreign key (LoaiTrangSuc) references LoaiTrangSuc(ID)
);

-- table nhan vien --
create table NhanVien(
	ID nvarchar(50),
	HoTen nvarchar(200),
	DiaChi text,
	Sdt nvarchar(20),
	CMND nvarchar(20),
	NgaySinh date,
	constraint PK_NHANVIEN primary key (ID)
);

-- table Hoa Don --
create table HoaDon(
	ID nvarchar(50),
	NgayLap date,
	NgayThanhToan date,
	NguoiLap nvarchar(50),
	TongTien int,
	HoTenKhachHang nvarchar(200),
	SdtKhachHang nvarchar(20),
	DiaChiKhachHang text,
	constraint PK_HOADON primary key (ID)
);

alter table dbo.HoaDon add TinhTrang int;

-- table Chi Tiet Hoa Don -- trang Suc --
create table CTHD_TrangSuc(
	IDHoaDon nvarchar(50),
	IDTrangSuc nvarchar(50),
	SoLuong int,
	Gia int,
	constraint PK_CTHD_TRANGSUC primary key (IDHoaDon,IDTrangSuc),
	constraint FK_CTHD_HOADON foreign key (IDHoaDon) references HOADON(ID) ON DELETE CASCADE,
	constraint FK_CTHD_TRANGSUC foreign key (IDTrangSuc) references TRANGSUC(ID) ON DELETE CASCADE
);

-- them thong tin cho bang loai trang suc --
insert into LoaiTrangSuc values ('L001',N'Vòng Tay');
insert into LoaiTrangSuc values ('L002',N'Vòng Cổ');
insert into LoaiTrangSuc values ('L003',N'Dây Chuyền');
insert into LoaiTrangSuc values ('L004',N'Lắc Tay');
insert into LoaiTrangSuc values ('L005',N'Lắc Chân');
insert into LoaiTrangSuc values ('L006',N'Nhẫn');

-- them thong tin cho bang nhan vien --
insert into NhanVien values ('NV001',N'Trần Phát Hưng',N'Cái Tàu Hạ, Châu Thành, Đồng Tháp','0376389227','341858886','1998-01-08');
insert into NhanVien values ('NV002',N'Trần Phát Hiệp',N'Cái Tàu Hạ, Châu Thành, Đồng Tháp','0919202379','340735965','1971-01-25');
insert into NhanVien values ('NV003',N'Nguyễn Thị Phượng',N'Cái Tàu Hạ, Châu Thành, Đồng Tháp','0376234227','321354689','1974-10-10');

