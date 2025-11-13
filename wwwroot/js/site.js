// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function EliminarApp(ID) {
    $.ajax({
        url: '/Home/eliminarAppOcio',
        data: { Id: ID },
        type: 'GET',
        dataType: 'json',
        success: function(response) {
            //document.getElementById("app_" + ID).style.display = "none";
            $("#app_" + ID).attr("style", "display: none !important;");

        }
    });
}
// Ocultar con !important

