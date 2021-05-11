using System;
using System.Collections.Generic;
using System.Text;
using UPB.FinalProject.Data.Models;

namespace UPB.FinalProject.Data
{
    public interface IDbContext
    {
        public Quotation AddQuotation(Quotation quo);

        public List<Quotation> GetAllQuotations();

        public Quotation UpdateQuotation(Quotation quoToUpdate);

        public Quotation DeleteQuotation(Quotation quoToDelete);
    }
}
