/* 
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
$(document).ready(function() {

    //call continue shopping button
    // $('a#contShopping').click($.facebox.close);
    //check delivery information
    $("a#checkDelivery").click(function(e) {

        $pincode = document.getElementById('pinJQ').value;

        if ($pincode == null || $pincode.length <= 0) {
            $('#pin_errorJQ').html('Please Enter Pincode');
            return false;
        } else {
            if (isNaN($pincode) || $pincode.length > 6 || $pincode.length < 4) {
                $('#pin_errorJQ').html('Pincode is not valid');
                return false;
            } else {

                var randNumber = Math.floor((Math.random() * 10000000) + 10000);
                $.ajax({
                    type: "POST",
                    url: "ajax/check-pincode.php",
                    data: "pincode=" + $pincode + "&rand=" + randNumber,
                    beforeSend: function() {
                        $('#pin_errorJQ').html('<img src="css/loading.gif" border="0" height="25px;">');
                        //facebox({loadingImage: 'css/loading.gif'});
                    },
                    success: function(msg) {
                        $('#pin_errorJQ').html(msg);

                    }
                });
            }
            //return true;
        }

    });


});

//check for integer
function CheckForIntegers(i)
{
    if (i.value.length > 0)//^[0-9]{3}$
    {
        i.value = i.value.replace(/[^\d]+/g, '');

    }
}

function removeCartItem(productID) {
	var cnf = confirm('Are you sure? You want to remove the book from cart.');
	if(cnf == true)
	{
    $.ajax({
        type: "POST",
        url: "./remove-cart-item.php",
        data: "productID=" + productID,
        success: function(result) {
            window.location.href="./order.php";
        }
    });
	}

}

function numberonly(myid)
{
    var qunatity_limit_on_product = 5;
    myid.value = myid.value.replace(/[^0-9.]/g, '');
    if (myid.value < 1)
    {
        myid.value = '';
    }
    if (myid.value > qunatity_limit_on_product)
    {
        myid.value = qunatity_limit_on_product;
    }

}
function pincodeonly(myid)
{
    //alert(myid.value);
    myid.value = myid.value.replace(/[^0-9.]/g, '');
    if (myid.value < 1)
    {
        myid.value = '';
    }

}


function updateCart() {

    //$("#cart_quantity_form").submit(function() {
    var str = $("#cart_quantity_form").serialize();
    $.ajax({
        type: "POST",
        url: "update-cart.php",
        data: str,
        beforeSend: function() {
            $('#update-cart').html('<img src="css/loading.gif" border="0" height="25px;">');
        },
        success: function(result) {
            $('div#displayCart').html(result);
        }
    });
    // });
}



//check pin code validation on product details page
function checkDeliveryValidate() {
    $pincode = document.getElementById('pin').value;
    if ($pincode == null || $pincode.length <= 0) {
        $('#pin_error').html('Please Enter Pincode');
        return false;
    } else {
        if (isNaN($pincode) || $pincode.length > 6 || $pincode.length < 4) {
            $('#pin_error').html('Pincode is not valid');
            return false;
        } else {
            $('#pin_error').html('');
            return true;
        }
        return true;
    }
}

//check pin code  on product details page
function checkDelivery() {
    if (checkDeliveryValidate()) {

        var pincode = document.getElementById('pin').value;
        var productsource = document.getElementById('productsource').value;
        var randNumber = Math.floor((Math.random() * 10000000) + 10000);
        $.ajax({
            type: "POST",
            url: "ajax/check-pincode-products.php",
            data: "pincode=" + pincode + "&productsource=" + productsource + "&rand=" + randNumber,
            beforeSend: function() {
                $('#pin_error').html('<img src="assets/lib/jquery.bxslider/images/bx_loader.gif" border="0" height="25px;">');
                //facebox({loadingImage: 'css/loading.gif'});
            },
            success: function(msg) {
                $('#pin_error').html(msg);
            }
        });
    }
}

//Login form validation
function loginValidation() {

    var login_email = document.getElementById('login_email');
    if (login_email.value.trim() == "") {
        alert('Please enter your email');
        login_email.focus();
        return false;
    }

    if (login_email.value.search(/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/) == -1) {
        alert('Please enter valid email');
        login_email.focus();
        return false;
    }
    var login_pwd = document.getElementById('login_pwd');
    if (login_pwd.value.trim() == "") {
        alert('Please enter your password');
        login_pwd.focus();
        return false;
    }

}

//function for registratio validation
function registrationValidation() {

    var reg_username = document.getElementById('reg_username');
    if (reg_username.value.trim() == "") {
        alert('Please enter your email');
        reg_username.focus();
        return false;
    }

    if (reg_username.value.search(/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/) == -1) {
        alert('Please enter valid email');
        reg_username.focus();
        return false;
    }

    var reg_pwd = document.getElementById('reg_pwd');
    if (reg_pwd.value.trim() == "") {
        alert('Please enter your Password');
        reg_pwd.focus();
        return false;
    }

    var reg_conpwd = document.getElementById('reg_conpwd');
    if (reg_conpwd.value.trim() == "") {
        alert('Please enter your Confirm Password');
        reg_conpwd.focus();
        return false;
    }
    if (reg_conpwd.value.trim() != reg_pwd.value.trim()) {
        alert('Entered Password do not match.');
        reg_conpwd.focus();
        return false;
    }
}


function changepassValidation() {

    var usr_pass = document.getElementById('usr_pass');
    if (usr_pass.value.trim() == "") {
        alert('Please enter your Current Password');
        usr_pass.focus();
        return false;
    }
    var usr_newpass = document.getElementById('usr_newpass');
    if (usr_newpass.value.trim() == "") {
        alert('Please enter your New Password');
        usr_newpass.focus();
        return false;
    }

    var usr_newpass_conf = document.getElementById('usr_newpass_conf');
    if (usr_newpass_conf.value.trim() == "") {
        alert('Please enter your Confirm Password');
        usr_newpass_conf.focus();
        return false;
    }
}

function accountValidation() {
    var u_name = document.getElementById('u_name');
    if (u_name.value.trim() == "") {
        alert('Please enter your Name');
        u_name.focus();
        return false;
    }

    var u_address = document.getElementById('u_address');
    if (u_address.value.trim() == "") {
        alert('Please enter your Address');
        u_address.focus();
        return false;
    }
	
	var u_country = document.getElementById('u_country');
    if (u_country.value.trim() == "na") {
        alert('Please enter your Country');
        u_country.focus();
        return false;
    }
	
    var u_state = document.getElementById('u_state');
    if (u_state.value.trim() == "") {
        alert('Please enter your State');
        u_state.focus();
        return false;
    }
    var u_city = document.getElementById('u_city');
    if (u_city.value.trim() == "") {
        alert('Please enter your City');
        u_city.focus();
        return false;
    }
    var u_pincode = document.getElementById('u_pincode');
    if (u_pincode.value.trim() == "") {
        alert('Please enter your Pincode');
        u_pincode.focus();
        return false;
    }
    var u_phone = document.getElementById('u_phone');
    if (u_phone.value.trim() == "") {
        alert('Please enter your Telephone');
        u_phone.focus();
        return false;
    }
}

//forgot password

function forgotPassword() {
    //emailid
    var user_email = document.getElementById('user_email');
    //alert(user_email.value);
    if (user_email.value.trim() == "") {
        alert("Please enter your email id.");
        user_email.focus();
        return false;
    }
    else if (user_email.value.search(/^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$/) == -1) {
        alert('Please provide a valid email');
        user_email.focus();
        return false;
    }
    else
    {
        return true;
    }
}

function myaccountValidation() {
    var first_name = document.getElementById('first_name');
    if (first_name.value.trim() == "") {
        alert('Please enter your First Name');
        first_name.focus();
        return false;
    }

    var last_name = document.getElementById('last_name');
    if (last_name.value.trim() == "") {
        alert('Please enter your Last Name');
        u_address.focus();
        return false;
    }

}

function copyToShip() {
    if (document.getElementById('chkSame').checked == true) {

        var billing_name = document.getElementById('billing_name').value.trim();
        document.getElementById('shipping_name').value = billing_name;//first name

        var billing_address = document.getElementById('billing_address').value.trim();
        document.getElementById('shipping_address').value = billing_address;//address line 1

        var billing_landmark = document.getElementById('billing_landmark').value.trim();
        document.getElementById('shipping_landmark').value = billing_landmark;//landmark

        var billing_city = document.getElementById('billing_city').value.trim();
        document.getElementById('shipping_city').value = billing_city;//city

        var billing_state = document.getElementById('billing_state').value.trim();
        document.getElementById('shipping_state').value = billing_state;//state

        var billing_country = document.getElementById('billing_country').value.trim();
        document.getElementById('shipping_country').value = billing_country;//country

        var billing_pincode = document.getElementById('billing_pincode').value.trim();
        document.getElementById('shipping_pincode').value = billing_pincode;//pin code

        var billing_phone = document.getElementById('billing_phone').value.trim();
        document.getElementById('shipping_phone').value = billing_phone;//contact no

        /*var billingAddress2 = document.getElementById('textBillingAddressLine2').value.trim();
         document.getElementById('textShippingAddressLine2').value = billingAddress2;//address line 2*/
    } else {
        document.getElementById('shipping_name').value = "";//first name
        document.getElementById('shipping_address').value = "";//address 
        document.getElementById('shipping_landmark').value = "";//landmark
        document.getElementById('shipping_state').value = "";//state
        document.getElementById('shipping_country').value = "";//country
        document.getElementById('shipping_pincode').value = "";//pincode
        document.getElementById('shipping_phone').value = "";//phone
    }
}


