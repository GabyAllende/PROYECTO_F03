using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UPB.FinalProject.Services.Models;

namespace UPB.FinalProject.Services
{
    public class PriceBookService : IPriceBookService
    {
        public async Task<List<Price>> GetAllPrices()
        {
            List<Price> myPriceBook = new List<Price>();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5002");
            var response = await client.GetAsync("/api/prices");

            string respBody = await response.Content.ReadAsStringAsync();
            myPriceBook = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Price>>(respBody);


            return myPriceBook;
        }
    }
}
