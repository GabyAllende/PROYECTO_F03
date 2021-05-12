using System;
using System.Collections.Generic;
using System.Text;

namespace UPB.FinalProject.Services.Models
{
    public class Pricing
    {
        public string Code { get; set; }
        public double Price { get; set; }
        public double PromotionPrice { get; set; }
        public int PricingBookId { get; set; }

    }
}
