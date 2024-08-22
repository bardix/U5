
using BW2_Team6.Context;
using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BW2_Team6.Controllers
{
    [Authorize(Policies.IsVeterinarian)]
    public class AnimalController : Controller
    {
        private readonly IAnimalService _animalService;
        private readonly IOwnerService _ownerService;
        private readonly DataContext _dbContext;

        public AnimalController (IAnimalService animalService, IOwnerService ownerService, DataContext dbContext)
        {
            _animalService = animalService;
            _ownerService = ownerService;
            _dbContext = dbContext;
        }

        public async Task<IActionResult> AllAnimals()
        {
            var animals = await _animalService.GetAll();
            return View(animals);
        }

        public async Task<IActionResult> Create()
        {
            var owners = await _ownerService.GetAll();
            ViewBag.Owners = new SelectList(owners, "Id", "FirstName");
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _animalService.Delete(id);
            return RedirectToAction("AllAnimals");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var animal = await _animalService.GetById(id);
            var owners = await _ownerService.GetAll();
            ViewBag.Owners = new SelectList(owners, "Id", "FirstName");

            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Animal newAnimal, int ownerId)
        {
            if (ModelState.IsValid)
            {
                if (ownerId == 0)
                {
                    newAnimal.Owner = null;
                }
                else
                {
                    newAnimal.Owner = await _dbContext.Owners.FindAsync(ownerId);
                }

                await _animalService.Create(newAnimal);
                return RedirectToAction("AllAnimals");
            }

            var allOwners = await _ownerService.GetAll();
            ViewBag.Owners = new SelectList(allOwners, "Id", "FirstName");

            return View(newAnimal);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Animal updateAnimal, int ownerId)
        {
            if (ModelState.IsValid)
            {
                if (ownerId == 0)
                {
                    updateAnimal.Owner = null;
                }
                else
                {
                    updateAnimal.Owner = await _dbContext.Owners
                    .FindAsync(ownerId);
                }
                await _animalService.Update(updateAnimal.Id, updateAnimal);
                return RedirectToAction("AllAnimals");
            }

            return View(updateAnimal);
        }

    }
}

