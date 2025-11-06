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

    if(config == 0)
    {

    }
    else if(config == 1)
    {

    }
    else
    {

    }

}

