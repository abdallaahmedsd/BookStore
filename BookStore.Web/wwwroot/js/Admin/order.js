const StatusCells = document.querySelectorAll(".status");

StatusCells.forEach(cell => {
    const status = cell.dataset.status; 
    switch (status) {
        case "Progress":
            cell.classList.add("bg-second");
            break;
        case "Complete":
            cell.classList.add("bg-main");
            break;
        case "Cancel":
            cell.classList.add("bg-red");
            break;
        default:
            // Optionally, handle unexpected status values
            break;
    }
})
