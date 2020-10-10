// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function displayModal(ID) {
    var mod = ID + "_modal";
    var modal = document.getElementById(mod);
    modal.style.display = "block";
    modal.addEventListener("click", function () { modal.style.display = "none" });
}

function yesnoCheck() {
    if (document.getElementById('yesCheck').checked) {
        document.getElementById('ifYes').style.display = 'none';
    }
    else document.getElementById('ifYes').style.display = 'block';

}

