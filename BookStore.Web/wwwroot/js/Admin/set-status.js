
const StatusCells = document.querySelectorAll(".status");

StatusCells.forEach(cell => {
    const status = cell.dataset.status;
    switch (status) {
        case SessionHelper.StatusApproved:
            cell.classList.add("bg-second");
            break;
        case SessionHelper.StatusInProcess:
            cell.classList.add("bg-main");
            break;
        case SessionHelper.StatusShipped:
            cell.classList.add("bg-text");
            break;
        case SessionHelper.StatusCanceled:
            cell.classList.add("bg-red");
            break;
        default:
            cell.classList.add("bg-gray");

    }
})

