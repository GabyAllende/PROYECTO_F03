using System;
using System.Collections.Generic;
using System.Text;

namespace UPB.FinalProject.Services.Exceptions
{
    // Esta es la estructura que debe tener nuestro manejo de excepciones e imprecion de logs.

    //1.- Restore app status => in this case we do not handle any status (data)
    //2.- Log the exception error with a significant message.
    //3.- Trow an SPECIFIC exception 

    class ServiceException : Exception
    {
        public ServiceException(string message) : base(message)
        {

        }
    }
}
