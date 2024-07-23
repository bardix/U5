using Microsoft.AspNetCore.Mvc;

public class CamereController : Controller
{
    private readonly ICamereService _camereService;

    public CamereController(ICamereService camereService)
    {
        _camereService = camereService;
    }

    public async Task<IActionResult> Index()
    {
        var camere = await _camereService.GetAllCamereAsync();
        return View(camere);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Camera camera)
    {
        if (ModelState.IsValid)
        {
            await _camereService.AddCameraAsync(camera);
            return RedirectToAction(nameof(Index));
        }
        return View(camera);
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }

        var camera = await _camereService.GetCameraByIdAsync(id);
        if (camera == null)
        {
            return NotFound();
        }
        return View(camera);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Camera camera)
    {
        if (id != camera.Numero)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _camereService.UpdateCameraAsync(camera);
            return RedirectToAction(nameof(Index));
        }
        return View(camera);
    }

    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }

        var camera = await _camereService.GetCameraByIdAsync(id);
        if (camera == null)
        {
            return NotFound();
        }

        return View(camera);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _camereService.DeleteCameraAsync(id);
        return RedirectToAction(nameof(Index));
    }
}
