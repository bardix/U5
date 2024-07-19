using EsVenerdi.Models;
using EsVenerdi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EsVenerdi.Controllers
{
    public class AnagraficaController : Controller
    {
        private readonly AnagraficaService _service;

        public AnagraficaController(AnagraficaService service)
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
        public IActionResult Create(Anagrafica anagrafica)
        {
            if (ModelState.IsValid)
            {
                _service.Add(anagrafica);
                return RedirectToAction("Index");
            }
            return View(anagrafica);
        }
    }
}