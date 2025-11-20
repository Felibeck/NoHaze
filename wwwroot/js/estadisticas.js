// let config = 0;

// function pasar(num) {
//     if(num != 0)
//     {
//         if (num == 1) {
//             if (config == 1) {
//                 config = -1;
//             } else {
//                 config++;
//             }
//         } else if (config == -1) {
//             config = 1;
//         } else {
//             config--;
//         }
//     }

//     // Ocultar todas las secciones
//     document.getElementById('seccion-diario').style.display = 'none';
//     document.getElementById('seccion-semanal').style.display = 'none';
//     document.getElementById('seccion-mensual').style.display = 'none';

    
//     // Mostrar la sección correspondiente y aplicar estilos
//     if (config == 0) {
//         // SEMANAL
//         document.getElementById('seccion-semanal').style.display = 'flex';
        
//         for (let i = 1; i <= 7; i++) {
//             const h2 = document.getElementById(`semanal${i}`);
//             const texto = h2.innerText;
//             const valor = parseFloat(texto);
//             const div = h2.parentElement;
//             div.style.paddingTop = (valor * 10) + "%";
//         }
//     } else if (config == 1) {
//         // MENSUAL
//         document.getElementById('seccion-mensual').style.display = 'flex';
        
//         for (let i = 1; i <= 4; i++) {
//             const h2 = document.getElementById(`mensual${i}`);
//             const texto = h2.innerText;
//             const valor = parseFloat(texto);
//             const div = h2.parentElement;
//             div.style.paddingTop = (valor * 10) + "%";
//         }
//     } else {
//         // DIARIO
//         document.getElementById('seccion-diario').style.display = 'flex';
        
//         const h2 = document.getElementById('dia');
//         const texto = h2.innerText;
//         const valor = parseFloat(texto);
//         const div = h2.parentElement;
//         div.style.paddingTop = (valor * 10) + "%";
//     }
// }


// window.onload = function() {
//     pasar(0); 
// }



let chart = null;
let config = 0;

// Datos desde ViewBag
const datos = {
    semanal: [@string.Join(",", ViewBag.horasSemanales ?? new int[7])],
    mensual: [@string.Join(",", ViewBag.horasMensuales ?? new int[4])],
    diario: [@ViewBag.horaDiaria ?? 0]
};

function initChart(tipo) {
    const ctx = document.getElementById('chartProductividad').getContext('2d');
    
    let labels, chartData, max;
    
    if (tipo === 'semanal') {
        labels = ['Lunes', 'Martes', 'Miércoles', 'Jueves', 'Viernes', 'Sábado', 'Domingo'];
        chartData = datos.semanal;
        max = Math.max(...chartData) + 10;
    } else if (tipo === 'mensual') {
        labels = ['Semana 1', 'Semana 2', 'Semana 3', 'Semana 4'];
        chartData = datos.mensual;
        max = Math.max(...chartData) + 10;
    } else {
        labels = ['Hoy'];
        chartData = [datos.diario];
        max = Math.max(...chartData) + 10;
    }

    if (chart) {
        chart.destroy();
    }

    chart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: labels,
            datasets: [{
                label: 'Minutos de productividad',
                data: chartData,
                backgroundColor: '#7A5BA8',
                borderColor: '#6A4B98',
                borderWidth: 2,
                borderRadius: 8,
                fill: true,
                tension: 0.4
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: true,
            plugins: {
                legend: {
                    display: false
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    max: max,
                    ticks: {
                        stepSize: 10,
                        color: '#666',
                        font: {
                            size: 12,
                            family: "'Zain', sans-serif"
                        }
                    },
                    grid: {
                        color: 'rgba(200, 200, 200, 0.2)',
                        drawBorder: false
                    }
                },
                x: {
                    ticks: {
                        color: '#666',
                        font: {
                            size: 12,
                            family: "'Zain', sans-serif"
                        }
                    },
                    grid: {
                        display: false
                    }
                }
            }
        }
    });
}

function pasar(num) {
    if (num === 1) {
        if (config === 1) {
            config = -1;
        } else {
            config++;
        }
    } else if (num === -1) {
        if (config === -1) {
            config = 1;
        } else {
            config--;
        }
    }

    let tipo;
    if (config === 0) {
        tipo = 'semanal';
        document.getElementById('navLabel').textContent = 'Semanal';
    } else if (config === 1) {
        tipo = 'mensual';
        document.getElementById('navLabel').textContent = 'Mensual';
    } else {
        tipo = 'diario';
        document.getElementById('navLabel').textContent = 'Diario';
    }

    initChart(tipo);
}

// Inicializar gráfico al cargar
window.addEventListener('load', () => {
    pasar(0);


    console.log("Semanal:", datos.semanal);

});