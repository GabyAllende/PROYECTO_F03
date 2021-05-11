using System;
using System.Collections.Generic;
using System.Text;
using UPB.FinalProject.Data.Models;

namespace UPB.FinalProject.Services.Models
{
    public class Price
    {
        public string CodProd { get; set; }
        public string ProdName { get; set; }
        public string CodDesc { get; set; }
        public List<Quotation> Products { get; set; }

    }
}
