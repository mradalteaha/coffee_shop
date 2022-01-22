

let navbar = document.querySelector('.navbar');

document.querySelector('#menu-btn').onclick = () => {
    navbar.classList.toggle('active');
    searchForm.classList.remove('active');
    cartItem.classList.remove('active');
}

let searchForm = document.querySelector('.search-form');

document.querySelector('#search-btn').onclick = () => {
    searchForm.classList.toggle('active');
    navbar.classList.remove('active');
    cartItem.classList.remove('active');
}

let cartItem = document.querySelector('.cart-items-container');

document.querySelector('#cart-btn').onclick = () => {
    cartItem.classList.toggle('active');
    navbar.classList.remove('active');
    searchForm.classList.remove('active');
}

window.onscroll = () => {
    navbar.classList.remove('active');
    searchForm.classList.remove('active');
    cartItem.classList.remove('active');
}


function myFunction() {

    alert(123);

    return true
}
function Add() {
    const menu = document.getElementById("menu-box")
    menu.innerHTML += `
    <div class="box">
    <img src="images/menu-2.png" alt="">
    <h3>tasty and healty</h3>
    <div class="price">$15.99 <span>20.99</span></div>
    <a href="#" class="btn">add to cart</a>
</div>
`

}

function AddtoCart(item) {
    var itemid = $(item).attr("itemid"); var formData = new FormData();
    formData.append("itemid", itemid);
    $.ajax({
        async: true, type: 'POST', contentType: false, processData: false, data: formData, url: '/UserModels/Index', success: function (data) {
            if (data.success)
            { $("#cartitem").text("Cart(" + data.Counter + ")"); }
        }, error: function () { alert("Problem occured") }
    })
}
function alreadyexist() {

    alert("User already exist");

    return true
}