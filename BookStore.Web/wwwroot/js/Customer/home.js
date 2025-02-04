let searchInput = document.querySelector(".search-box input");
let navbarUl = document.querySelector(".navbar-nav");

searchInput.addEventListener("keyup", e => {
    if (e.keyCode === 13) {  // Enter key
        let searchValue = searchInput.value.trim();

        if (searchValue !== "") {
            window.location.href = `/Customer/Book/Index?search=${encodeURIComponent(searchValue)}`;
        }
    }
});


