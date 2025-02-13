
document.addEventListener("DOMContentLoaded", function () {

    let searchInput = document.getElementById("bookSearch");
    let books = document.querySelectorAll(".book-item");
    function filterBooks() {
        let filter = searchInput.value.toLowerCase();

        books.forEach(function (book) {
            let title = book.getAttribute("data-title").toLowerCase();

            if (title.includes(filter)) {
                book.style.display = "flex";
            } else {
                book.style.display = "none";
            }
        });
    }

    // Apply filter on page load (if there's an initial search value)
    if (searchInput.value.trim() !== "") {
        filterBooks();
    }

    // Apply filter dynamically when typing
    searchInput.addEventListener("keyup", filterBooks);
});