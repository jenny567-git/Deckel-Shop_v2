﻿// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var slideIndex = 1;
$(document).ready(function () {

    showSlides(slideIndex);

});

// Next/previous controls
function plusSlides(n) {
    showSlides(slideIndex += n);
}

// Thumbnail image controls
function currentSlide(n) {
    showSlides(slideIndex = n);
}

function showSlides(n) {
    var i;
    var slides = document.getElementsByClassName("mySlides");
    var dots = document.getElementsByClassName("dot");
    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
} 

//Admin -> Edit customer -> Show password
function showPassword() {
    var x = document.getElementById("inputPassword");
    if (x.type === "password") {
        x.type = "text";
    } else {
        x.type = "password";
    }
};

//Admin -> all search bar
$(document).ready(function () {
    $(".searchbarForTable").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $(".tableSearch tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});

////Admin -> Customer order history -> table search (Filter table)
//$(document).ready(function () {
//    $("#tableSearch").on("keyup", function () {
//        var value = $(this).val().toLowerCase();
//        $("#customerOrderHistoryTable tr").filter(function () {
//            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
//        });
//    });
//});

// tooltip function
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})