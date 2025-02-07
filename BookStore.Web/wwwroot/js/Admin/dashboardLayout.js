

function confirmDelete(id, controller) {
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




$(document).ready(function () {
    let table = $('#example').DataTable({
        dom: 'lrtip',
        pageLength: 5,
        lengthChange: true,
        lengthMenu: [
            [5, 10, 25, 50, -1],
            [5, 10, 25, 50, "All"]
        ],
        language: {
            search: "بحث:",
            lengthMenu: "عرض _MENU_ سجل لكل صفحة",
            info: "عرض _START_ إلى _END_ من _TOTAL_ سجل",
            infoEmpty: "لا توجد سجلات متاحة",
            infoFiltered: "(تمت التصفية من إجمالي _MAX_ سجل)",
            zeroRecords: "لم يتم العثور على سجلات",
        },
    });

    $('.table-search-box input').on('keyup', function () {
        table.search($(this).val()).draw();
    });


    let controller = window.location.pathname.split("/")[2]; // Extracts the controller name dynamically


    $('#add-button').on('click', function () {
        window.location.href = `/Admin/${controller}/Create`;
    });

    $("#example tbody").on("dblclick", "tr", function () {
        let id = $(this).data("id");
        if (id) {
            window.location.href = `/Admin/${controller}/Details/${id}`;
        }
    });

    $('#example .delete-button').on('click', function () {
        let id = $(this).closest("tr").data("id");

        if (!id) {
            Swal.fire("Error", "Invalid ID. Cannot delete.", "error");
            return;
        }

        confirmDelete(id, controller);

    });
});
