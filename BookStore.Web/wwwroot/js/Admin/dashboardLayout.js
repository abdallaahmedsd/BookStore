

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

    ////$('#delete-button').on('click', function () {
    //$('#example .delete-button').on('click', function () {
    //    alert("ff");
    //    let id = $(this).closest("tr").data("id");

    //    if (!id) {
    //        Swal.fire("Error", "Invalid ID. Cannot delete.", "error");
    //        return;
    //    }

    //    confirmDelete(id, controller);

    //});
});
