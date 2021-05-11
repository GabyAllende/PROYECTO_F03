using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UPB.FinalProject.Services.Models;

namespace UPB.FinalProject.Services
{
    public interface IPriceBookService
    {
        public Task<List<Price>> GetAllPrices();
    }
}
