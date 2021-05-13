using System;
using System.Collections.Generic;
using System.Text;

namespace UPB.FinalProject.Logic.Exceptions
{
    public class InvalidQuotationDataException : Exception
    {
        public InvalidQuotationDataException(string mesagge) : base("Logic Layer: " + mesagge) { }

    }
}