//billing-details from validation   
function checkUserDetails() {

    var billing_name = document.getElementById('billing_name');
    if (billing_name.value.trim() == "") {
        alert('Please enter your Billing name');
        billing_name.focus();
        return false;
    }
    var billing_address = document.getElementById('billing_address');
    if (billing_address.value.trim() == "") {
        alert('Please enter your Billing Address');
        billing_address.focus();
        return false;
    }

    var billing_city = document.getElementById('billing_city');
    if (billing_city.value.trim() == "") {
        alert('Please enter your Billing City');
        billing_city.focus();
        return false;
    }
    var billing_state = document.getElementById('billing_state');
    if (billing_state.value.trim() == "") {
        alert('Please enter your Billing State');
        billing_state.focus();
        return false;
    }
    var billing_country = document.getElementById('billing_country');
    if (billing_country.value.trim() == "na") {
        alert('Please select your Billing Country');
        billing_country.focus();
        return false;
    }
    var billing_pincode = document.getElementById('billing_pincode');
    if (billing_pincode.value.trim() == "") {
        alert('Please enter your Billing Pincode');
        billing_pincode.focus();
        return false;
    }
    var billing_phone = document.getElementById('billing_phone');
    if (billing_phone.value.trim() == "") {
        alert('Please enter your Billing Phone');
        billing_phone.focus();
        return false;
    }

    var shipping_name = document.getElementById('shipping_name');
    if (shipping_name.value.trim() == "") {
        alert('Please enter your Shipping Name');
        shipping_name.focus();
        return false;
    }
    var shipping_address = document.getElementById('shipping_address');
    if (shipping_address.value.trim() == "") {
        alert('Please enter your Shipping Address');
        shipping_address.focus();
        return false;
    }

    var shipping_city = document.getElementById('shipping_city');
    if (shipping_city.value.trim() == "") {
        alert('Please enter your Shipping City');
        shipping_city.focus();
        return false;
    }
    var shipping_state = document.getElementById('shipping_state');
    if (shipping_state.value.trim() == "") {
        alert('Please enter your Shipping State');
        shipping_state.focus();
        return false;
    }
    var shipping_country = document.getElementById('shipping_country');
    if (shipping_country.value.trim() == "na") {
        alert('Please select your Shipping Country');
        shipping_country.focus();
        return false;
    }
    var shipping_pincode = document.getElementById('shipping_pincode');
    if (shipping_pincode.value.trim() == "") {
        alert('Please enter your Shipping Pincode');
        shipping_pincode.focus();
        return false;
    }


    var shipping_phone = document.getElementById('shipping_phone');
    if (shipping_phone.value.trim() == "") {
        alert('Please enter your Shipping phone.');
        shipping_phone.focus();
        return false;
    }

}
function payvalidation() {
    
    //radio button validation
    if (document.getElementById('rdoCOD').checked == false && document.getElementById('rdoPaynetz').checked == false) {
        alert("Please select payment option.");
        document.getElementById('rdoCOD').focus();
        return false;
    }


    //check box validation
    var chktnc = document.getElementById('chktnc');
    if (chktnc.checked == false) {
        alert("Please check agreed to the terms and conditions.");
        chktnc.focus();
        return false;
    }

    if (document.getElementById('odtotal').value > 20000)
    {
        alert("Orders upto Rs. 20,000(Twenty thousand) will be accepted as COD. Please try another payment options.");
        return false;
    }
}

