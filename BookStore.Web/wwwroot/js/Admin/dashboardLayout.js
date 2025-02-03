 //Table
$(document).ready(function () {
    let table = $('#example').DataTable({

        dom: 'lrtip',// This removes the default search box if you're using a custom one
        pageLength: 5,  // Default page length is 5
        lengthChange: true, // Allow user to change page length
        lengthMenu: [
            [5, 10, 25, 50, -1],  // The actual values used by DataTables (-1 means "All")
            [5, 10, 25, 50, "All"] // The display text for each option
        ],
        language: {
            search: "بحث:", // Change the search box label
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


    $('#add-button').on('click', function () {
        window.location.href = `/Admin/Book/Create`;
    });


    $('#edit-button').on('click', function () {
        let id = $(this).closest("tr").data("id");

        let data = table.row($(this).parents('tr')).data();
        alert('Edit button clicked for ' + data[1] + 'with id ' + id); 

        if (id) {
            window.location.href = `/Admin/Book/Edit/${id}`;
        }
    });

    $('#delete-button').on('click', function () {
        let id = $(this).closest("tr").data("id");

        let data = table.row($(this).parents('tr')).data();
        alert('Delete button clicked for ' + data[1] + 'with id ' + id);

        if (id) {
            window.location.href = `/Admin/Book/Delete/${id}`;
        }
    });


    $("#example tbody").on("dblclick", "tr", function () {
        let id = $(this).data("id"); 

        if (id) {
            window.location.href = `/Admin/Book/Details/${id}`;
        }
    });

});

