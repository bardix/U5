public class Prenotazione
{
    public int IDPrenotazione { get; set; }
    public string CodiceFiscaleCliente { get; set; }
    public int NumeroCamera { get; set; }
    public DateTime DataPrenotazione { get; set; }
    public int NumeroProgressivo { get; set; }
    public int Anno { get; set; }
    public DateTime DataInizioSoggiorno { get; set; }
    public DateTime DataFineSoggiorno { get; set; }
    public decimal Caparra { get; set; }
    public decimal Tariffa { get; set; }
    public string Dettagli { get; set; } // Proprietà per i dettagli
}
