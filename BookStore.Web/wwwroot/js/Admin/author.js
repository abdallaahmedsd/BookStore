
const img = document.querySelector(".form-book-cover img"); // fix this naming 
const imgDeleteBtn = document.querySelector(".input-group .delete-image");
const imgInput = document.getElementById("ProfileImage");

imgInput.addEventListener("change", function (event) {

    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            img.src = e.target.result;
            img.style.display = "block";
        };
        reader.readAsDataURL(file);
    }
});

imgDeleteBtn.addEventListener("click", function (event) {
    alert("delete image");
    imgInput.value = "";

    img.style.display = "none";

    alert("delete image");

});

