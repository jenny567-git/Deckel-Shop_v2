// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/***NavBar Function***/
const navSlide = () => {
    const burger = document.querySelector('.burger');
    const nav = document.querySelector('.nav-links');
    const navLinks = document.querySelectorAll('.nav-links li');

    function ResetAnimation(listItem) {
        listItem.style.animation = 'none';
        listItem.offsetHeight; /* trigger reflow */
        listItem.style.animation = null;
    }

    burger.addEventListener('click', () => {
        //toggle nav
        nav.classList.toggle('nav-active');

        //Animate Links
        navLinks.forEach((link, index) => {
            // Fires only the first time. Animation is set by class
            if (link.style.animation == '') {
                ResetAnimation(link);
                link.style.animation = `navLinkFade 0.5s ease forwards ${index / 7 + 0.3}s`;
                // Fires when side nav has been closed at least once
            } else if (link.style.animation === '0s ease 0s 1 normal forwards running navLinkFade') {
                ResetAnimation(link);
                link.style.animation = `navLinkFade 0.5s ease forwards ${index / 7 + 0.3}s`;
                // Fires when opening side nav
            } else {
                ResetAnimation(link);
                link.style.animation = '0s ease 0s 1 normal forwards running navLinkFade';
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

////Admin -> Edit customer -> Show password
//function showPassword() {
//    var x = document.getElementById("inputPassword");
//    if (x.type === "password") {
//        x.type = "text";
//    } else {
//        x.type = "password";
//    }
//};

//Admin -> all search bar
$(document).ready(function () {
    $(".searchbarForTable").on("keyup", function () {
        var value = $(this).val().toLowerCase();
        $(".tableSearch tr").filter(function () {
            $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
        });
    });
});


// tooltip function
$(function () {
    $('[data-toggle="tooltip"]').tooltip()
})

// Modal scripts

function GetModalInfo(id, modalType) {
    // Api call
    $(function () {
        console.log("testing function" + id + " " + modalType);
        var Id = '{"Id": "' + id + '"}';
        console.log(Id);
        $.ajax({
            type: "POST",
            url: "/api/Orders/" + modalType,
            data: Id,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) { // response = order info
                if (modalType === 'orderDetails') {
                    PopulateOrderModal(response);
                }
                else if (modalType === 'customerDetails') {
                    PopulateCustomerModal(response)
                }
                else if (modalType === 'stockDetails') {
                    PopulateStockModal(response)
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

function PopulateStockModal(product) {
    $('input[id="ProductId"]').val(product.id);
    $('input[id="ProductName"]').val(product.name);
    $('input[id="ProductAmount"]').val(product.amount);
    $('input[id="ProductPrice"]').val(product.price);
    $('input[id="ProductCategory"]').val(product.category);
    $('input[id="ProductStatus"]').val(product.status);
    $('textarea[id="ProductDescription"]').val(product.description);
    $('input[id="uploadFile1"]').val(product.imgName);
    $('input[id="uploadFile2"]').val(product.imgName2);
    
}

function ChangeRoleToCustomer(id, userName) {
    $('#changeUserRoleId').val(id);
    $('#changeRoleDiv').html("Do you really want to change the role of " + userName + "  to the customer role?");
}


function ChangeRoleToAdmin(id, userName) {
    $('#changeUserRoleId').val(id);
    $('#changeRoleDiv').html("Do you really want to to change the role of " + userName + "  to the admin role?");
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


function AddToCart(id) {
    $('#AddToCartBtn').val(id);
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
    $("#personEmail").html(order.customer.email);

    var shippingCost = 50;
    $("#total").html(order.orderTotal + shippingCost);
}


// Wait for window to load
window.addEventListener('load', function () {
 
    $("#ModalShowBtn").click(function () {
        event.preventDefault();
       
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

    $('#StockDetails').on('hidden.bs.modal', function () {
        var form = document.getElementById("stockForm");
        var elements = form.elements;
        for (var i = 0, len = elements.length; i < len; ++i) {
            
            elements[i].disabled = true;
        }
        saveStockBtn.disabled = true;
        editStockBtn.disabled = false;
        cancelStockBtn.disabled = false;
        CloseStockBtn.disabled = false;
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

function enableStockForm() {
    var form = document.getElementById("stockForm");
    var elements = form.elements;
    for (var i = 0, len = elements.length; i < len; ++i) {
        
        elements[i].disabled = false;
    }
    saveStockBtn.disabled = false;
    CloseStockBtn.disabled = false;
   
}
// add an imgae to stock details modal

function readURL1(input1) {
    if (input1.files && input1.files[0]) {
        var reader = new FileReader();
        
        console.log(input1.files[0].name)
     
        reader.onload = function (e) {
            $('#imageResult1')
                .attr('src', e.target.result);
        };
        reader.readAsDataURL(input1.files[0]);
    }
}

$(function () {
    $('#upload1').on('change', function () {
        readURL1(input1);
    });
});

function readURL2(input2) {
    if (input2.files && input2.files[0]) {
        var reader2 = new FileReader();

        reader2.onload = function (e) {
            $('#imageResult2')
                .attr('src', e.target.result);
        };
        reader2.readAsDataURL(input2.files[0]);
    }
}

$(function () {
    $('#upload2').on('change', function () {
        readURL2(input2);
    });
});

// add an imgae to add a new product modal

function readURLAdd1(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        console.log(input.files[0].name)

        reader.onload = function (e) {
            $('#imageResultAdd1')
                .attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

$(function () {
    $('#uploadAdd1').on('change', function () {
        readURL1(input1);
    });
});


function readURLAdd2(input) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();

        console.log(input.files[0].name)

        reader.onload = function (e) {
            $('#imageResultAdd2')
                .attr('src', e.target.result);
        };
        reader.readAsDataURL(input.files[0]);
    }
}

$(function () {
    $('#uploadAdd2').on('change', function () {
        readURL1(input1);
    });
});

/*  ==========================================
    SHOW UPLOADED IMAGE NAME
* ========================================== */
// detail stock modal
var inputFile1 = document.getElementById('uploadFile1');
var input1 = document.getElementById('upload1');
var infoArea1 = document.getElementById('upload-label1');

var inputFile2 = document.getElementById("uploadFile2");
var input2 = document.getElementById('upload2');
var infoArea2 = document.getElementById('upload-label2');


function showFileName1(event) {

    var input = event.srcElement;
    var fileName = input.files[0].name;
    inputFile1.value = "/Image/" + fileName;
    infoArea1.textContent = 'File name: ' + fileName;
}
function showFileName2(event) {
    var input = event.srcElement;
    var fileName = input.files[0].name;
    inputFile2.value = "/Image/" + fileName;
    infoArea2.textContent = 'File name: ' + fileName;
}
input1.addEventListener('change', showFileName1);
input2.addEventListener('change', showFileName2);

// add a product modal
var inputFileAdd1 = document.getElementById('uploadFileAdd1');
var inputAdd1 = document.getElementById('uploadAdd1');
var infoAreaAdd1 = document.getElementById('upload-labelAdd1');

var inputFileAdd2 = document.getElementById("uploadFileAdd2");
var inputAdd2 = document.getElementById('uploadAdd2');
var infoAreaAdd2 = document.getElementById('upload-labelAdd2');

function showFileNameAdd1(event) {

    var input = event.srcElement;
    var fileName = input.files[0].name;
    inputFileAdd1.value = "/Image/" + fileName;
    infoAreaAdd1.textContent = 'File name: ' + fileName;
}
function showFileNameAdd2(event) {
    var input = event.srcElement;
    var fileName = input.files[0].name;
    inputFileAdd2.value = "/Image/" + fileName;
    infoAreaAdd2.textContent = 'File name: ' + fileName;
}
inputAdd1.addEventListener('change', showFileNameAdd1);
inputAdd2.addEventListener('change', showFileNameAdd2);

