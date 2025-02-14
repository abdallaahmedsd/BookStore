const StatusCells = document.querySelectorAll(".status");

StatusCells.forEach(cell => {
    const status = cell.dataset.status;

    switch (status) {
        case SessionHelper.StatusApproved:
            cell.classList.add("color-second");
            break;
        case SessionHelper.StatusInProcess:
            cell.classList.add("color-gray");
            break;
        case SessionHelper.StatusShipped:
            cell.classList.add("color-main");
            break;
        case SessionHelper.StatusCanceled:
            cell.classList.add("color-red");
            break;
        default:
            cell.classList.add("bg-gray");

    }
})

