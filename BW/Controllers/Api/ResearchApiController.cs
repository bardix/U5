using BW2_Team6.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BW2_Team6.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResearchApiController : ControllerBase
    {
        private readonly IPharmacyService _pharmacySvc;

        public ResearchApiController(IPharmacyService pharmacySvc)
        {
            _pharmacySvc = pharmacySvc;
        }

        [HttpGet("{fiscalcode}")]
        public async Task<IActionResult> SellsByFiscalCode(string fiscalcode)
        {
            var visit = await _pharmacySvc.GetSellsByFiscalCode(fiscalcode);
            return Ok(visit);
        }

        [HttpGet("SearchProduct/{id}")]
        public async Task<IActionResult> SearchProduct(int id)
        {
            var product = await _pharmacySvc.SearchProduct(id);
            return Ok(product);
        }

        [HttpGet("SellByDate/{data}")]

        public async Task<IActionResult> SellByDate(DateTime data)
        {
            var sell = await _pharmacySvc.GetSellsByDate(data);
            return Ok(sell);
        }


    }
}
