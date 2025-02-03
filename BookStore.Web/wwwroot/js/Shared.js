const passwordField = document.querySelector(".password-box input");
const togglePasswordSVG = passwordField.closest(".password-box").querySelector("#toggle-password");

togglePasswordSVG.addEventListener("click", function () {
    if (passwordField.type === "password") {
        passwordField.type = "text"; // Show password
    } else {
        passwordField.type = "password"; // Hide password
    }
});