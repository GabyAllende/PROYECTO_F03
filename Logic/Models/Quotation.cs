using System;
using System.Collections.Generic;
using System.Text;

namespace UPB.FinalProject.Logic.Models
{
    public class Quotation
    {
        public string CodProd { get; set; }

        public string CodClient { get; set; }

        public int Stock { get; set; }
        public double Price { get; set; }

        public bool Sale { get; set; }
    }
}
