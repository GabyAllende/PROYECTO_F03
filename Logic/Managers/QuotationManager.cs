using System;
using System.Collections.Generic;
using System.Text;
using UPB.FinalProject.Logic.Models;
using UPB.FinalProject.Data;
using UPB.FinalProject.Services;
using UPB.FinalProject.Services.Models;
using System.Linq;

namespace UPB.FinalProject.Logic.Managers
{
    public class QuotationManager : IQuotationManager
    {
        private IDbContext _dbContext;
        private int counter;

        private readonly IPriceBookService _priceBookService;

        public QuotationManager(IDbContext dbContext, IPriceBookService priceBookService)
        {
            _dbContext = dbContext;
            _priceBookService = priceBookService;
            counter = (int)(_dbContext.GetAllQuotations().Last().Id);
        }

        public PricingBook GetAllPrices()
        {
            return _priceBookService.GetAllPrices().Result;
        }
        // POST
        public Quotation CreateQuotation(Quotation quo)
        {
            // serialize
            counter += 1;

            if (String.IsNullOrEmpty(quo.CodClient) || String.IsNullOrEmpty(quo.CodProd) || quo.Quantity <= 0 )
            {
                Console.Out.WriteLine("CodProd NULL o CodClient NULL o Cantidad <= 0 ");
                throw new Exception("CodProd NULL o CodClient NULL o Cantidad <= 0");
            }

            PricingBook myBook = _priceBookService.GetAllPrices().Result;
            List<Product> myPriceBook = myBook.Content;
            Product precioProd = myPriceBook.Find(pr => pr.ProductId == quo.CodProd);

            quo.Id = counter;
            //quo.Price = precioProd != null ? (precioProd.PromotionPrice != 0 ? precioProd.PromotionPrice : precioProd.Price) : 0;
            quo.Sale = false;

            _dbContext.AddQuotation(DTOMappers.MapGroupLD(quo));



            return quo;
        }
        // DEL
        public int DeleteQuotation(int id)
        {
            if (id<= 0)
            {
                Console.Out.WriteLine("id incorrecto para eliminar");
                throw new Exception("id incorrecto para eliminar");
            }

            int deleted = _dbContext.DeleteQuotation(id);
            return deleted;
        }
        // GET
        public List<Quotation> GetAllQuotations()
        {
           
            PricingBook myBook = _priceBookService.GetAllPrices().Result;
            List<Product> myPriceBook = myBook.Content;
            List<Data.Models.Quotation> quotations = _dbContext.GetAllQuotations();
            List<Quotation> quots = DTOMappers.MapQuotations(quotations);

            Console.Out.WriteLine("=================LISTA DE PRECIOS: ====================");
            foreach (var p in myPriceBook)
            {
                Console.WriteLine($"Precio CodProd: {p.ProductId} SetPrice: {p.FixedPrice} PromotionPrice: {p.PromotionPrice}");
            }

            Console.Out.WriteLine("==================LISTA DE COTIZACIONES===================");
            foreach (var qu in quots)
            {
                Product precioProd = myPriceBook.Find(pr => pr.ProductId == qu.CodProd);
                double miPrecio = 0;
                if (precioProd != null)
                {
                    if (precioProd.PromotionPrice == 0)
                    {
                        miPrecio = precioProd.FixedPrice;
                    }
                    else
                    {
                        miPrecio = precioProd.PromotionPrice;
                    }
                }
                else
                {
                    Console.WriteLine($"NO SE ENCONTRO EL CODIGO: {qu.CodProd}");
                }

                //qu.Price = miPrecio;
                Console.Out.WriteLine($"Id: {qu.Id} CodProd: {qu.CodProd} CodCliente: {qu.CodClient}" /*Price: {qu.Price}"*/);
            }

            
            return quots;
        }

        // PUT
        public Quotation UpdateQuotation(Quotation quo)
        {
            if (quo.Id<=0 || quo.Quantity <=0 || (String.IsNullOrEmpty(quo.CodProd)))
            {
                Console.Out.WriteLine("Id o cantidad incorrecta o codigo de Producto erroneo");
                throw new Exception("Id cantidad incorrecta o codigo de Producto erroneo");
            }

            Data.Models.Quotation qu = _dbContext.UpdateQuotation(quo.Id, quo.CodProd, quo.Quantity);
            return DTOMappers.MapQuotationDL(qu);
        }


        public Quotation UpdateSaleTrue(int id)
        {
            Data.Models.Quotation qu = _dbContext.UpdateSaleTrue(id);
            return DTOMappers.MapQuotationDL(qu);
        }
        public Quotation UpdateSaleFalse (int id)
        {
            Data.Models.Quotation qu = _dbContext.UpdateSaleFalse(id);
            return DTOMappers.MapQuotationDL(qu);
        }
    }
}
