
let navLinks = document.querySelectorAll(".nav-link");

navLinks.forEach(link => {

    link.addEventListener("click",e => {

        let activeLink = document.querySelector(".nav-link.active");
        activeLink?.classList.remove("active");

        link.classList.add("active");
    })


})

