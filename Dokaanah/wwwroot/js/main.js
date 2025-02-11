
// ===================Loader=====[1]============
const loader = document.querySelector(".loader");

loader.classList.add("loader-hidden");

loader.addEventListener("transitionend", () => {
    loader.remove();
});

//==================================[2]================


// Open and Close cart

//var cartmenu = document.querySelector('.cart');

//function open_cart() {
//    cartmenu.classList.add("active");
//}
//function close_cart() {
//    cartmenu.classList.remove("active");
//}



//================================================================
// back to top when scrolling ==================[3]===============
//================================================================

window.onscroll = function () {
    scrollFunction()
};

function scrollFunction() {
    if (document.body.scrollTop > 300 || document.documentElement.scrollTop > 300) {
        document.getElementById("movetop").style.display = "block";
    } else {
        document.getElementById("movetop").style.display = "none";
    }
}

function topFunction() {
    document.body.scrollTop = 0;
    document.documentElement.scrollTop = 0;
}

//Cart function======================================================================

//let cartItems = [];

//const addToCartButtons = document.querySelectorAll('.add-cart-btnnn');
//addToCartButtons.forEach(button => {
//  button.addEventListener('click', addToCart);
//});

//function addToCart(event) {
//  let productCard = event.target.closest('.product-item');
//  let productName = productCard.querySelector('.product-name a').textContent;
//  let productPrice = parseFloat(productCard.querySelector('.new-price').textContent.replace('$', ''));
//  let productImage = productCard.querySelector('.product-image').src;

//  let existingProduct = cartItems.find(item => item.name === productName);
//  if (existingProduct) {
    
//    existingProduct.quantity++;
//  } else {
    
//    const newProduct = {
//      name: productName,
//      price: productPrice,
//      image: productImage,
//      quantity: 1
//    };
//    cartItems.push(newProduct);
//  }

//  updateCart();
//}

//function updateCart() {
//    let cartContent = document.querySelector('.cart-content');
//    let emptyCart = document.querySelector('.empty-cart');
//    let totalPrice = document.querySelector('.total-price');
//    let cartItemCount = document.querySelectorAll('.badge');
//    let willbeHiddeniNEmpty = document.querySelector('.willbeHiddeniNEmpty');
//  cartContent.innerHTML = '';
//  let total = 0;

//  if (cartItems.length === 0) {
//    emptyCart.style.display = 'block';
//    totalPrice.textContent = '$0';
//    willbeHiddeniNEmpty.style.display = 'none';
//  } else {
//    emptyCart.style.display = 'none';
//    willbeHiddeniNEmpty.style.display = 'block';
//    cartItems.forEach(item => {
//      const cartBox = document.createElement('div');
//      cartBox.classList.add('cart-box');
//      cartBox.innerHTML = `
//        <img src="${item.image}" class="cart-image" alt="${item.name}">
//        <div class="detail-box">
//          <div class="cart-product-title">${item.name}</div>
//          <div class="cart-price">$${item.price.toFixed(2)}</div>
//          <input type="number" value="${item.quantity}" min="1" class="cart-quantity" data-name="${item.name}">
//        </div>
//        <i class="fa-solid fa-trash-can cart-remove" data-name="${item.name}"></i>
//      `;
//      cartContent.appendChild(cartBox);
//      total += item.price * item.quantity;
//    });
//    totalPrice.textContent = `$${total.toFixed(2)}`;
//  }

//  cartItemCount.forEach(count => {
//    count.textContent = cartItems.length;
//  });

//  addRemoveEventListeners();
//  addQuantityEventListeners();
//}

//function addRemoveEventListeners() {
//    let removeButtons = document.querySelectorAll('.cart-remove');
//  removeButtons.forEach(button => {
//    button.addEventListener('click', removeFromCart);
//  });
//}

//function removeFromCart(event) {
//    let productName = event.target.dataset.name;
//  cartItems = cartItems.filter(item => item.name !== productName);
//  updateCart();
//}

//function addQuantityEventListeners() {
//    let quantityInputs = document.querySelectorAll('.cart-quantity');
//  quantityInputs.forEach(input => {
//    input.addEventListener('change', updateQuantity);
//  });
//}

//function updateQuantity(event) {
//    let productName = event.target.dataset.name;
//    let newQuantity = parseInt(event.target.value);
//    let product = cartItems.find(item => item.name === productName);
//  product.quantity = newQuantity;
//  updateCart();
//}
//updateCart();




// ==================================================================================
// shop Page Filteration
const filterBtns = document.querySelectorAll('.filter-btn');
const filterItems = document.querySelectorAll('.filter-item');

filterBtns.forEach(btn => {
    btn.addEventListener('click', () => {
        const filter = btn.dataset.filter;
        filterItems.forEach(item => {
            if (filter === 'all') {
                item.style.display = 'block';
            } else {
                item.style.display = item.classList.contains(filter) ? 'block' : 'none';
            }
        });
        filterBtns.forEach(btn => btn.classList.remove('active'));
        btn.classList.add('active');
    });
});


// ==================================================================================


document.getElementById('searchIcon').addEventListener('click', function () {
  $('#searchModal').modal('show');
});


document.getElementById('userIcon').addEventListener('click', function () {
    $('#userModal').modal('show');
});

document.getElementById('signInBtn').addEventListener('click', function () {
    $('#signInForm').show();
    $('#signUpForm').hide();
    $('#signInBtn').addClass('active');
    $('#signUpBtn').removeClass('active');
});

document.getElementById('signUpBtn').addEventListener('click', function () {
    $('#signInForm').hide();
    $('#signUpForm').show();
    $('#signInBtn').removeClass('active');
    $('#signUpBtn').addClass('active');
});
document.getElementById('returnToSignUp').addEventListener('click', function () {
    $('#signInForm').hide();
    $('#signUpForm').show();
    $('#signInBtn').removeClass('active');
    $('#signUpBtn').addClass('active');
});

document.getElementById('returnToSignIn').addEventListener('click', function () {
    document.getElementById('signInForm').style.display = 'block';
    document.getElementById('signUpForm').style.display = 'none';
    document.getElementById('signInBtn').classList.add('active');
    document.getElementById('signUpBtn').classList.remove('active');
});



// Product Detals Page
var buttons = document.querySelectorAll('.quantity-detail button');
buttons.forEach(function(button) {
    button.addEventListener('click', function() {
        var oldValue = parseFloat(this.parentNode.parentNode.querySelector('input').value);

        if (this.classList.contains('btn-plus')) {
            var newVal = oldValue + 1;
        } else {
            if (oldValue > 1) {
                var newVal = oldValue - 1;
            } else {
                newVal = 1;
            }
        }

        this.parentNode.parentNode.querySelector('input').value = newVal;
    });
});


// Account Page



