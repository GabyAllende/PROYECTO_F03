using System;
using System.Collections.Generic;
using System.Text;
using UPB.FinalProject.Data.Models;

namespace UPB.FinalProject.Data
{
    public class DbContext : IDbContext
    {
        public int cont { get; set; }

        public List<Quotation> QuotationTable { get; set; }

        string[] cat = { "SOCCER",  "BASKET" };
        public DbContext() 
        {
            QuotationTable = new List<Quotation>();
            Random r = new Random();
            int est = r.Next(15, 20);

            cont = 0;

            for (int i = 0; i < est; i++)
            {
                cont += 1;
                string med = cont >= 100 ? "" + cont : (cont >= 10 ? "0" + cont : "00" + cont);
                int numClient = r.Next(1, 1000001);
                string med2 = numClient >= 1000000 ? "" + numClient : (numClient >= 100000 ? "0" + numClient : (numClient >= 10000 ? "00" + numClient : (numClient >= 1000 ? "000" + numClient : (numClient >= 100 ? "00" + numClient : (numClient >= 10 ? "0" + numClient : "00" + numClient)))));
                //Console.Out.WriteLine("HOLA");
                QuotationTable.Add(new Quotation()
                {
                    
                    CodProd = cat[r.Next(0, 2)]+"-"+med,
                    CodClient = "MTR-"+med2,
                   // Id = uniqueId(cont)
                   Sale = false,
                   Stock = r.Next(0,51),
                   //Price = r.NextDouble()*100

                });
            }
        }
        public Quotation AddQuotation(Quotation quo)
        {
            if (String.IsNullOrEmpty(quo.CodClient) || String.IsNullOrEmpty(quo.CodProd))
            {
                Console.Out.WriteLine("CodProd NULL o CodClient NULL: el ID o AvailableSlots del grupo esta vacio o Null");
                throw new Exception("CodProd NULL o CodClient NULL: el ID o AvailableSlots del grupo esta vacio o Null");
            }

            List<Quotation> matches = QuotationTable.FindAll(quo => (quo.CodProd == quo.CodProd && quo.CodClient == quo.CodClient));
            if ( matches.Count > 0)
            {
                Console.Out.WriteLine("Ya existe una cotizacion con ese CodProd y CodClient");
                throw new Exception("Ya existe una cotizacion con ese CodProd y CodClient");
            }



            QuotationTable.Add(quo);
            return quo;
        }

        public Quotation DeleteQuotation(Quotation quoToDelete)
        {
            if (String.IsNullOrEmpty(quoToDelete.CodClient) || String.IsNullOrEmpty(quoToDelete.CodProd))
            {
                Console.Out.WriteLine("CodProd NULL o CodClient NULL: el ID o AvailableSlots del grupo esta vacio o Null");
                throw new Exception("CodProd NULL o CodClient NULL: el ID o AvailableSlots del grupo esta vacio o Null");
            }
            QuotationTable.RemoveAll(quo => quo.CodProd == quoToDelete.CodProd && quo.CodClient == quoToDelete.CodClient && quo.Sale == quoToDelete.Sale && quo.Price == quoToDelete.Price && quo.Stock == quoToDelete.Stock);
            return quoToDelete;
        }

        public List<Quotation> GetAllQuotations()
        {
            return QuotationTable;
        }

        public Quotation UpdateQuotation(Quotation quoToUpdate)
        {
            if (String.IsNullOrEmpty(quoToUpdate.CodClient) || String.IsNullOrEmpty(quoToUpdate.CodProd))
            {
                Console.Out.WriteLine("CodProd NULL o CodClient NULL: el ID o AvailableSlots del grupo esta vacio o Null");
                throw new Exception("CodProd NULL o CodClient NULL: el ID o AvailableSlots del grupo esta vacio o Null");
            }
            Quotation foundQuotation = QuotationTable.Find(quo => (quo.CodProd == quoToUpdate.CodProd && quo.CodClient == quoToUpdate.CodClient));

            if (foundQuotation == null) { Console.Out.WriteLine("El quo a update es null"); }

            Console.WriteLine($"Updating CodProd: {quoToUpdate.CodProd} CodClient: {quoToUpdate.CodClient}");

            foundQuotation.Sale = quoToUpdate.Sale;
            foundQuotation.Price = quoToUpdate.Price;
            foundQuotation.Stock = quoToUpdate.Stock;

            return foundQuotation;

        }
    }
}
