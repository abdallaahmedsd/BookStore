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
                url: `/api/admin/${controller}/${id}`,
                type: "DELETE",
                //data: { id: id }, // Send as a form field
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

                        window.location.href = `/admin/${controller}/index`;
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
