
import { confirmOrderAction } from '/js/Admin/order.js';

const cancelBtn = document.getElementById("cancel-button");

cancelBtn.addEventListener("click", e => {
    let id = cancelBtn.dataset.id;

    if (!id) {
        Swal.fire("Error", "Invalid ID. Cannot cancel.", "error");
        return;
    }

    confirmOrderAction("cancel", id);
});

