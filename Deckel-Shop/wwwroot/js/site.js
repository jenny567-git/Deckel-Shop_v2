// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//Admin -> Edit customer -> Show password
function showPassword() {
    var x = document.getElementById("inputPassword");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
};

//Admin -> Customer order history -> table search (Filter table)
$(document).ready(function () {
    $("#tableSearch").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $("#customerOrderHistoryTable tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});