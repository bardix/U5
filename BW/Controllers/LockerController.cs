using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BW2_Team6.Controllers
{
    [Authorize(Policies.IsPharmacist)]
    public class LockerController : Controller
    {

        private readonly ILockerService _lockerSvc;
        public LockerController(ILockerService lockerSvc)
        {
            _lockerSvc = lockerSvc;
        }
        public async Task<IActionResult> AllLockers()
        {
            var locker = await _lockerSvc.GetAll();
            return View(locker);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create(LockerViewModel locker)
        {
            if(ModelState.IsValid)
            {
                await _lockerSvc.Create(locker);
                return RedirectToAction("AllLockers");
            }
            return View(locker);
        }
        public async Task<IActionResult> Update(int id)
        {
            var locker = await _lockerSvc.ReadV(id);
            return View(locker);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, LockerViewModel locker)
        {
            if (ModelState.IsValid)
            {
                await _lockerSvc.Update(id, locker);
                return RedirectToAction("AllLockers");
            } else
            {
                return View(locker); 
            }
        }

            public async Task<IActionResult> Delete(int id)
        {
            var locker = await _lockerSvc.Read(id);
            await _lockerSvc.Delete(locker.Id);
            return RedirectToAction("AllLockers");
        }
    }
}
