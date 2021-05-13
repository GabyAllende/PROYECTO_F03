using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UPB.FinalProject.Services.Exceptions;
using UPB.FinalProject.Services.Models;

namespace UPB.FinalProject.Services
{
    public class PriceBookService : IPriceBookService
    {
        public async Task<PricingBook> GetAllPrices()
        {
            try
            {
                List<PricingBook> ListBook = new List<PricingBook>();
                PricingBook myPriceBook;
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5004");
                var response = await client.GetAsync("/api/pricing-books");

                string respBody = await response.Content.ReadAsStringAsync();
                ListBook = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PricingBook>>(respBody);
                myPriceBook = ListBook.Last();


                return myPriceBook;
            }
            
            catch (Exception ex)
            {
                Log.Error("The error was: " + ex.StackTrace + ex.Message);
                throw new ServiceException("Can not connect to service");
            }
        }
    }
}
