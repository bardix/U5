namespace EsSettimanale.Models
{
    public class Spedizione
    {
        public int ID { get; set; }
        public int ClienteID { get; set; }
        public string NumeroIdentificativo { get; set; }
        public DateTime DataSpedizione { get; set; }
        public float Peso { get; set; }
        public string CittàDestinataria { get; set; }
        public string IndirizzoDestinatario { get; set; }
        public string NominativoDestinatario { get; set; }
        public decimal Costo { get; set; }
        public DateTime DataConsegnaPrevista { get; set; }
        public Cliente Cliente { get; set; }
    }
}
