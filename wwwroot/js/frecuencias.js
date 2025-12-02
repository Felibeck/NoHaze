let audioActual = null;
let botonActual = null;

function Play(frecuenciaNombre, boton) {
    const rutaAudio = `/frecuencias/${frecuenciaNombre}`;
    
    // Si hay un audio reproduciéndose
    if (audioActual) {
        // Si es la misma frecuencia, pausar/reanudar
        if (audioActual.src.includes(frecuenciaNombre)) {
            if (audioActual.paused) {
                audioActual.play();
                cambiarIconoPause(boton);
            } else {
                audioActual.pause();
                cambiarIconoPlay(boton);
            }
            return;
        } else {
            // Si es otra frecuencia, detener la anterior
            audioActual.pause();
            audioActual.currentTime = 0;
            if (botonActual) {
                cambiarIconoPlay(botonActual);
            }
        }
    }
    
    // Reproducir la nueva frecuencia
    audioActual = new Audio(rutaAudio);
    botonActual = boton;
    
    audioActual.play().catch(err => console.log("Error al reproducir:", err));
    cambiarIconoPause(boton);
    
    // Cuando termine, volver al ícono de play
    audioActual.addEventListener('ended', () => {
        cambiarIconoPlay(boton);
    });
}

function cambiarIconoPlay(boton) {
    const svg = boton.querySelector('svg path');
    if (svg) {
        svg.setAttribute('d', 'M8 5v14l11-7z'); // Ícono play
    }
}

function cambiarIconoPause(boton) {
    const svg = boton.querySelector('svg path');
    if (svg) {
        svg.setAttribute('d', 'M6 4h4v16H6V4zm8 0h4v16h-4V4z'); // Ícono pause
    }
}