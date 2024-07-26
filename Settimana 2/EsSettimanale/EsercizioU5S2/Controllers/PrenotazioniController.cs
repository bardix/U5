using _1BW_BE.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

[Authorize(Policy = "DipendentePolicy")]
public class PrenotazioniController : Controller
{
    private readonly IPrenotazioniService _prenotazioniService;
    private readonly IClientiService _clientiService;
    private readonly ICamereService _camereService;
    private readonly IServiziService _serviziService;
    private readonly IServizioPrenotazioneService _servizioPrenotazioneService;

    public PrenotazioniController(IPrenotazioniService prenotazioniService, IClientiService clientiService, ICamereService camereService, IServiziService serviziService, IServizioPrenotazioneService servizioPrenotazioneService)
    {
        _prenotazioniService = prenotazioniService;
        _clientiService = clientiService;
        _camereService = camereService;
        _serviziService = serviziService;
        _servizioPrenotazioneService = servizioPrenotazioneService;
    }

    public async Task<IActionResult> Index()
    {
        var prenotazioni = await _prenotazioniService.GetAllPrenotazioniAsync();
        return View(prenotazioni);
    }

    public async Task<IActionResult> Create()
    {
        ViewBag.Clienti = new SelectList(await _clientiService.GetAllClientiAsync(), "CodiceFiscale", "NomeCompleto");
        ViewBag.Camere = new SelectList(await _camereService.GetAllCamereAsync(), "Numero", "Descrizione");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("CodiceFiscaleCliente,NumeroCamera,DataPrenotazione,NumeroProgressivo,Anno,DataInizioSoggiorno,DataFineSoggiorno,Caparra,Tariffa,Dettagli")] Prenotazione prenotazione)
    {
        if (ModelState.IsValid)
        {
            await _prenotazioniService.AddPrenotazioneAsync(prenotazione);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Clienti = new SelectList(await _clientiService.GetAllClientiAsync(), "CodiceFiscale", "NomeCompleto");
        ViewBag.Camere = new SelectList(await _camereService.GetAllCamereAsync(), "Numero", "Descrizione");
        return View(prenotazione);
    }

    public async Task<IActionResult> Edit(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }

        var prenotazione = await _prenotazioniService.GetPrenotazioneByIdAsync(id);
        if (prenotazione == null)
        {
            return NotFound();
        }

        ViewBag.Clienti = new SelectList(await _clientiService.GetAllClientiAsync(), "CodiceFiscale", "NomeCompleto");
        ViewBag.Camere = new SelectList(await _camereService.GetAllCamereAsync(), "Numero", "Descrizione");

        return View(prenotazione);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("IDPrenotazione,CodiceFiscaleCliente,NumeroCamera,DataPrenotazione,NumeroProgressivo,Anno,DataInizioSoggiorno,DataFineSoggiorno,Caparra,Tariffa,Dettagli")] Prenotazione prenotazione)
    {
        if (id != prenotazione.IDPrenotazione)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            await _prenotazioniService.UpdatePrenotazioneAsync(prenotazione);
            return RedirectToAction(nameof(Index));
        }

        ViewBag.Clienti = new SelectList(await _clientiService.GetAllClientiAsync(), "CodiceFiscale", "NomeCompleto");
        ViewBag.Camere = new SelectList(await _camereService.GetAllCamereAsync(), "Numero", "Descrizione");

        return View(prenotazione);
    }

    public async Task<IActionResult> Delete(int id)
    {
        if (id == 0)
        {
            return NotFound();
        }

        var prenotazione = await _prenotazioniService.GetPrenotazioneByIdAsync(id);
        if (prenotazione == null)
        {
            return NotFound();
        }

        return View(prenotazione);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _prenotazioniService.DeletePrenotazioneAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "Dipendente")]
    public IActionResult GestionePrenotazioni()
    {
        return View();
    }

    [Authorize(Roles = "Dipendente")]
    public async Task<IActionResult> StampaDettaglioPrenotazione(int id)
    {
        var prenotazione = await _prenotazioniService.GetPrenotazioneByIdAsync(id);
        if (prenotazione == null)
        {
            return NotFound();
        }

        var servizi = await _servizioPrenotazioneService.GetServiziPrenotazioniByPrenotazioneIdAsync(id);
        var totaleDaSaldare = prenotazione.Tariffa - prenotazione.Caparra + servizi.Sum(s => s.Prezzo * s.Quantità);

        var viewModel = new DettaglioPrenotazioneViewModel
        {
            Prenotazione = prenotazione,
            Servizi = servizi,
            TotaleDaSaldare = totaleDaSaldare
        };

        return View(viewModel);
    }

    [Authorize(Roles = "Dipendente")]
    public async Task<IActionResult> RicercaPrenotazioniCliente(string codiceFiscale)
    {
        if (string.IsNullOrEmpty(codiceFiscale))
        {
            return View(new List<Prenotazione>());
        }

        var prenotazioni = await _prenotazioniService.GetPrenotazioniByClienteCodiceFiscaleAsync(codiceFiscale);
        ViewBag.CodiceFiscale = codiceFiscale; // Per visualizzare il codice fiscale nella vista
        return View(prenotazioni);
    }

    [Authorize(Roles = "Dipendente")]
    public async Task<IActionResult> RicercaPrenotazioniPensioneCompleta()
    {
        var numeroPrenotazioni = await _prenotazioniService.GetNumeroPrenotazioniPensioneCompletaAsync();
        ViewBag.NumeroPrenotazioni = numeroPrenotazioni;
        return View();
    }

    [Authorize(Roles = "Dipendente")]
    public async Task<IActionResult> ConteggioPrenotazioniPensioneCompleta()
    {
        var numeroPrenotazioni = await _prenotazioniService.GetNumeroPrenotazioniPensioneCompletaAsync();
        ViewBag.NumeroPrenotazioni = numeroPrenotazioni;
        return View();
    }
}
