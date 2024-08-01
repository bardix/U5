// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.


// Write your JavaScript code.

function addToCart(productId) {
    $.ajax({
        url: '/Orders/AddProduct',
        type: 'POST',
        data: { productId: productId, quantity: 1 },
        success: function (response) {
            alert('Prodotto aggiunto al carrello!');
        },
        error: function () {
            alert('Si è verificato un errore. Riprova.');
        }
    });
}
