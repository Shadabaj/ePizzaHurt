
function AddToCart(ItemId, Name, UnitPrice, Quantity) {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: '/Cart/AddToCart/' + ItemId + "/" + UnitPrice + "/" + Quantity,
        success: function (d) {
            if (d.length > 0) {
                var data = JSON.parse(d);
                var message = '<strong>' + Name + '</strong> Added to <a href="/cart">Cart</a> Successfully!';
               // confirm(message);
                //$('#CartCounter').html();
                $('#CartCounter').text(data.CartItems.length);
                $('#ToastCart > .toast-body').html(message);
                $('#ToastCart').show();  // Use Bootstrap's toast show method
                setTimeout(function () {
                    $('#ToastCart').toast('hide');
                }, 4000);
            }
        },
        error: function (xhr, status, error) {
            console.error("Error: " + error);
            console.error("Status: " + status);
            console.error(xhr.responseText); // Log the server error message
            alert("Failed to add item to cart. Please try again.");
        }
    });
}


$(document).ready(function () {
    $.ajax({
        type: "GET",
        contentType: "application/json; charset=utf-8",
        url: "/Cart/GetCartCount",
        dataType: "json",
        success: function (data) {
            $('#CartCounter').text(data);
        },
        error: function (result) {

        }
    });
});


function DeleteItem(Id) {
    if (Id > 0) {
        $.ajax({
            type: "GET",
            contentType: "application/json; charset=utf-8",
            url: "/Cart/Delete/" + Id,
            success: function (data) {
                if (data >= 0) {
                    location.reload();// // Agar item successfully delete ho jaata hai, toh page reload hoga
                }
            }, error: function (result) {

            }
        });
    }
}



function UpdateQuantity(id, Total, Quantity) {
    if (id > 0 && Quantity > 0) {
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: "/Cart/UpdateQuantity/" + id + "/" + Quantity,  // Corrected syntax here
            success: function (data) {
                if (data > 0) {
                    location.reload();
                }
            },
            error: function (result) {
                console.error("Failed to update quantity.");
            }
        });
    }
    else if (id > 0 && Quantity < 0 && Total > 1) {
        $.ajax({
            type: "GET",
            contentType: "application/json",
            url: "/Cart/UpdateQuantity/" + id + "/" + Quantity,  // Corrected syntax here
            success: function (data) {  // Changed 'data' to 'response'
                if (data > 0) {
                    location.reload();
                }
            },
            error: function (result) {
                console.error("Failed to update quantity.");
            }
        });
    }
}
