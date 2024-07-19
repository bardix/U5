using EsVenerdi.Models;
using EsVenerdi.Services;
using Microsoft.AspNetCore.Mvc;

namespace EsVenerdi.Controllers
{
    public class VerbaleController : Controller
    {
        private readonly VerbaleService _service;

        public VerbaleController(VerbaleService service)
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
        public IActionResult Create(Verbale verbale)
        {
            if (ModelState.IsValid)
            {
                _service.Add(verbale);
                return RedirectToAction("Index");
            }
            return View(verbale);
        }
    }
}