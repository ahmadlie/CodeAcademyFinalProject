
var imageButton = document.getElementById("imageButton");
imageButton.addEventListener("click", function (event) {
    event.preventDefault();
    var imageInput = document.getElementById("imageInput");
    imageInput.click();
});

var reviewCardButton = document.getElementById("reviewCardBtn");
reviewCardButton.addEventListener("click", function (event) {
    event.preventDefault();
    document.getElementById("userCreateCard").style.display = "block";

    var firstname = document.getElementById("firstnameInput").value;
    var lastname = document.getElementById("lastnameInput").value;
    var email = document.getElementById("emailInput").value;
    var phonenumber = document.getElementById("phonenumberInput").value;
    var username = document.getElementById("usernameInput").value;

    document.getElementById("fistnameTag").innerText = "First Name : " + firstname;
    document.getElementById("lastnameTag").innerText = "Last Name : " + lastname;
    document.getElementById("emailTag").innerText = "Email : " + email;
    document.getElementById("phonenumberTag").innerText = "Phone Number : " + phonenumber;
    document.getElementById("usernameTag").innerText = "User Name : " + username;
});

