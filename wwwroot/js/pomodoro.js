// let tiempoTrabajo = 30;
// let tiempoDescanso = 5;
// let estado;

// function alternarEncendido()
// {
//     if(estado)
//     {
//         estado = false;


//     }else
//     {
//         estado = true;

        
//     }
// }



let tiempoTrabajo = 25;
let tiempoDescanso = 5;
let segundosRestantes = tiempoTrabajo * 60;
let estado = "pausado"; // "trabajo", "descanso", o "pausado"
let temporizador = null;

function actualizarPantalla() {
  const min = Math.floor(segundosRestantes / 60);
  const seg = segundosRestantes % 60;
  document.getElementById("tiempo").textContent =
    `${min.toString().padStart(2, '0')}:${seg.toString().padStart(2, '0')}`;
}

function alternarModo() {
  if (estado === "trabajo") {
    estado = "descanso";
    segundosRestantes = tiempoDescanso * 60;
    document.getElementById("estado").textContent = "Descanso 😌";
  } else {
    estado = "trabajo";
    segundosRestantes = tiempoTrabajo * 60;
    document.getElementById("estado").textContent = "¡A trabajar! 💪";
  }
}

function iniciar() {
  // Leer tiempos desde los inputs
  tiempoTrabajo = parseInt(document.getElementById("inputTrabajo").value);
  tiempoDescanso = parseInt(document.getElementById("inputDescanso").value);

  if (estado === "pausado") {
    estado = "trabajo";
    document.getElementById("estado").textContent = "¡A trabajar! 💪";
  }

  if (temporizador) {
    // Si ya está corriendo, pausa
    clearInterval(temporizador);
    temporizador = null;
    document.getElementById("estado").textContent += " (Pausado ⏸️)";
  } else {
    temporizador = setInterval(() => {
      segundosRestantes--;
      actualizarPantalla();
      if (segundosRestantes <= 0) {
        alternarModo();
        actualizarPantalla();
      }
    }, 1000);
  }
}

function reiniciar() {
  clearInterval(temporizador);
  temporizador = null;
  estado = "pausado";
  tiempoTrabajo = parseInt(document.getElementById("inputTrabajo").value);
  segundosRestantes = tiempoTrabajo * 60;
  document.getElementById("estado").textContent = "Listo para empezar";
  actualizarPantalla();
}

actualizarPantalla();