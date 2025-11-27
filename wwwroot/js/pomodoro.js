let tiempoTrabajo = 30;
let tiempoDescanso = 5;
let segundosRestantes = tiempoTrabajo * 60;
let estado = "pausado"; // "trabajo", "descanso", o "pausado"
let temporizador = null;
let tiempoTrabajado = 0;

let audioAlarma = new Audio('/sounds/alarma.mp3');

// Función para reproducir audio por 3 segundos
function reproducirAlarma() {
    audioAlarma.currentTime = 0; // Reiniciar desde el inicio
    audioAlarma.play().catch(err => console.log("Error al reproducir:", err));
    
    setTimeout(() => {
        audioAlarma.pause();
        audioAlarma.currentTime = 0;
    }, 3000); // 3 segundos
}

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
                    
                    // Reproducir alarma y mostrar notificación
                    reproducirAlarma();
                    mostrarNotificacion();
                    
                } else {
                    estado = "trabajo";
                    segundosRestantes = tiempoTrabajo * 60;
                    document.getElementById("estado").textContent = "Tiempo de trabajo";
                    
                    // Reproducir alarma cuando vuelve a trabajar
                    reproducirAlarma();
                    mostrarNotificacionTrabajo();
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


function mostrarNotificacion() {
    const notificacion = document.getElementById("notificacionDescanso");
    notificacion.style.display = "block";
    setTimeout(() => {
        notificacion.classList.add("show");
    }, 10);
}

function cerrarNotificacion() {
    const notificacion = document.getElementById("notificacionDescanso");
    notificacion.classList.remove("show");
    setTimeout(() => {
        notificacion.style.display = "none";
    }, 300);
}

function mostrarNotificacionTrabajo() {
    const notificacion = document.getElementById("notificacionDescanso");
    notificacion.querySelector("h2").textContent = "¡A trabajar! 💪";
    notificacion.querySelector("p").textContent = "Tu descanso ha terminado";
    notificacion.style.display = "block";
    setTimeout(() => {
        notificacion.classList.add("show");
    }, 10);
    
    // Resetear el texto después de cerrar
    setTimeout(() => {
        notificacion.querySelector("h2").textContent = "¡Tiempo de descanso! 🎉";
        notificacion.querySelector("p").textContent = "Has completado tu sesión de trabajo";
    }, 5000);
}