
using _1BW_BE.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

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

    public IActionResult Create()
    {
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
        return View(prenotazione);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Prenotazione prenotazione)
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

        return View(new DettaglioPrenotazioneViewModel
        {
            Prenotazione = prenotazione,
            Servizi = servizi,
            TotaleDaSaldare = totaleDaSaldare
        });
    }

    [Authorize(Roles = "Dipendente")]
    public async Task<IActionResult> RicercaPrenotazioniCliente(string codiceFiscale)
    {
        var prenotazioni = await _prenotazioniService.GetPrenotazioniByClienteCodiceFiscaleAsync(codiceFiscale);
        return View(prenotazioni);
    }

    [Authorize(Roles = "Dipendente")]
    public async Task<IActionResult> RicercaPrenotazioniPensioneCompleta()
    {
        var numeroPrenotazioni = await _prenotazioniService.GetNumeroPrenotazioniPensioneCompletaAsync();
        ViewBag.NumeroPrenotazioni = numeroPrenotazioni;
        return View();
    }
}
