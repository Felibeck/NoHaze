let tiempoTrabajo = 30;
let tiempoDescanso = 5;
let segundosRestantes = tiempoTrabajo * 60;
let estado = "pausado"; // "trabajo", "descanso", o "pausado"
let temporizador = null;
let tiempoTrabajado = 0;

function actualizarPantalla() {
    const min = Math.floor(segundosRestantes / 60);
    const seg = segundosRestantes % 60;
    document.getElementById("tiempo").textContent =
        `${min.toString().padStart(2, '0')}:${seg.toString().padStart(2, '0')}`;
}

function iniciar() {
    if (estado === "pausado") {
        estado = "trabajo";
        document.getElementById("estado").textContent = "Tiempo de trabajo";
        segundosRestantes = tiempoTrabajo * 60;
        actualizarPantalla();
    }

    if (temporizador) {
        // Si ya está corriendo, pausa
        clearInterval(temporizador);
        temporizador = null;
    } else {
        // Iniciar el temporizador
        temporizador = setInterval(() => {
            segundosRestantes--;
            actualizarPantalla();
            
            if (segundosRestantes <= 0) {
                // Cambiar entre trabajo y descanso
                if (estado === "trabajo") {
                    estado = "descanso";
                    segundosRestantes = tiempoDescanso * 60;
                    document.getElementById("estado").textContent = "Tiempo de descanso";
                    tiempoTrabajado += tiempoTrabajo;
                } else {
                    estado = "trabajo";
                    segundosRestantes = tiempoTrabajo * 60;
                    document.getElementById("estado").textContent = "Tiempo de trabajo";
                }
                actualizarPantalla();
            }
        }, 1000);
    }
}

function reiniciar() {
    clearInterval(temporizador);
    temporizador = null;
    estado = "pausado";
    segundosRestantes = tiempoTrabajo * 60;
    document.getElementById("estado").textContent = "Tiempo de trabajo";

     if (tiempoTrabajado > 0)
     {
         $.ajax({
             url: '/Home/AgregarRegistro',
             data: { Tiempo: tiempoTrabajado},
             type: 'GET',
             dataType: 'json',
             success: function(response) {
            }
        }); 
    }
    actualizarPantalla();
}

function abrirModal() {
    document.getElementById("modalEditar").style.display = "flex";
}

function cerrarModal() {
    document.getElementById("modalEditar").style.display = "none";
}

function guardarTiempos() {
    tiempoTrabajo = parseInt(document.getElementById("inputTrabajo").value);
    tiempoDescanso = parseInt(document.getElementById("inputDescanso").value);
    
    // Reiniciar con los nuevos tiempos
    reiniciar();
    cerrarModal();
}

// Inicializar al cargar
actualizarPantalla();