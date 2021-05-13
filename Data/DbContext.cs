using Grpc.Core;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Collections.Generic;
using System.Text;
using UPB.FinalProject.Data.Models;
using UPB.FinalProject.Services.Models;

namespace UPB.FinalProject.Data
{
    public class DbContext : IDbContext
    {
        public int cont { get; set; }

        public IConfiguration _config;
        public List<Quotation> QuotationTable { get; set; }

        string[] cat = { "SOCCER",  "BASKET" };

        string MyUser = "Gaby";

        public DbContext(IConfiguration config) 
        {
            _config = config;
           
           
            string myJsonString = System.IO.File.ReadAllText("" + _config.GetSection("ConnectionStrings").GetSection("DBPath").GetSection(MyUser).Value);
            var quotationTable = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Quotation>>(myJsonString);
            QuotationTable = new List<Quotation>();
            foreach (var item in quotationTable)
            {
                QuotationTable.Add(new Quotation()
                {
                    Id = item.Id,
                    CodProd = item.CodProd,
                    CodClient = item.CodClient,
                    Quantity = item.Quantity,
                    Sale = item.Sale
                });
                Console.Out.WriteLine("Se agrego: \n"+item.Id);
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
            
            string convertedJson = Newtonsoft.Json.JsonConvert.SerializeObject(QuotationTable, Formatting.None);
            System.IO.File.WriteAllText(""+_config.GetSection("ConnectionStrings").GetSection("DBPath").GetSection(MyUser).Value, convertedJson);

            return quo;
        }   

        public int DeleteQuotation(int id)
        {
            int deleted = QuotationTable.RemoveAll(quo => quo.Id ==id);

            string convertedJson = Newtonsoft.Json.JsonConvert.SerializeObject(QuotationTable, Formatting.None);
            System.IO.File.WriteAllText("" + _config.GetSection("ConnectionStrings").GetSection("DBPath").GetSection(MyUser).Value, convertedJson);
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

            foundQuotation.CodProd = codProd;
            foundQuotation.Quantity = quantity;

            string convertedJson = Newtonsoft.Json.JsonConvert.SerializeObject(QuotationTable, Formatting.None);
            System.IO.File.WriteAllText("" + _config.GetSection("ConnectionStrings").GetSection("DBPath").GetSection(MyUser).Value, convertedJson);
            return foundQuotation;
        }


        public Quotation UpdateSaleTrue(int id) 
        {
            
            Quotation foundQuotation = QuotationTable.Find(qu => (qu.Id == id));
            foundQuotation.Sale = true;

            string convertedJson = Newtonsoft.Json.JsonConvert.SerializeObject(QuotationTable, Formatting.None);
            System.IO.File.WriteAllText("" + _config.GetSection("ConnectionStrings").GetSection("DBPath").GetSection(MyUser).Value, convertedJson);

            return foundQuotation;
        }
        public Quotation UpdateSaleFalse(int id)
        {
            Quotation foundQuotation = QuotationTable.Find(qu => (qu.Id == id));
            foundQuotation.Sale = false;

            string convertedJson = Newtonsoft.Json.JsonConvert.SerializeObject(QuotationTable, Formatting.None);
            System.IO.File.WriteAllText("" + _config.GetSection("ConnectionStrings").GetSection("DBPath").GetSection(MyUser).Value, convertedJson);
            return foundQuotation;
        }
    }
}
