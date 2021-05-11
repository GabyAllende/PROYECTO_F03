using System;
using System.Collections.Generic;
using System.Text;
using UPB.FinalProject.Logic.Models;
using UPB.FinalProject.Data;
using UPB.FinalProject.Services;
using UPB.FinalProject.Services.Models;

namespace UPB.FinalProject.Logic.Managers
{
    public class QuotationManager : IQuotationManager
    {
        private IDbContext _dbContext;

        private readonly IPriceBookService _priceBookService;

        public List<Price> GetAllPrices() 
        {
            return _priceBookService.GetAllPrices().Result;
        }

        public QuotationManager(IDbContext dbContext, IPriceBookService priceBookService)
        {
            _dbContext = dbContext;
            _priceBookService = priceBookService;
        }
        public Quotation CreateQuotation(Quotation quo)
        {
            _dbContext.AddQuotation(DTOMappers.MapGroupLD(quo));
            return quo;
        }

        public Quotation DeleteQuotation(Quotation quo)
        {
            Data.Models.Quotation qu = _dbContext.DeleteQuotation(DTOMappers.MapGroupLD(quo));
            return DTOMappers.MapQuotationDL(qu);
        }

        public List<Quotation> GetAllQuotations()
        {
            List<Data.Models.Quotation> quo = _dbContext.GetAllQuotations();
            List<Price> myPriceBook = _priceBookService.GetAllPrices().Result;

            Console.Out.WriteLine("=================LISTA DE PRECIOS: ====================");
            foreach (var p in myPriceBook) 
            {
                Console.WriteLine($"Precio CodProd: {p.CodProd} SetPrice: {p.SetPrice} PromotionPrice: {p.PromotionPrice}");
            }

            Console.Out.WriteLine("==================LISTA DE COTIZACIONES===================");
            foreach (var prod in quo) 
            {
                Price precioProd = myPriceBook.Find(pr => pr.CodProd == prod.CodProd);
                double miPrecio = 0;
                if (precioProd != null)
                {
                    if (precioProd.PromotionPrice == 0)
                    {
                        miPrecio = precioProd.SetPrice;
                    }
                    else
                    {
                        miPrecio = precioProd.PromotionPrice;
                    }
                }
                else 
                {
                    Console.WriteLine($"NO SE ENCONTRO EL CODIGO: {prod.CodProd}");
                }
                
                prod.Price = miPrecio;
                Console.Out.WriteLine($"CodProd: {prod.CodProd} CodCliente: {prod.CodClient} Price: {prod.Price}");
            }

            return DTOMappers.MapQuotations(quo);
        }

        public Quotation UpdateQuotation(Quotation quo)
        {
            Data.Models.Quotation qu = _dbContext.UpdateQuotation(DTOMappers.MapGroupLD(quo));
            return DTOMappers.MapQuotationDL(qu);
        }
    }
}
