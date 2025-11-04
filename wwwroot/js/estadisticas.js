let config = 0;

function sumar()
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

function restar()
{
    if(config == -1)
    {
        config = 1;
    }
    else
    {
        config--;
    }
}