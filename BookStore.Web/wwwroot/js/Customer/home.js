let searchInput = document.querySelector(".search-box input");

searchInput.addEventListener("keyup", e => {
    if (e.keyCode === 13) {  // Enter key
        let searchValue = searchInput.value.trim();

        if (searchValue !== "") {
            window.location.href = `/Customer/Book/Index?filterSearch=${encodeURIComponent(searchValue)}`;
        }
    }
});


