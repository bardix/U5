using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BW2_Team6.Controllers
{
    [Authorize(Policies.IsPharmacist)]
    public class CompaniesController : Controller
    {
        private readonly ICompaniesService _companiesSvc;

        public CompaniesController(ICompaniesService companiesService)
        {
            _companiesSvc = companiesService;
        }
        public async Task<IActionResult> AllCompanies()
        {
            var companies = await _companiesSvc.GetAll();
            return View(companies);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Company company)
        {
            if (ModelState.IsValid) {
                await _companiesSvc.Create(company);
                return RedirectToAction("AllCompanies");
            } else
                { 
                return View(company); 
            }
        }

        public async Task<IActionResult> Update(int id)
        {
            var company = await _companiesSvc.Read(id);
            return View(company);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, Company company) 
        {
            if (ModelState.IsValid)
            {
                await _companiesSvc.Update(id, company);
                return RedirectToAction("AllCompanies");
            }
            else
            {
                return View(company);
            }
        }
    public async Task<IActionResult> Delete(int id)
    {
        var visit = await _companiesSvc.Read(id);
        return View(visit);
    }

    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _companiesSvc.Delete(id);
        return RedirectToAction("AllCompanies");

    }
    }

}
