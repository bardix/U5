using EsVenerdi.Models;
using System.Data.SqlClient;

namespace EsVenerdi.Services
{
    public class VerbaleService : SqlServerServiceBase
    {
        public VerbaleService(IConfiguration configuration) : base(configuration) { }

        public List<Verbale> GetAll()
        {
            var list = new List<Verbale>();
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = GetCommand("SELECT * FROM VERBALE", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Verbale
                        {
                            IDVerbale = reader.GetInt32(0),
                            IDAnagrafica = reader.GetInt32(1),
                            IDViolazione = reader.GetInt32(2),
                            DataViolazione = reader.GetDateTime(3),
                            IndirizzoViolazione = reader.GetString(4),
                            Nominativo_Agente = reader.GetString(5),
                            DataTrascrizioneVerbale = reader.GetDateTime(6),
                            Importo = reader.GetDecimal(7),
                            DecurtamentoPunti = reader.GetInt32(8)
                        });
                    }
                }
            }
            return list;
        }

        public void Add(Verbale verbale)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = GetCommand("INSERT INTO VERBALE (IDAnagrafica, IDViolazione, DataViolazione, IndirizzoViolazione, Nominativo_Agente, DataTrascrizioneVerbale, Importo, DecurtamentoPunti) VALUES (@IDAnagrafica, @IDViolazione, @DataViolazione, @IndirizzoViolazione, @Nominativo_Agente, @DataTrascrizioneVerbale, @Importo, @DecurtamentoPunti)", connection);
                command.Parameters.AddWithValue("@IDAnagrafica", verbale.IDAnagrafica);
                command.Parameters.AddWithValue("@IDViolazione", verbale.IDViolazione);
                command.Parameters.AddWithValue("@DataViolazione", verbale.DataViolazione);
                command.Parameters.AddWithValue("@IndirizzoViolazione", verbale.IndirizzoViolazione);
                command.Parameters.AddWithValue("@Nominativo_Agente", verbale.Nominativo_Agente);
                command.Parameters.AddWithValue("@DataTrascrizioneVerbale", verbale.DataTrascrizioneVerbale);
                command.Parameters.AddWithValue("@Importo", verbale.Importo);
                command.Parameters.AddWithValue("@DecurtamentoPunti", verbale.DecurtamentoPunti);
                command.ExecuteNonQuery();
            }
        }
    }
}