import { confirmDelete } from '/js/Shared.js';

const deleteBtns = document.querySelectorAll(".delete-button");


deleteBtns.forEach(btn => {
    btn.addEventListener("click", e => {

        let id = btn.dataset.id;

        console.log(id);

        if (!id) {
            Swal.fire("Error", "Invalid ID. Cannot delete.", "error");
            return;
        }

        //confirmDelete(id, "Customer","Cart");
    });
});

