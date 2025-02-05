const today = new Date().toISOString().split("T")[0];

const dateInput = document.getElementById("bookDate");
dateInput.max = today;  
dateInput.min = "1900-01-01"; 
dateInput.value = today;  



const img = document.querySelector(".form-book-cover img")
const label = document.querySelector(".form-book-cover label")
const imgOptions = document.querySelector(".img-options")
const imgDeleteBtn = document.querySelector(".img-options .delete")
const imgResetBtn = document.querySelector(".img-options .reset")
const imgContainer = document.querySelector(".form-book-cover")

const imgInput = document.getElementById("CoverImage");

imgInput.addEventListener("change", function (event) {
    alert("test");

    const file = event.target.files[0];
    if (file) {
        const reader = new FileReader();
        reader.onload = function (e) {
            img.src = e.target.result;
            img.style.display = "block";
            label.style.display = "none";
            //imgOptions.style.display = "flex";
            //imgContainer.style.borderRadius = ".875rem 0 0.875rem";
        };
        reader.readAsDataURL(file);
    }
});
imgDeleteBtn.addEventListener("click", function (event) {
    alert("are you sure?");
    imgInput.value = "";

    img.style.display = "none";
    label.style.display = "block";

});

imgResetBtn.addEventListener("click", function (event) {
    alert("reset");
    imgInput.value = "";
    imgInput.click();
});


//imgContainer.style.borderRadius = ".875rem";
