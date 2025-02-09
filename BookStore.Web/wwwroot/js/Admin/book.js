const today = new Date().toISOString().split("T")[0];

const dateInput = document.getElementById("bookDate");
dateInput.max = today;  
dateInput.min = "1900-01-01"; 


const img = document.querySelector(".form-book-cover img");
const imgDeleteBtn = document.querySelector(".input-group .delete-image");
const imgInput = document.getElementById("CoverImage");

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
    event.preventDefault(); 
    imgInput.value = "";

    img.style.display = "none";
});

