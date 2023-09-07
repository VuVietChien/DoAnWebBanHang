$(document).ready(function () {
    ShowCount();
    
    $('body').on('click', '.btnAddToCart', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = 1;
        var tQuantity = $("#quantity_value").text();
        if (tQuantity != "") {
            quantity = parseInt(tQuantity);
        }

        $.ajax({
            url: '/shoppingcart/addtocart',
            type: 'POST',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                if (rs.Success) {
                    alert(rs.msg);
                    $("#checkout_items").html(rs.count);
                }
            }
        });

    });

    $('body').on('click', '.btnUpdate', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var quantity = $("#Quantity_" + id).val();
        Update(id, quantity);

    });
    $('body').on('click', '.btnDelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm("Bạn có chắc muốn xóa sản phẩm này khỏi giỏ hàng không ?");
        if (conf) {
            $.ajax({
                url: '/shoppingcart/delete',
                type: 'POST',
                data: { id: id },
                success: function (rs) {
                    if (rs.Success) {
                        $("#trow_" + id).remove();
                        $("#checkout_items").html(rs.count);
                        LoadCart();
                    }
                }
            });
        }
       

    });
    $('body').on('click', '.btnDeleteAll', function (e) {
        e.preventDefault();
        var id = $(this).data('id');
        var conf = confirm("Bạn có chắc muốn xóa hết sản phẩm trong giỏ hàng không ?");
        if (conf) {
            DeleteAll()
        }
    });


});

function ShowCount() {
    $.ajax({
        url: '/shoppingcart/ShowCount',
        type: 'GET',
        success: function (rs) {
            $("#checkout_items").html(rs.count);
        }
    });
}

function DeleteAll() {
    $.ajax({
        url: '/shoppingcart/deleteAll',
        type: 'POST',
        success: function (rs) {
            if (rs.Success) {
                LoadCart()
            }
        }
    });
}

function Update(id, quantity) {
    $.ajax({
        url: '/shoppingcart/Update',
        type: 'POST',
        data: { id: id, quantity: quantity },
        success: function (rs) {
            if (rs.Success) {
                LoadCart()
            }
        }
    });
}

function LoadCart() {
    $.ajax({
        url: '/shoppingcart/Partial_Item_Cart',
        type: 'GET',
        success: function (rs) {
            $("#load-data").html(rs);
        }
    });
}