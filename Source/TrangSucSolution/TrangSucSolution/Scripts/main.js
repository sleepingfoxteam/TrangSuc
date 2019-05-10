$(document).ready(function () {
    InitialEvents();
});
function InitialEvents() {
    InitialAddToCartEvent();
}

function InitialAddToCartEvent() {
    $('.btn-cart').click(function () {
        let id = $(this).data('id');
        $.ajax({
            type: "GET",
            method: "GET",
            url: 'Index/AddToCart?id=' + id,
            success: function (result) {
                if (result === 1) {
                    alert('Thành công');
                }
                else {
                    alert("không thành công");
                }
            }
        });
    })
}