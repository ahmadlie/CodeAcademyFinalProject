document.getElementById('formFile').addEventListener('change', function (event) {
    var input = event.target;
    var reader = new FileReader();

    reader.onload = function () {
        var imageContainer = document.querySelector('.image-container');
        var imgElement = imageContainer.querySelector('img');

        imgElement.src = reader.result;
        imageContainer.classList.remove('hidden'); // Optional: Show the image container if hidden initially
    };

    reader.readAsDataURL(input.files[0]);
});

// script.js

//document.addEventListener('DOMContentLoaded', function () {
//    var count = 0;

//    document.querySelectorAll('[id^="newImage"]').forEach(function (input) {
//        input.addEventListener('change', function () {
//            if (this.files && this.files[0]) {
//                var reader = new FileReader();
//                var imageContainer = this.closest('.form-group').previousElementSibling.querySelector('img');

//                reader.onload = function (e) {
//                    imageContainer.src = e.target.result;
//                }

//                reader.readAsDataURL(this.files[0]);
//            }
//        });
//    });
//});

