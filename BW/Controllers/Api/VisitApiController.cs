using BW2_Team6.Context;
using BW2_Team6.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BW2_Team6.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitApiController : ControllerBase
    {
        private readonly IVisitService _visitSvc;
        public VisitApiController(IVisitService visitSvc)
        {
            _visitSvc = visitSvc;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> DetailsVisit(int id)
        {
            var visits = await _visitSvc.AllVisitByAnimalId(id);
            return Ok(visits);
        }
    }
}
