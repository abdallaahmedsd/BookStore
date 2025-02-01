let navbarUl = document.querySelector(".navbar-nav");


let navLinks = NavbarUl.querySelectorAll("li a.active");
navLinks.forEach(link => link.classList.remove("active"));