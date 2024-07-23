public class DettaglioPrenotazioneViewModel
{
    public Prenotazione Prenotazione { get; set; }
    public IEnumerable<ServizioPrenotazione> Servizi { get; set; }
    public decimal TotaleDaSaldare { get; set; }
}