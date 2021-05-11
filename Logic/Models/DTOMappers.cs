using System;
using System.Collections.Generic;
using System.Text;

namespace UPB.FinalProject.Logic.Models
{
    public static class DTOMappers
    {
        public static List<Quotation> MapQuotations(List<FinalProject.Data.Models.Quotation> quotations)
        {
            List<Quotation> mappedQuotations = new List<Quotation>();
            foreach (UPB.FinalProject.Data.Models.Quotation quo in quotations)
            {
                mappedQuotations.Add(new Quotation()
                {
                    CodProd = quo.CodProd,
                    CodClient = quo.CodClient,
                    Stock = quo.Stock,
                    Sale = quo.Sale,
                    Price = quo.Price
                });
            }

            return mappedQuotations;
        }

        public static Quotation MapQuotationDL(FinalProject.Data.Models.Quotation quo)
        {
            Quotation myLogicQuo = new Quotation()
            {
                CodProd = quo.CodProd,
                CodClient = quo.CodClient,
                Stock = quo.Stock,
                Sale = quo.Sale,
                Price = quo.Price
            };

            return myLogicQuo;
        }


        public static FinalProject.Data.Models.Quotation MapGroupLD(Quotation quo)
        {
            FinalProject.Data.Models.Quotation myDataQuo = new FinalProject.Data.Models.Quotation()
            {
                CodProd = quo.CodProd,
                CodClient = quo.CodClient,
                Stock = quo.Stock,
                Sale = quo.Sale,
                Price = quo.Price
            };

            return myDataQuo;
        }
    }
}
