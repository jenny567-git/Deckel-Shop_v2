"use strict";
var connection = new signalR.HubConnectionBuilder().withUrl("/DeckelHub").build();
connection.on("OrderHasBeenPlaced", function (nrOfChatters) {
    console.log("An order was placed!");

    toastr.info("A new order has just been placed!");
});



connection.start().then(function () {
}).catch(function (err) {
    return console.error(err.toString());
});