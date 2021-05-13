using Microsoft.Extensions.Configuration;
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

        public IConfiguration _config;
        public List<Quotation> QuotationTable { get; set; }

        string[] cat = { "SOCCER",  "BASKET" };
        public DbContext(IConfiguration config) 
        {
            _config = config;
            //============CHICOSS AQUI TIENE QUE ESTAR LA CONEXION CON LA BASE DE DATOS JSON==============
            //EL QuotationTable debe estar inicializado con los contenidos de la base de datos
            //Ej
            //var list = JsonConvert.DeserializeObject<List<Person>>(myJsonString);
            //list.Add(new Person(1234, "carl2");
            //var convertedJson = JsonConvert.SerializeObject(list, Formatting.Indented);
            //https://stackoverflow.com/questions/33081102/json-add-new-object-to-existing-json-file-c-sharp/33081258


            //Obtenemos la direccion del .json con ayuda del _config
            //Devolvera "C:\\Users\\Acer Aspie 3\\Documents\\CERTIFICACION 1\\PARCIAL 3\\ejemploTest\\PROYECTO_F03\\Data\\Database"
            string myJsonString = System.IO.File.ReadAllText(_config.GetSection("ConnectionStrings").GetSection("DBPath").Value);
            var QuotationTable = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Quotation>>(myJsonString);
            //QuotationTable = new List<Quotation>();
            foreach (var item in QuotationTable)
            {
                QuotationTable.Add(new Quotation()
                {
                    CodProd = item.CodProd,
                    CodClient = item.CodClient,
                    Quantity = item.Quantity,
                    Sale = item.Sale
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
