using System;
using System.Collections.Generic;
using System.Text;
using UPB.FinalProject.Data.Models;
using UPB.FinalProject.Services.Models;

namespace UPB.FinalProject.Data
{
    public interface IDbContext
    {
        public Quotation AddQuotation(Quotation quo);

        public List<Quotation> GetAllQuotations();

        public Quotation UpdateQuotation(int id, string codProd, int quantity);

        public int DeleteQuotation(int id);

        public Quotation UpdateSaleTrue(int id);
        public Quotation UpdateSaleFalse(int id);
    }
}
