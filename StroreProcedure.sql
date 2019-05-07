create or alter procedure SP_INSERT_NHANVIEN
	@HoTen nvarchar(200),
	@DiaChi text,
	@sdt nvarchar(20),
	@CMND nvarchar(20),
	@NgaySinh date
AS
BEGIN
	DECLARE @ret int, @ID nvarchar(50),@num_id nvarchar(50);
	-- lay ID lon nhat --
	SET @ID = (SELECT TOP 1 nv.ID FROM dbo.NhanVien nv ORDER BY nv.ID DESC);
	IF @ID = NULL
	BEGIN
		SET @ID = 'NV000';
	END
	SET @num_id = SUBSTRING(@ID,3,3);
	SET @ret = CONVERT(int,@num_id);
	SET @ret = @ret + 1;
	SET @ID = CONCAT('NV00',@ret);
	--
	BEGIN TRY
		INSERT INTO dbo.NhanVien VALUES(@ID,@HoTen,@DiaChi,@sdt,@CMND,@NgaySinh);
		SET @ret = 1;
	END TRY
	BEGIN CATCH
		SET @ret = -1
	END CATCH
	--
	IF @ret = -1
	BEGIN
		RETURN -1;
	END
	--
	EXEC @ret = sp_addlogin @ID, '123456';
	IF @ret = -1
	BEGIN
		RETURN -1
	END

	EXEC @ret = sp_adduser @ID,@ID;
	IF @ret = -1
	BEGIN
		RETURN -1
	END
	--
	EXEC @ret = sp_addrolemember 'NhanVien',@ID;
	IF @ret = -1
	BEGIN
		return -1
	END
	--
	EXEC @ret = sp_addsrvrolemember @ID, 'processadmin';
	IF @ret = -1
	BEGIN
		return -1
	END
	RETURN 1
END

-- xoa het nhan vien trong bang NHANVIEN --
DELETE FROM dbo.NhanVien

--  them lai 3 nhan vien 
EXEC SP_INSERT_NHANVIEN N'Trần Phát Hưng',N'Cái Tàu Hạ, Châu Thành, Đồng Tháp', '0376389227','341858886','1998-01-08';
EXEC SP_INSERT_NHANVIEN N'Trần Phát Hiệp',N'Cái Tàu Hạ, Châu Thành, Đồng Tháp', '0919202379','340735965','1971-01-25';
EXEC SP_INSERT_NHANVIEN N'Nguyễn Thị Phượng',N'Cái Tàu Hạ, Châu Thành, Đồng Tháp', '0376234227','340265987','1974-10-10';
-- UPDATE NHANVIEN
create or alter procedure SP_UPDATE_NHANVIEN
	@HoTen nvarchar(200),
	@DiaChi text,
	@sdt nvarchar(20),
	@NgaySinh date,
	@ID nvarchar(50)
AS
BEGIN
	BEGIN TRY
		UPDATE dbo.NhanVien 
			SET dbo.NhanVien.HoTen = @HoTen,
				dbo.NhanVien.DiaChi = @DiaChi,
				dbo.NhanVien.NgaySinh = @NgaySinh,
				dbo.NhanVien.Sdt = @sdt
			WHERE dbo.NhanVien.ID = @ID
		RETURN 1
	END TRY
	BEGIN CATCH
		RETURN -1
	END CATCH
END
-- DELETE NHANVIEN
create procedure SP_DELETE_NHANVIEN
	@ID nvarchar(50)
AS
BEGIN
	BEGIN TRY
		DELETE FROM dbo.NhanVien WHERE dbo.NhanVien.ID = @ID;
		EXEC sp_dropuser @ID;
		EXEC sp_droplogin @ID;
		RETURN 1
	END TRY
	BEGIN CATCH
		RETURN -1
	END CATCH
END

create or alter procedure SP_INSERT_TRANGSUC
	@TenTrangSuc nvarchar(200),
	@LoaiTrangSuc nvarchar(50),
	@GiaCong int,
	@KhoiLuongTinh float,
	@SoHat int,
	@GiaHat int,
	@HinhAnh text
