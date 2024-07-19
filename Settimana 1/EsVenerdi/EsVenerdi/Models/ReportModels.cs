namespace EsVenerdi.Models
{
    public class AnagraficaReport
    {
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public int TotaleVerbali { get; set; }
        public int TotalePunti { get; set; }
    }

    public class VerbaleReport
    {
        public string Cognome { get; set; }
        public string Nome { get; set; }
        public DateTime DataViolazione { get; set; }
        public decimal Importo { get; set; }
        public int DecurtamentoPunti { get; set; }
    }
}