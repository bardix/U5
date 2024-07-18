namespace EsSettimanale.Models
{
    public class Cliente
    {
        public int ID { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public string CodiceFiscale { get; set; }
        public string PartitaIVA { get; set; }
        public string Indirizzo { get; set; }
        public string Città { get; set; }
        public string CAP { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
    }
}
