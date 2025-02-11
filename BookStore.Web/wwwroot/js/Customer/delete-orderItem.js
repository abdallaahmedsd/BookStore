const deleteBtns = document.querySelectorAll(".delete-button");

deleteBtns.forEach(btn => {
    btn.addEventListener("click", e => {
        let id = btn.dataset.id;

        if (!id) {
            Swal.fire("Error", "Invalid ID. Cannot delete.", "error");
            return;
        }

        confirmDelete("item", id);
    });
});

const deleteCartBtn = document.getElementById("delete-cart");

    deleteCartBtn.addEventListener("click", e => {
        confirmDelete("cart");
    });



export function confirmDelete(deleteType,id) {
    Swal.fire({
        title: "هل أنت متأكد؟",
        text: "لن تتمكن من التراجع عن هذا!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "نعم، احذفه!",
        cancelButtonText: "إلغاء",
        customClass: {
            confirmButton: "btn btn-second-open",
            cancelButton: "btn btn-red"
        }
    }).then((result) => {
        if (result.isConfirmed) {
            // Build URL based on whether an id was provided
            let url = `/api/customer/Cart/${deleteType}`;
            if (id) {
                url += `/${id}`;
            }

            // Send DELETE request
            $.ajax({
                url: url,
                type: "DELETE",
                success: function (response) {

                    if (response.success) {
                        // Store the success notification message in sessionStorage
                        sessionStorage.setItem('toastr-success-message', response.message);

                        // Redirect to the categories list page
                        window.location.href = '/customer/Cart/index';
                    } else {
                        // Handle case where success is false but not a server error
                        toastr.error(response.message || "حدث خطأ غير متوقع.");
                    }
                },
                error: function (xhr, status, error) {
                    // Handle server-side error (status 500, etc.)
                    const response = xhr.responseJSON;
                    if (response && response.message) {
                        toastr.error(response.message); // Display the error message using toastr
                    } else {
                        toastr.error("حدث خطأ غير معروف."); // Fallback message
                    }

                }
            });
        }
    });
}
