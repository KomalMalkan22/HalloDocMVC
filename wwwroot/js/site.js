// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/* Modal */

var targetModal = new bootstrap.Modal(document.getElementById('targetModal', {}))
targetModal.show();

var dismissModal = document.getElementById('dismissModal');
dismissModal.addEventListener("click", () => {
    targetModal.hide();
})

/* Choose File */

$("#files").change(function () {
    console.log("function");
    filename = this.files[0].name;
    $("#chooseFile").text(filename);
})

/* Password */

function passtoggle() {
    var x = document.getElementById("floatingPassword");
    if (x.type === "password") {
        x.type = "text";
        document.querySelectorAll("i.fa.fa-eye-slash").forEach(i => i.style.display = "none");
        document.querySelectorAll("i.fa.fa-eye").forEach(i => i.style.display = "block");
    }
    else {
        x.type = "password";
        document.querySelectorAll("i.fa.fa-eye-slash").forEach(i => i.style.display = "block");
        document.querySelectorAll("i.fa.fa-eye").forEach(i => i.style.display = "none");
    }
}