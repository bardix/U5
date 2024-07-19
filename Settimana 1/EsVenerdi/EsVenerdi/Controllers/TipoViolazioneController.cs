using EsVenerdi.Models;
using EsVenerdi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EsVenerdi.Controllers
{
    public class TipoViolazioneController : Controller
    {
        private readonly TipoViolazioneService _service;

        public TipoViolazioneController(TipoViolazioneService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var list = _service.GetAll();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TipoViolazione tipoViolazione)
        {
            if (ModelState.IsValid)
            {
                _service.Add(tipoViolazione);
                return RedirectToAction("Index");
            }
            return View(tipoViolazione);
        }
    }
}