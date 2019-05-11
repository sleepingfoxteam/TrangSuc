$(document).ready(function () {
    InitialEvents();
});
function InitialEvents() {
    InitialAddToCartEvent();
    InitialDatHangEvent();
    InitialThemTrangSucVaoPhieu();
    InitialHoanThanhThem();
}

function InitialHoanThanhThem() {
    $("#btn-hoanthanhchon").click(function () {
        let trangsucs = [];
        let tongtien = 0;
        $("[name='chon']:checked").each(function (index, el) {
            let parent = $(el).parent();
            let mats = parent.find(".id").text();
            let tents = parent.find(".tents").text();
            let gia = parent.find(".giathanh").text().split(",").join("");
            let soluong = parent.find("[name='soluong']").val();
            tongtien += soluong * parseInt(gia);
            let item_object = {
                mats,
                tents,
                soluong,
                giathanh: gia
            }
            trangsucs.push(item_object);
        });
        let send_object = {
            trangsucs,
            tongtien
        }
        let send_json = JSON.stringify(send_object);
        location.href = location.href.split("ThemSanPhamVaoPhieu")[0] + "HoanThanhChon?jsondata=" + send_json;
    });
}

function InitialThemTrangSucVaoPhieu() {
    $("#btn-addtrangsuc").click(function () {
        location.href = location.href.split("Create")[0] + "ThemSanPhamVaoPhieu";
    });
}
function InitialDatHangEvent() {
    $("#btn-dathang").click(function () {
        let giohangitems = $("#list-giohang").find(".item");
        let giohang = [];
        giohangitems.each(function (index, el) {
            let _ = $(el);
            let soluong = _.find('.soluong').text();
            let id = _.find('.id').text();
            let tentrangsuc = _.find('.tentrangsuc').text();
            let giathanh = _.find('.giathanh').text().split(",").join("");
            let object = {
                id,
                tentrangsuc,
                soluong,
                giathanh
            }
            giohang.push(object);
        });
        let formkhachhang = $("#thongtinkhachhang");
        let form_infor = {
            hoten: formkhachhang.find('#hoten').val(),
            sdt: formkhachhang.find('#sodienthoai').val(),
            diachi: formkhachhang.find('#diachi').val()
        }
        let tonggiatri = $('#tonggiatri').find('.tonggiatri-value').text().split(",").join("");
        let send_object = {
            giohang,
            khachhang: form_infor,
            tonggiatri
        }
        $.ajax({
            type: 'POST',
            method: 'POST',
            url:'../HoaDons/TaoHoaDon',
            data: {
                jsondata: JSON.stringify(send_object)
            },
            success: function (result) {
                if (result == 1) {
                    alert("Dat Hang Thanh Cong, Cho Xac Nhan Nhe ^_^ !");
                }
                else {
                    alert(result);
                }
            }
        })
    });
}
function InitialAddToCartEvent() {
    $('.btn-cart').click(function () {
        let id = $(this).data('id');
        $.ajax({
            type: "GET",
            method: "GET",
            url: '../../../Index/AddToCart?id=' + id,
            success: function (result) {
                if (result == 1) {
                    alert('thanh cong');
                }
                else {
                    alert('khong thanh cong');
                }
            }
        });
    })
}