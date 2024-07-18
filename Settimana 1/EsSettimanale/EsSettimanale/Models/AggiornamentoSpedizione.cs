namespace EsSettimanale.Models
{
    public class AggiornamentoSpedizione
    {
        public int ID { get; set; }
        public int SpedizioneID { get; set; }
        public string Stato { get; set; }
        public string Luogo { get; set; }
        public string Descrizione { get; set; }
        public DateTime DataOraAggiornamento { get; set; }
        public Spedizione Spedizione { get; set; }
    }
}
