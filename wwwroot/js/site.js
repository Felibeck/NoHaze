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
            $("#app_" + ID).attr("style", "display: none !important;");

        }
    });
}

// Ocultar con !important

function AgregarApp(ID) {
    $.ajax({
        url: '/Home/agregarAppOcio',
        data: { id: ID },
        type: 'GET',
        dataType: 'json',
        success: function(response) {
            $("#noApp_" + ID).attr("style", "display: none !important;");

        }
    });
}

function CompletarMision(iDdxu, meta, desafio, recompensa) {
    if(desafio >= meta){
        $.ajax({
            url: '/Home/CompletarMision',
            data: { id: iDdxu, recompensa: recompensa},
            type: 'GET',
            dataType: 'json',
            success: function(response) {
                $("#boton_" + iDdxu).attr("style", "background-color : --color-principal !important;");
                $("#boton_" + iDdxu).text("Reclamado");
            }
        });
    }
}