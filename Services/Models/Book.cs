using System;
using System.Collections.Generic;
using System.Text;


namespace UPB.FinalProject.Services.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Pricing> Products { get; set; }

    }
}
