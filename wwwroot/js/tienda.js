let minutosSeleccionados = 15;
let appIDActual = 0;

function abrirModal(fotoApp, nombreApp, idApp) {
    appIDActual = idApp;
    minutosSeleccionados = 15;
    
    // Actualizar la imagen de la app
    document.getElementById('modalAppLogo').src = fotoApp;
    document.getElementById('modalAppLogo').alt = nombreApp;
    
    // Actualizar valores iniciales
    actualizarValores();
    
    // Mostrar contenido de compra y ocultar gracias
    document.getElementById('contenidoCompra').style.display = 'block';
    document.getElementById('contenidoGracias').style.display = 'none';
    
    // Mostrar modal
    document.getElementById('modalCompra').style.display = 'flex';
}

function cerrarModal() {
    document.getElementById('modalCompra').style.display = 'none';
}

function cambiarMinutos(cantidad) {
    minutosSeleccionados += cantidad;
    
    // Limitar entre 5 y 120
    if (minutosSeleccionados < 5) minutosSeleccionados = 5;
    if (minutosSeleccionados > 120) minutosSeleccionados = 120;
    
    actualizarValores();
}

function actualizarValores() {
    document.getElementById('valorMinutos').textContent = minutosSeleccionados + ' mins';
    document.getElementById('modalCantidad').textContent = minutosSeleccionados;
    document.getElementById('precioTotal').textContent = minutosSeleccionados;
}

function realizarCompra() {
    
    console.log(minutosSeleccionados);
    $.ajax({
        url: '/Home/ComprarTiempo',
        data: {minutos: minutosSeleccionados },
        type: 'POST',
        dataType: 'json',
        success: function(response) {
            mostrarGracias();
        }
    });
}

function mostrarGracias() {
    document.getElementById('contenidoCompra').style.display = 'none';
    document.getElementById('contenidoGracias').style.display = 'flex';
}

// Cerrar modal al hacer clic fuera de él
document.addEventListener('click', function(event) {
    const modal = document.getElementById('modalCompra');
    if (event.target === modal) {
        cerrarModal();
    }
});