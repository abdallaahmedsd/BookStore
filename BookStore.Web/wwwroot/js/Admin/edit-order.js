
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

function ValidateInput() {
    let carrier = document.getElementById("Carrier");
    let shippingDate = document.getElementById("ShippingDate");
    let trackingNumber = document.getElementById("TrackingNumber");
    let estimatedDate = document.getElementById("EstimatedDelivery");
    if (!carrier.value) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: "Please enter carrier!"
        });

        return false;
    }
    if (!shippingDate.value) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: "Please enter shipping Date!"
        });

        return false;
    }

    if (!trackingNumber.value) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: "Please enter tracking number!"
        });

        return false;
    }
    if (!estimatedDate.value) {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: "Please enter estimated Date!"
        });

        return false;
    }

    return true;
}
