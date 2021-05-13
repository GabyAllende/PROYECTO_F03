using System;
using System.Collections.Generic;
using System.Text;

namespace UPB.FinalProject.Logic.Models
{
    public class Quotation
    {
        public int Id { get; set; }
        public string CodProd { get; set; }

        public string CodClient { get; set; }

        public int Quantity { get; set; }
        public double Price { get; set; }

        public bool Sale { get; set; }
    }
}
