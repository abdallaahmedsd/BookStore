
const cancelBtns = document.querySelectorAll(".cancel-button");

cancelBtns.forEach(btn => {
    btn.addEventListener("click", e => {
        let id = btn.dataset.id;

        if (!id) {
            Swal.fire("Error", "Invalid ID. Cannot cancel.", "error");
            return;
        }

        confirmOrderAction("cancel", id);
    });
});


export function confirmOrderAction(operationType, id) {
    Swal.fire({
        title: "?? ??? ??????",
        text: "?? ????? ?? ??????? ?? ???!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "???? ?????!",
        cancelButtonText: "?????",
        customClass: {
            confirmButton: "btn btn-red-open",
            cancelButton: "btn btn-red"
        }
    }).then((result) => {
        if (result.isConfirmed) {
            let url = `/api/admin/Order/${operationType}/${id}`;

            // Send request
            $.ajax({
                url: url,
                type: "POST",
                success: function (response) {

                    if (response.success) {
                        // Store the success notification message in sessionStorage
                        sessionStorage.setItem('toastr-success-message', response.message);

                        // Redirect to the categories list page
                        window.location.href = '/admin/Order/index';
                    } else {
                        // Handle case where success is false but not a server error
                        toastr.error(response.message || "??? ??? ??? ?????.");
                    }
                },
                error: function (xhr, status, error) {
                    // Handle server-side error (status 500, etc.)
                    const response = xhr.responseJSON;
                    if (response && response.message) {
                        toastr.error(response.message); // Display the error message using toastr
                    } else {
                        toastr.error("??? ??? ??? ?????."); // Fallback message
                    }
                }
            });
        }
    });
}


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
