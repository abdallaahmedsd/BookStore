import Swal from 'sweetalert2'

import Swal from 'sweetalert2/dist/sweetalert2.js'
import 'sweetalert2/src/sweetalert2.scss'

// or via CommonJS
const Swal = require('sweetalert2')




const passwordField = document.querySelector(".password-box input");
const togglePasswordSVG = passwordField.closest(".password-box").querySelector("#toggle-password");

togglePasswordSVG.addEventListener("click", function () {
    if (passwordField.type === "password") {
        passwordField.type = "text"; // Show password
    } else {
        passwordField.type = "password"; // Hide password
    }
});



export function confirmDelete(id, controller) {
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
                url: `/Admin/${controller}/Delete`,
                type: "POST",
                data: { id: id }, // Send as a form field
                success: function () {
                    Swal.fire({
                        title: "تم الحذف!",
                        text: "تم حذف السجل بنجاح.",
                        icon: "success",
                        confirmButtonText: "حسنًا",
                        customClass: {
                            confirmButton: "btn btn-main"
                        }
                    }).then(() => {
                        // Remove the deleted row from the DataTable
                        table.row($(`tr[data-id="${id}"]`)).remove().draw();
                    });
                },
                error: function (xhr, status, error) {
                    console.error("Error:", xhr.responseText);
                    Swal.fire("خطأ", "فشل في حذف السجل.", "error");
                }
            });
        }
    });
}
