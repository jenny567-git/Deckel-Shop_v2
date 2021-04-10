"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/DeckelHub").build();

connection.on("OrderHasBeenPlaced", function (nrOfChatters) {
    toastr.info("A new order has just been placed!");
});

document.getElementById("CheckoutForm").addEventListener('submit', function (event) {
    connection.invoke("OrderPlaced");
    return true;
});

connection.start().then(function () {
    console.log("Connection start")
}).catch(function (err) {
    return console.error(err.toString());
});