$(document).ready(function () {

    const toast = function (message) {
        Swal.fire({
            toast: true,
            title: message,
            icon: 'success',
            position: "bottom",
            timerProgressBar: true,
            showConfirmButton: false
        });
    }

    // Add to Cart
    $(document).on('click', '.addToCart', function (e) {
        e.preventDefault();
        const id = $(this).data("id");

        axios.post('/Cart/AddToCart', {
            id
        })
            .then(function (response) {
                toast(response.data.Message)
                $('#cart-status').text(response.data.CartCount);
            })
            .catch(function (error) {
                console.log(error);
            });
    });

    // Remove from cart
    $('.removeFromCart').click(function (e) {
        e.preventDefault();
        const id = $(this).data("id");

        axios.post('/Cart/RemoveFromCart', {
            id
        })
            .then(function (response) {
                $(`#row-${response.data.DeleteId}`).fadeOut('slow');
                $('#cart-total').text(response.data.CartTotal);
                toast(response.data.Message)
                $('#cart-status').text(response.data.CartCount);
            })
            .catch(function (error) {
                console.log(error);
            });
    });

    // Increase Item in Cart
    $('.increaseItemInCart').click(function (e) {
        e.preventDefault();
        const id = $(this).data("id");

        axios.post('/Cart/IncreaseItem', {
            id
        })
            .then(function (response) {
                $(`#item-count-${response.data.ItemId}`).val(response.data.ItemCount);
                $('#cart-total').text(response.data.CartTotal);
                toast(response.data.Message)
                $('#cart-status').text(response.data.CartCount);
            })
            .catch(function (error) {
                console.log(error);
            });
    });

    $('.decreaseItemInCart').click(function (e) {
        e.preventDefault();
        const id = $(this).data("id");

        axios.post('/Cart/DecreaseItem', {
            id
        })
            .then(function (response) {
                if (response.data.ItemCount == 0) {
                    $('#row-' + response.data.ItemId).fadeOut('slow');
                } else {
                    $('#item-count-' + response.data.ItemId).val(response.data.ItemCount);
                }
                $('#cart-total').text(response.data.CartTotal);
                toast(response.data.Message)
                $('#cart-status').text(response.data.CartCount);
            })
            .catch(function (error) {
                console.log(error);
            });
    });

    $(document).on('click', '.addToWishList', function (e) {
        e.preventDefault();
        const id = $(this).data("id");

        axios.post('/WishList/AddToWishList', {
            id
        })
            .then(function (response) {
                toast(response.data.Message)
                var wishList = $(`.addToWishList[data-id=${id}]`);
                var wishListParent = wishList.prev();
                wishListParent.next().addClass(response.data.Class).removeClass('addToWishList');
                wishList.children().addClass(response.data.Color)
            })
            .catch(function (error) {
                console.log(error);
            });
    });

    $(document).on('click', '.removeFromWishList', function (e) {
        e.preventDefault();
        const id = $(this).data("id");

        axios.post('/WishList/RemoveFromWishList', {
            id
        })
            .then(function (response) {
                toast(response.data.Message)
                var wishList = $(`.removeFromWishList[data-id=${id}]`);
                var wishListParent = wishList.prev();
                wishListParent.next().addClass(response.data.Class).removeClass('removeFromWishList');
                wishList.children().removeClass(response.data.Color)
                $(`#row-${response.data.DeleteId}`).fadeOut('slow');
            })
            .catch(function (error) {
                console.log(error);
            });
    });

    $(document).on('click', '.createCart', function (e) {
        e.preventDefault();
        const id = $(this).data("id");

        axios.post('/WishList/createCart', {
            id
        })
            .then(function (response) {
                toast(response.data.Message)
                $(`#row-${response.data.DeleteId}`).fadeOut('slow');
                $('#cart-status').text(response.data.CartCount);
            })
            .catch(function (error) {
                console.log(error);
            });
    });
});

