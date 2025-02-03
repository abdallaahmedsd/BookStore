 //Table


//$(document).ready(function () {
//    $('#example').DataTable({
//        dom: 'Bfrtip', // Adds buttons
//        buttons: ['copy', 'excel', 'pdf', 'print'],
//        //responsive: true, // Makes it responsive
//    });
//});


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


    // Bind custom search input functionality
    $('.table-search-box input').on('keyup', function () {
        table.search($(this).val()).draw();
    });

    // Optional: Attach an event to the "إضافة" (add) button
    $('#add-button').on('click', function () {
        // Add your functionality here (e.g., show a modal, navigate, etc.)
        alert("Add button clicked!");
    });

    // Optionally, bind events to the dynamically created buttons:
    $('#edit-button').on('click', function () {
        // Get data for the row.
        let id = $(this).data('id');

        let data = table.row($(this).parents('tr')).data();
        alert('Edit button clicked for ' + data[1] +'with id '+ id); // Assuming the second column is the title.
    });

    $('#delete-button').on('click', function () {
        let id = $(this).data('id');

        let data = table.row($(this).parents('tr')).data();
        alert('Delete button clicked for ' + data[1] + 'with id ' + id);
    });

});
