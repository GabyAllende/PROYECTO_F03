using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using UPB.FinalProject.Logic.Models;
using UPB.FinalProject.Logic.Managers;

namespace PR_F02.Controllers
{
    [ApiController]
    [Route("/api/quotations")]
    public class QuotationsController : ControllerBase
    {
        private static IConfiguration _config;
        private readonly IQuotationManager _quotationManager; 
        private readonly ILogger<QuotationsController> _logger;

        public QuotationsController(IConfiguration config,IQuotationManager quotationManager)

        {
            _config = config;
            _quotationManager = quotationManager;
        }

        [HttpGet]
        public List<Quotation> GetGroup()
        {
            return _quotationManager.GetAllQuotations();
        }

        [HttpPost]
        public Quotation CreateGroup([FromBody] Quotation quo)//, [FromBody] string studentLastName ) 
        {
            return _quotationManager.CreateQuotation(quo);
        }
        [HttpPut]
        public Quotation UpdateGroup([FromBody] Quotation quo)
        {
            return _quotationManager.UpdateQuotation(quo);
        }

        [HttpDelete]
        public Quotation DeleteGroup([FromBody] Quotation quo)
        {
            return _quotationManager.DeleteQuotation(quo);
        }
    }
}
