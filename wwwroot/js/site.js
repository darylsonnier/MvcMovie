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

function checkReqFields() {
	var returnValue;
	var name = document.getElementById("shipName").value;
	var address1 = document.getElementById("shipAdd1").value;
	var address2 = document.getElementById("billAdd1").value;
	var name1 = document.getElementById("shipName").value;
	var name2 = document.getElementById("billName").value;



	returnValue = true;
	if (name.trim() == "") {
		document.getElementById("shipName").innerHTML = "* Name is required.";
		returnValue = false;
	}
	if (address.trim() == "") {
		document.getElementById("shipAdd1").innerHTML = "* Address is required.";
		returnValue = false;
	}
	return returnValue;
}