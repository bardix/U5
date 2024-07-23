using Microsoft.AspNetCore.Mvc;

public class ClientiController : Controller
{
    private readonly IClientiService _clientiService;

    public ClientiController(IClientiService clientiService)
    {
        _clientiService = clientiService;
    }

    public async Task<IActionResult> Index()
    {
        var clienti = await _clientiService.GetAllClientiAsync();
        return View(clienti);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Cliente cliente)
    {
        if (ModelState.IsValid)
        {
            await _clientiService.AddClienteAsync(cliente);
            return RedirectToAction(nameof(Index));
        }
        return View(cliente);
    }

    public async Task<IActionResult> Edit(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cliente = await _clientiService.GetClienteByIdAsync(id);
        if (cliente == null)
        {
            return NotFound();
        }
        return View(cliente);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string id, Cliente cliente)
    {
        if (id != cliente.CodiceFiscale)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _clientiService.UpdateClienteAsync(cliente);
            return RedirectToAction(nameof(Index));
        }
        return View(cliente);
    }

    public async Task<IActionResult> Delete(string id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var cliente = await _clientiService.GetClienteByIdAsync(id);
        if (cliente == null)
        {
            return NotFound();
        }

        return View(cliente);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string id)
    {
        await _clientiService.DeleteClienteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    private async Task<bool> ClienteExists(string id)
    {
        return await _clientiService.ClienteExistsAsync(id);
    }
}
