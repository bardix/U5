using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BW2_Team6.Controllers
{
    [Authorize(Policies.IsPharmacist)]
    public class PharmacyController : Controller
    {
        private readonly IPharmacyService _pharmacyService;
        private readonly ICompaniesService _companiesSvc;
        private readonly IOwnerService _ownerService;

        public PharmacyController(IPharmacyService pharmacyService, ICompaniesService companiesService, IOwnerService ownerService)
        {
            _pharmacyService = pharmacyService;
            _companiesSvc = companiesService;
            _ownerService = ownerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> AllProducts(string filter = "all")
        {
            IEnumerable<Product> products;

            if (filter == "all")
            {
                products = await _pharmacyService.GetAllProducts();
            }
            else
            {
                products = await _pharmacyService.GetFilteredProducts(new string[] { filter });
            }

            var owner = await _ownerService.GetAll();
            ViewBag.Owners = new SelectList(owner, "Id", "FirstName");
            ViewBag.Filter = filter;
            return View(products);
        }



        [HttpGet]
        public async Task<IActionResult> DetailProduct(int id)
        {
            var product = await _pharmacyService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        public async Task<IActionResult> CreateProduct()
        {
            var companies = await _companiesSvc.GetAll();
            ViewBag.Companies = new SelectList(companies, "Id", "Name");
            ViewBag.ProductTypes = new SelectList(new List<string> { "alimentari", "prodotti farmaceutici" });
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(Product product, int companyId)
        {
            if (companyId == 0)
            {
                ViewBag.ErrorMessage = "Please select a company.";
                var companies = await _companiesSvc.GetAll();
                ViewBag.Companies = new SelectList(companies, "Id", "Name");
                ViewBag.ProductTypes = new SelectList(new List<string> { "alimentari", "prodotti farmaceutici" });
                return View(product);
            }

            product.Company = await _companiesSvc.Read(companyId);
            await _pharmacyService.CreateProduct(product);
            return RedirectToAction("AllProducts");
        }


        [HttpGet]
        public async Task<IActionResult> EditProduct(int id)
        {
            var companies = await _companiesSvc.GetAll();
            ViewBag.Companies = new SelectList(companies, "Id", "Name");
            ViewBag.ProductTypes = new SelectList(new List<string> { "alimentari", "prodotti farmaceutici" });

            var product = await _pharmacyService.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(int id, Product product, int companyId)
        {
            if (companyId == 0)
            {
                ViewBag.ErrorMessage = "Please select a company.";
                var companies = await _companiesSvc.GetAll();
                ViewBag.Companies = new SelectList(companies, "Id", "Name");
                ViewBag.ProductTypes = new SelectList(new List<string> { "alimentari", "prodotti farmaceutici" });
                return View(product);
            }

            product.Company = await _companiesSvc.Read(companyId);
            var updatedProduct = await _pharmacyService.UpdateProduct(id, product);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return RedirectToAction("AllProducts", "Pharmacy");
        }


        [HttpGet]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _pharmacyService.GetProductById(id);
            return View(product);
        }

        [HttpPost, ActionName("DeleteProduct")]
        public async Task<ActionResult> DeleteConfirmedProduct(int id)
        {
            await _pharmacyService.DeleteProduct(id);
            return RedirectToAction("AllProducts", "Pharmacy");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sell>>> SellsByCustomer(int id)
        {
            var sales = await _pharmacyService.GetSellsByCustomer(id);
            return View(sales);
        }
    }
}
