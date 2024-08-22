using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace BW2_Team6.Controllers
{
    [Authorize(Policies.IsVeterinarian)]
    public class RecoverController : Controller
    {
        private readonly IRecoverService _recoverSvc;

        public RecoverController(IRecoverService recoverService)
        {
            _recoverSvc = recoverService;
        }
        public async Task<IActionResult> AllRecovers()
        {
            var recovers = await _recoverSvc.GetAll();
            return View(recovers);
        }

        public IActionResult Create(int id)
        {
            ViewBag.animalId = id;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int animalId, RecoverViewModel entity)
        {
            if (ModelState.IsValid)
            {
                await _recoverSvc.Create(animalId, entity);
                return RedirectToAction("AllRecovers", "Recover");
            } else
            {
                return View(entity);
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            var recover = await _recoverSvc.Read(id);
            var recoverModel = new RecoverViewModel
            {
                ExistingImagePath = recover.Image,
            };
            return View(recoverModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, RecoverViewModel entity)
        {
            if (ModelState.IsValid)
            {
                var recover = await _recoverSvc.Update(id, entity);
               return RedirectToAction("AllRecovers", "Recover");
            }
            else
            {
                return View(entity);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            var recover = await _recoverSvc.Read(id);
            return View(recover);
        }

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recover = await _recoverSvc.Delete(id);
            return RedirectToAction("AllRecovers", "Recover");
        }

        [AllowAnonymous]
        public IActionResult SearchYourAnimal()
        {
             return View(); 
        }
        public IActionResult RecoverAnimal()
        {
             return View(); 
        }

       public async Task<IActionResult> ChangeStatus(int id)
        {
            await _recoverSvc.ChangeStatus(id);
            return RedirectToAction("AllRecovers", "Recover");
        }
    }
}
