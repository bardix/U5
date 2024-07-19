using EsVenerdi.Models;
using System.Data.SqlClient;

namespace EsVenerdi.Services
{
    public class AnagraficaService : SqlServerServiceBase
    {
        public AnagraficaService(IConfiguration configuration) : base(configuration) { }

        public List<Anagrafica> GetAll()
        {
            var list = new List<Anagrafica>();
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = GetCommand("SELECT * FROM ANAGRAFICA", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Anagrafica
                        {
                            IDAnagrafica = reader.GetInt32(0),
                            Cognome = reader.GetString(1),
                            Nome = reader.GetString(2),
                            Indirizzo = reader.GetString(3),
                            Città = reader.GetString(4),
                            CAP = reader.GetString(5),
                            Cod_Fisc = reader.GetString(6)
                        });
                    }
                }
            }
            return list;
        }

        public void Add(Anagrafica anagrafica)
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                var command = GetCommand("INSERT INTO ANAGRAFICA (Cognome, Nome, Indirizzo, Città, CAP, Cod_Fisc) VALUES (@Cognome, @Nome, @Indirizzo, @Città, @CAP, @Cod_Fisc)", connection);
                command.Parameters.AddWithValue("@Cognome", anagrafica.Cognome);
                command.Parameters.AddWithValue("@Nome", anagrafica.Nome);
                command.Parameters.AddWithValue("@Indirizzo", anagrafica.Indirizzo);
                command.Parameters.AddWithValue("@Città", anagrafica.Città);
                command.Parameters.AddWithValue("@CAP", anagrafica.CAP);
                command.Parameters.AddWithValue("@Cod_Fisc", anagrafica.Cod_Fisc);
                command.ExecuteNonQuery();
            }
        }
    }
}