function myaccountValidation() {
    var first_name = document.getElementById('first_name');
    if (first_name.value.trim() == "") {
        alert('Please enter your First Name');
        first_name.focus();
        return false;
    }

    var last_name = document.getElementById('last_name');
    if (last_name.value.trim() == "") {
        alert('Please enter your Last Name');
        u_address.focus();
        return false;
    }
      
}

//********************************* Quantity Update ******************************

$(function() {
    $("#limit_error").hide();
    $(".quantity_up").click(function() {

        var quantity_id = $(this).attr("data-id");
        var productId = quantity_id.replace('quantity_up_', '');
        var curr_quantity = Number($("#quantity-" + productId).val());
        if(curr_quantity < 5 ) {
            var new_quantity = curr_quantity + 1;
            $("#quantity-" + productId).val(new_quantity);
            $("#cart_quantity_form").submit();
        } else {
            $("#limit_error").show();
            $("#limit_error").html("<img src='../assets/images/alert-triangle.png'> This book has a limit of only 5 per customer. You can't add more than 5 copies in a day."); 
            return false;            
        }
        
    });
    
    
    $(".quantity_down").click(function() {
        
        var quantity_id = $(this).attr("data-id");
        var productId = quantity_id.replace('quantity_down_', '');
        var curr_quantity = Number($("#quantity-" + productId).val());
        if(curr_quantity>1){
        var new_quantity = curr_quantity - 1;
        } else {
        var new_quantity = curr_quantity;                
        }
        $("#quantity-" + productId).val(new_quantity);
        $("#cart_quantity_form").submit();
    });
	
	// Refine search pannel display //
	 $(".title_block").click(function() {
		 $('#left-search-block').slideToggle('show');	
});
});

