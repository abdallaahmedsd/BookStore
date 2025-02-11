﻿import '../lib/toastr.js/toastr.min.js';

export function confirmDelete(id, controller) {

    const segments = window.location.pathname.split('/').filter(segment => segment !== '');
    let area = segments[0];


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
            // Send DELETE request
            $.ajax({
                url: `/api/${area}/${controller}/${id}`,
                type: "DELETE",
                success: function (response) {

                    if (response.success) {
                        // Store the success notification message in sessionStorage
                        sessionStorage.setItem('toastr-success-message', response.message);

                        // Redirect to the categories list page
                        window.location.href = `/${area}/${controller}/index`;
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
