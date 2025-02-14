const StatusApproved = "تم الموافقة";
const StatusInProcess = "قيد الانتظار";
const StatusShipped = "تم شحنه";
const StatusCanceled = "تم إلغائه";

const StatusCells = document.querySelectorAll(".status");

StatusCells.forEach(cell => {
    const status = cell.dataset.status;

    switch (status) {
        case StatusApproved:
            cell.classList.add("color-second");
            break;
        case StatusInProcess:
            cell.classList.add("color-gray");
            break;
        case StatusShipped:
            cell.classList.add("color-main");
            break;
        case StatusCanceled:
            cell.classList.add("color-red");
            break;
        default:
            cell.classList.add("bg-gray");
    }
});