AS
BEGIN
	DECLARE @ID nvarchar(50),@len int,@ID_num int;
	SET @ID = (SELECT TOP 1 ID FROM dbo.TrangSuc ORDER BY dbo.TrangSuc.ID);
	IF @ID = NULL
	BEGIN
		SET @ID = 'TS0';
	END
	SET @len = LEN(@ID) - 2;
	SET @ID_num = CONVERT(int,SUBSTRING(@ID,3,@len)) + 1;
	SET @ID = CONCAT('TS',@ID_num);
	INSERT INTO dbo.TrangSuc VALUES (@ID,@TenTrangSuc,@LoaiTrangSuc,@GiaCong,@KhoiLuongTinh,@SoHat,@GiaHat,@HinhAnh);
END

create or alter procedure SP_UPDATE_TRANGSUC
	@ID nvarchar(50),
	@GiaCong int,
	@GiaHat int,
	@HinhAnh text
AS
BEGIN
	BEGIN TRY
		UPDATE dbo.TrangSuc
		SET dbo.TrangSuc.GiaCong = @GiaCong,
			dbo.TrangSuc.GiaHat = @GiaHat,
			dbo.TrangSuc.HinhAnh = @HinhAnh
		WHERE dbo.TrangSuc.ID = @ID
		RETURN 1
	END TRY
	BEGIN CATCH
		RETURN -1
	END CATCH
END

create or alter procedure SP_DELETE_TRANGSUC
	@ID nvarchar(50)
AS
BEGIN
	DELETE FROM dbo.TrangSuc WHERE dbo.TrangSuc.ID = @ID;
END

create or alter procedure SP_INSERT_HOADON
	@ThanhToanLuon bit,
	@NguoiLap nvarchar(50),
	@TongTien int,
	@HoTenKhach nvarchar(200),
	@stdKhach nvarchar(20),
	@DiachiKhach text,
	@TinhTrang int
AS
BEGIN
	DECLARE @today date, @ID nvarchar(50), @ID_Num int , @len int;
	SET @today = GETDATE();
	SET @ID = (SELECT ID FROM dbo.HoaDon WHERE dbo.HoaDon.ID LIKE CONCAT(@today,'%'))
	IF @ID = NULL
	BEGIN
		SET @ID = CONCAT(@today,'000')
	END
	SET @len = LEN(@ID) - LEN(@today);
	SET @ID_Num = CONVERT(int,SUBSTRING(@ID,11,@len)) + 1;
	SET @ID = CONCAT(@today,@ID_num);
	IF @ThanhToanLuon = 0
	BEGIN
		BEGIN TRY
			INSERT INTO dbo.HoaDon VALUES(@ID,@today,NULL,@NguoiLap,@TongTien,@HoTenKhach,@stdKhach,@DiachiKhach,@TinhTrang);
			RETURN 1
		END TRY
		BEGIN CATCH
			RETURN -1
		END CATCH
	END
	IF @ThanhToanLuon = 1
	BEGIN
		BEGIN TRY
			INSERT INTO dbo.HoaDon VALUES(@ID,@today,@today,@NguoiLap,@TongTien,@HoTenKhach,@stdKhach,@DiachiKhach,@TinhTrang);
			RETURN 1
		END TRY
		BEGIN CATCH
			RETURN -1
		END CATCH
	END
END

create procedure SP_UPDATE_HOADON
	@NgayThanhToan date,
	@TinhTrang int,
	@ID nvarchar(50)
AS
BEGIN
	BEGIN TRY
		UPDATE dbo.HoaDon 
		SET dbo.HoaDon.NgayThanhToan = @NgayThanhToan,
			dbo.HoaDon.TinhTrang = @TinhTrang
		WHERE dbo.HoaDon.ID = @ID
		RETURN 1
	END TRY
	BEGIN CATCH
		RETURN -1
	END CATCH
END

create procedure SP_DELETE_HOADON
	@ID nvarchar(50)
AS
BEGIN
	BEGIN TRY
		DELETE FROM dbo.HoaDon WHERE dbo.HoaDon.ID = @ID;
		RETURN 1;
	END TRY
	BEGIN CATCH
		RETURN -1;
	END CATCH
END

create procedure SP_INSERT_CTHD
	@IDHD nvarchar(50),
	@IDTS nvarchar(50),
	@Gia int,
	@SoLuong int
AS
BEGIN
	BEGIN TRY
		INSERT INTO dbo.CTHD_TrangSuc VALUES (@IDHD,@IDTS,@SoLuong,@Gia)
		RETURN 1;
	END TRY
	BEGIN CATCH
		RETURN -1;
	END CATCH
END