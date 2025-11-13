let config = 0;

function pasar(num) {
    if (num == 1) {
        if (config == 1) {
            config = -1;
        } else {
            config++;
        }
    } else if (config == -1) {
        config = 1;
    } else {
        config--;
    }

    // Ocultar todas las secciones
    document.getElementById('seccion-diario').style.display = 'none';
    document.getElementById('seccion-semanal').style.display = 'none';
    document.getElementById('seccion-mensual').style.display = 'none';

    // Actualizar el texto del carrusel
    const carouselTitle = document.querySelector('#carouselExample .carousel-item.active h1');
    
    // Mostrar la sección correspondiente y aplicar estilos
    if (config == 0) {
        // SEMANAL
        carouselTitle.textContent = 'Semanal';
        document.getElementById('seccion-semanal').style.display = 'flex';
        
        for (let i = 1; i <= 7; i++) {
            const h2 = document.getElementById(`semanal${i}`);
            const texto = h2.innerText;
            const valor = parseFloat(texto);
            const div = h2.parentElement;
            div.style.paddingTop = (valor * 10) + "%";
        }
    } else if (config == 1) {
        // MENSUAL
        carouselTitle.textContent = 'Mensual';
        document.getElementById('seccion-mensual').style.display = 'flex';
        
        for (let i = 1; i <= 4; i++) {
            const h2 = document.getElementById(`mensual${i}`);
            const texto = h2.innerText;
            const valor = parseFloat(texto);
            const div = h2.parentElement;
            div.style.paddingTop = (valor * 10) + "%";
        }
    } else {
        // DIARIO
        carouselTitle.textContent = 'Diario';
        document.getElementById('seccion-diario').style.display = 'flex';
        
        const h2 = document.getElementById('dia');
        const texto = h2.innerText;
        const valor = parseFloat(texto);
        const div = h2.parentElement;
        div.style.paddingTop = (valor * 10) + "%";
    }
}


window.onload = function() {
    pasar(0); 
}