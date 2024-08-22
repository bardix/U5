using BW2_Team6.Models;
using BW2_Team6.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BW2_Team6.Controllers
{
    [Authorize(Policies.IsPharmacist)]
    public class SellController : Controller
    {
        private readonly ISellService _sellService;
        private readonly IPharmacyService _pharmacyService;
        private readonly IOwnerService _ownerService;

        public SellController(ISellService sellService, IPharmacyService pharmacyService, IOwnerService ownerService)
        {
            _sellService = sellService;
            _pharmacyService = pharmacyService;
            _ownerService = ownerService;
        }

        public async Task<IActionResult> AllSells()
        {
            var sells = await _sellService.GetAll();
            return View(sells);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var sell = await _sellService.GetById(id);
            return View(sell);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _sellService.Delete(id);

            return RedirectToAction("AllSells");
        }

        [HttpPost]
        public async Task<IActionResult> SellProducts (int[] selectedProducts, int ownerId, string numberOfRecipe)
        {
            if(selectedProducts != null && selectedProducts.Length > 0)
            {
                foreach(var productId in selectedProducts)
                {
                    var product = await _pharmacyService.GetProductById(productId);
                    var owner = await _ownerService.Read(ownerId);
                    if(product != null)
                    {
                        var sell = new Sell
                        {
                            Owner = owner,
                            Product = product,
                            NumberOfRecipe = numberOfRecipe
                        };
                        await _sellService.Create(sell);
                    }
                }
            }

            return RedirectToAction("AllSells");
        }

        public IActionResult SearchForPharmacist() 
        { 
            return View(); 
        }
    }
}

