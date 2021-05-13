using System;
using System.Collections.Generic;
using System.Text;

namespace UPB.FinalProject.Data.Exceptions
{
    public class DataBaseException : Exception
    {
        public DataBaseException(string message) : base(message)
        {

        }
    }
}
