using System;
using System.Collections.Generic;
using System.Text;
using UPB.FinalProject.Logic.Models;

namespace UPB.FinalProject.Logic.Managers
{
    public interface IQuotationManager
    {
        public List<Quotation> GetAllQuotations();



        public Quotation CreateQuotation(Quotation quo);//, [FromBody] string studentLastName ) 



        public Quotation UpdateQuotation(Quotation quo);




        public int DeleteQuotation(int id);
        public Quotation UpdateSaleTrue(int id);

        public Quotation UpdateSaleFalse(int id);
    }
}
