let config = 0;

function pasar(num)
{
    if(num == 1)
    {
        if(config == 1)
        {
            config = -1;
        }
        else
        {
            config++;
        }

    }
    else if(config == -1)
    {
        config = 1;
    }
    else
    {
        config--;
    }

    //falta hacer
    if(config == 0)
    {
        for (let i = 1; i <= 7; i++) {
            const h2 = document.getElementById(`semanal${i}`);
            const texto = h2.innerText;        
            const valor = parseFloat(texto);   
    
            const div = h2.parentElement;
            div.style.paddingTop = (valor * 10) + "px";  
        }

    }
    else if(config == 1)
    {

    }
    else
    {

    }

}



