// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/***NavBar Function***/
const navSlide = () => {
    const burger = document.querySelector('.burger');
    const nav = document.querySelector('.nav-links');
    const navLinks = document.querySelectorAll('.nav-links li');

    burger.addEventListener('click', () => {
        //toggle nav
        nav.classList.toggle('nav-active');

        //Animate Links
        navLinks.forEach((link, index) => {
            if (link.style.animation) {
                //link.style.animation = '';
            } else {
                link.style.animation = `navLinkFade 0.5s ease forwards ${index / 7 + 0.3}s`;
            }
        });
        //Burger animation
        burger.classList.toggle('toggle');

    });


}

navSlide();
/**Navbar Function Ends**/


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

// Modal scripts

function GetModalInfo(id, modalType) {
    // Api call
    $(function () {
        console.log("testing function" + id + " " + modalType);
        //var person = '{Name: "' + $("#txtName").val() + '" }';
        var Id = '{"Id": "' + id + '"}';
        console.log(Id);
        $.ajax({
            type: "POST",
            url: "/api/Orders/" + modalType,
            data: Id, 
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) { // response = order info
                //alert("Hello: " + response.id);
                if (modalType === 'orderDetails') {
                    PopulateOrderModal(response);
                } else if (modalType === 'customerDetails') {
                    PopulateCustomerModal(response)
                }
                else {
                    console.log("Error: Could not find modalType!");
                }
            },
            failure: function (response) {
                alert("Failure " + response.responseText);
            },
            error: function (response) {
                alert("Error " + response.responseText);
            }
        });
    });
}

function PopulateCustomerModal(customer) {
    $('input[id="detailsCustomerID"]').val(customer.id);
    $('input[id="detailsFirstName"]').val(customer.firstName);
    $('input[id="detailsLastName"]').val(customer.lastName);
    $('input[id="detailsEmail"]').val(customer.email);
    $('input[id="detailsPhone"]').val(customer.phone);
    $('input[id="detailsStreet"]').val(customer.street);
    $('input[id="detailsCity"]').val(customer.city);
    $('input[id="detailsZip"]').val(customer.zipCode);

    console.log(customer.firstName);

}

function RemoveProduct(id) {

    $('#DeleteBtn').val(id);

}


function AddBackToStock(id) {
    $('#AddBackToStockBtn').val(id);
}

function SendOrder(id) {
    $('#SendOrderBtn').val(id);
}

function RemoveOrder(id) {

    $('#DeleteOrderBtn').val(id);

}


function PopulateOrderModal(order) {
    var productList = document.getElementById("OrderedItemsList");
    $("#exampleModalLongTitle").html("Order id: " + order.orderId);
    // Ordered items list
    for (var i = 0; i < order.orderedItems.length; i++) {
        var listItem = document.createElement('li');

        var div = document.createElement('div');
        var headline = document.createElement('h6');
        var small = document.createElement('small');
        var span = document.createElement('span');


        document.body.appendChild(div);
        document.body.appendChild(headline);
        document.body.appendChild(small);
        document.body.appendChild(span);


        // add relevant classes to each element
        listItem.classList.add("list-group-item", "d-flex", "justify-content-between", "lh-sm");
        headline.classList.add("my-0");
        small.classList.add("text-muted");
        span.classList.add("text-muted");

        // add the text to the element
        headline.appendChild(document.createTextNode(order.orderedItems[i].product.name));
        small.appendChild(document.createTextNode("Amount: " + order.orderedItems[i].amount));
        span.appendChild(document.createTextNode(order.orderedItems[i].product.price + " SEK"));

        listItem.appendChild(div);
        div.appendChild(headline);
        div.appendChild(small);
        listItem.appendChild(span);

        productList.appendChild(listItem);
    }

    $("#personName").html(order.customer.firstName + " " + order.customer.lastName);
    $("#personAddress").html(order.customer.street + ", " + order.customer.zipCode + " " + order.customer.city);
    $("#personPhone").html(order.customer.phone);
    $("#orderCost").html(order.orderTotal);

    var shippingCost = 50;
    $("#total").html(order.orderTotal + shippingCost);
}


// Wait for window to load
window.addEventListener('load', function () {
    //console.log('All assets are loaded')
    // When clicking the modal button
    $("#ModalShowBtn").click(function () {
        event.preventDefault();
        //console.log($("#orderIdRow").html());
        // Set value of the delete button. Value is sent to controller
        $('#DeleteBtn').val($("#orderIdRow").html());
    });

    // When modal is closed
    $('#exampleModalCenter').on('hidden.bs.modal', function () {
        $("#OrderedItemsList").empty();

    })

    $('#exampleModal-details').on('hidden.bs.modal', function () {
        var form = document.getElementById("customerForm");
        var elements = form.elements;
        for (var i = 0, len = elements.length; i < len; ++i) {
            
            elements[i].disabled = true;
        }
        saveBtnForEditCustomer.disabled = true;
        editBtn.disabled = false;
        CancleBtn.disabled = false;

    })


});




function enableForm() {
    var form = document.getElementById("customerForm");
    var elements = form.elements;
    for (var i = 0, len = elements.length; i < len; ++i) {
        
        elements[i].disabled = false;
    }
    saveBtnForEditCustomer.disabled = false;
   
}
