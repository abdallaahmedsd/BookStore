const img = document.querySelector(".form-book-cover img");
const imgDeleteBtn = document.querySelector(".input-group .delete-image");

const profileImageInput = document.getElementById("ProfileImage");

profileImageInput.addEventListener("change", function (event) {

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
    event.preventDefault(); 
    profileImageInput.value = "";    
    img.style.display = "none"; 
});

