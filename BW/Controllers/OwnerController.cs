using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BW2_Team6.Controllers
{
    [Authorize(Policies.IsVeterinarian)]
    public class OwnerController : Controller
    {
        private readonly IOwnerService _ownerService;

        public OwnerController(IOwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Owner>>> AllOwner()
        {
            var owners = await _ownerService.GetAll();
            return View(owners);
        }


        public IActionResult CreateOwner() 
        {
            return View();
                
        }


        [HttpPost]
        public async Task<ActionResult<Owner>> CreateOwner(Owner owner)
        {
           
           if (ModelState.IsValid)
            {
                await _ownerService.Create(owner);
                return RedirectToAction("AllOwner","Owner");
            }
            return View(owner);

        }

        public async Task<IActionResult> EditOwner(int id)
        {
            var owner = await _ownerService.Read(id);
            return View(owner);
        }

        [HttpPost]
        public async Task<ActionResult<Owner>> EditOwner(int id, Owner owner)
        {
            var updatedOwner = await _ownerService.Update(id, owner);
            if (updatedOwner == null)
            {
                return NotFound();
            }
            return RedirectToAction("AllOwner","Owner");
        }
       
        public async Task <IActionResult> DeleteOwner(int id)
        {
            var owner = await _ownerService.Read(id);
            return View(owner);

        }

        public async Task<ActionResult> DeleteConfirmedOwner(int id)
        {
            await _ownerService.Delete(id);
            return RedirectToAction("AllOwner", "Owner");
        }
    }
}
