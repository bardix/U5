using EsVenerdi.Models;
using System.Data.SqlClient;

namespace EsVenerdi.Services
{
    public class TipoViolazioneService : SqlServerServiceBase
    {
        public TipoViolazioneService(IConfiguration configuration) : base(configuration) { }

        public List<TipoViolazione> GetAll()
        {
            var list = new List<TipoViolazione>();
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = GetCommand("SELECT * FROM TIPO_VIOLAZIONE", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new TipoViolazione
                        {
                            IDViolazione = reader.GetInt32(0),
                            Descrizione = reader.GetString(1)
                        });
                    }
                }
            }
            return list;
        }

        public void Add(TipoViolazione tipoViolazione)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = GetCommand("INSERT INTO TIPO_VIOLAZIONE (Descrizione) VALUES (@Descrizione)", connection);
                command.Parameters.AddWithValue("@Descrizione", tipoViolazione.Descrizione);
                command.ExecuteNonQuery();
            }
        }
    }
}