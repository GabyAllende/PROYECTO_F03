using System;
using System.Collections.Generic;
using System.Text;
using UPB.FinalProject.Data.Models;
using UPB.FinalProject.Services.Models;

namespace UPB.FinalProject.Data
{
    public class DbContext : IDbContext
    {
        public int cont { get; set; }

        public List<Quotation> QuotationTable { get; set; }

        string[] cat = { "SOCCER",  "BASKET" };
        public DbContext() 
        {
            //============CHICOSS AQUI TIENE QUE ESTAR LA CONEXION CON LA BASE DE DATOS JSON==============
            //EL QuotationTable debe estar inicializado con los contenidos de la base de datos
            //Ej
            //var list = JsonConvert.DeserializeObject<List<Person>>(myJsonString);
            //list.Add(new Person(1234, "carl2");
            //var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
            //https://stackoverflow.com/questions/33081102/json-add-new-object-to-existing-json-file-c-sharp/33081258


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
                
                QuotationTable.Add(new Quotation()
                {
                    
                    CodProd = cat[r.Next(0, 2)]+"-"+med,
                    CodClient = "MTR-"+med2,
                   Id = cont,
                   Sale = false,
                   Quantity = r.Next(0,51)
                  

                });
            }
        }
        public Quotation AddQuotation(Quotation quo)
        {
            
            List<Quotation> matches = QuotationTable.FindAll(qu => (qu.Id == quo.Id ));
            if ( matches.Count > 0)
            {
                Console.Out.WriteLine($"Ya existe una cotizacion con ese Id: {quo.Id}");
                throw new Exception($"Ya existe una cotizacion con ese Id: {quo.Id}");
            }



            QuotationTable.Add(quo);
            return quo;
        }

        public int DeleteQuotation(int id)
        {
            
            int deleted = QuotationTable.RemoveAll(quo => quo.Id ==id);
            return deleted;
        }

        public List<Quotation> GetAllQuotations()
        {

            
            return QuotationTable;
        }

        public Quotation UpdateQuotation(int id , string codProd, int quantity)
        {
           
            Quotation foundQuotation = QuotationTable.Find(quo => (quo.Id == id ));

            

            Console.WriteLine($"Updating CodProd: { foundQuotation.CodProd} CodClient: { foundQuotation.CodClient}");

            //foundQuotation.Sale = quoToUpdate.Sale;
            //foundQuotation.Price = quoToUpdate.Price;
            foundQuotation.CodProd = codProd;
            foundQuotation.Quantity = quantity;

            return foundQuotation;

        }


        public Quotation UpdateSaleTrue(int id) 
        {
            //var list = JsonConvert.DeserializeObject<List<Person>>(myJsonString);
            Quotation foundQuotation = QuotationTable.Find(qu => (qu.Id == id));
            foundQuotation.Sale = true;

            return foundQuotation;
        }
        public Quotation UpdateSaleFalse(int id)
        {
            Quotation foundQuotation = QuotationTable.Find(qu => (qu.Id == id));
            foundQuotation.Sale = false;
            return foundQuotation;
        }
    }
}
