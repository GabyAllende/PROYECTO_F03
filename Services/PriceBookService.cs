using Serilog;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UPB.FinalProject.Services.Exceptions;
using UPB.FinalProject.Services.Models;

namespace UPB.FinalProject.Services
{
    public class PriceBookService : IPriceBookService
    {
        public async Task<Book> GetAllPrices()
        {
            try
            {
                Book myPriceBook = new Book();
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5002");
                var response = await client.GetAsync("/api/prices");

                string respBody = await response.Content.ReadAsStringAsync();
                myPriceBook = Newtonsoft.Json.JsonConvert.DeserializeObject<Book>(respBody);


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
