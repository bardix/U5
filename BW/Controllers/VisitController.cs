using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BW2_Team6.Controllers
{
    [Authorize(Policies.IsVeterinarian)]
    public class VisitController : Controller
    {

        private readonly IVisitService _visitSvc;
        public VisitController(IVisitService visitService)
        {
            _visitSvc = visitService;
        }
        public async Task<IActionResult> AllVisits()
        {
            var visits = await _visitSvc.GetAll();
            return View(visits);
        }

        public IActionResult Create(int id)
        {
            ViewBag.animalId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int animalId, VisitViewModel visit)
        {
            if (ModelState.IsValid)
            {
                await _visitSvc.Create(animalId, visit);
                return RedirectToAction("AllVisits", "Visit");
            }
            return View(visit);
        }

        public async Task<IActionResult> Update(int id)
        {
            var visit = await _visitSvc.GetById(id);
            var visitsModel = new VisitViewModel
            {
                TypeOfCure = visit.TypeOfCure,
                TypeOfExam = visit.TypeOfExam,
            };
            return View(visitsModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, VisitViewModel visit)
        {
            if (ModelState.IsValid)
            {
                await _visitSvc.Update(id, visit);
                return RedirectToAction("AllVisits", "Visit");
            }
            return View(visit);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var visit = await _visitSvc.GetById(id);
            return View(visit);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _visitSvc.Delete(id);
            return RedirectToAction("AllVisits", "Visit");

        }

        public async Task<IActionResult> DetailsVisit(int id)
        {

            var visits = await _visitSvc.AllVisitByAnimalId(id);
            return Json(visits);
        }
    }
}
