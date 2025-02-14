const inputPassword = document.querySelector(".passwordInput");
const showPasswordTrg = document.getElementById("showPasswordTrg");

showPasswordTrg.addEventListener("click", e => {
    if (inputPassword.type === "password") {
        inputPassword.type = "text";
        showPasswordTrg.innerHTML = `
            <svg id="showPasswordTrg" width="22" height="22" viewBox="0 0 22 22" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path d="M11 4.58333C7.55573 4.58333 4.98302 6.82037 3.45342 8.65273C2.3056 10.0277 2.30578 11.9725 3.45363 13.3475C4.98323 15.1796 7.55594 17.4167 11 17.4167C14.4442 17.4167 17.0169 15.1796 18.5465 13.3473C19.6942 11.9723 19.6944 10.0275 18.5466 8.65248C17.0169 6.82037 14.4442 4.58333 11 4.58333ZM11 14.6667C9.39946 14.6667 8.08333 13.3505 8.08333 11.75C8.08333 10.1495 9.39946 8.83333 11 8.83333C12.6005 8.83333 13.9167 10.1495 13.9167 11.75C13.9167 13.3505 12.6005 14.6667 11 14.6667Z" stroke="#C2C3CB" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>`;
    } else {
        inputPassword.type = "password";
        showPasswordTrg.innerHTML = `
          <svg id="showPasswordTrg" width="22" height="22" viewBox="0 0 22 22" fill="none" xmlns="http://www.w3.org/2000/svg">
                <path d="M8.95035 4.85392C9.59419 4.68164 10.2779 4.58333 11 4.58333C14.4442 4.58333 17.0169 6.82037 18.5465 8.65273C19.6944 10.0277 19.6942 11.9725 18.5464 13.3475C18.3705 13.5582 18.1808 13.7742 17.9776 13.9915M11.4583 8.28802C12.6099 8.48122 13.5188 9.3901 13.712 10.5417M2.74998 2.75L19.25 19.25M10.5417 13.712C9.54783 13.5452 8.73475 12.8455 8.40647 11.9167M3.98669 8.0467C3.79693 8.25113 3.61917 8.45418 3.45363 8.65248C2.30578 10.0275 2.3056 11.9723 3.45342 13.3473C4.98302 15.1796 7.55573 17.4167 11 17.4167C11.7352 17.4167 12.4308 17.3147 13.0849 17.1366" stroke="#C2C3CB" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
            </svg>
        `;
    }
})