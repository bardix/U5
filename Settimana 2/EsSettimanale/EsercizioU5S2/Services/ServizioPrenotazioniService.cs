using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace _1BW_BE.Service
{
    public class ServizioPrenotazioneService : IServizioPrenotazioneService
    {
        private readonly string _connectionString;

        public ServizioPrenotazioneService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<ServizioPrenotazione>> GetAllServiziPrenotazioniAsync()
        {
            var serviziPrenotazioni = new List<ServizioPrenotazione>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM ServiziPrenotazioni";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            serviziPrenotazioni.Add(new ServizioPrenotazione
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                IDPrenotazione = Convert.ToInt32(reader["IDPrenotazione"]),
                                IDServizio = Convert.ToInt32(reader["IDServizio"]),
                                Data = Convert.ToDateTime(reader["Data"]),
                                Quantità = Convert.ToInt32(reader["Quantità"]),
                                Prezzo = Convert.ToDecimal(reader["Prezzo"])
                            });
                        }
                    }
                }
            }
            return serviziPrenotazioni;
        }

        public async Task<ServizioPrenotazione> GetServizioPrenotazioneByIdAsync(int id)
        {
            ServizioPrenotazione servizioPrenotazione = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM ServiziPrenotazioni WHERE ID = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            servizioPrenotazione = new ServizioPrenotazione
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                IDPrenotazione = Convert.ToInt32(reader["IDPrenotazione"]),
                                IDServizio = Convert.ToInt32(reader["IDServizio"]),
                                Data = Convert.ToDateTime(reader["Data"]),
                                Quantità = Convert.ToInt32(reader["Quantità"]),
                                Prezzo = Convert.ToDecimal(reader["Prezzo"])
                            };
                        }
                    }
                }
            }
            return servizioPrenotazione;
        }

        public async Task AddServizioPrenotazioneAsync(ServizioPrenotazione servizioPrenotazione)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO ServiziPrenotazioni (IDPrenotazione, IDServizio, Data, Quantità, Prezzo)
                               VALUES (@IDPrenotazione, @IDServizio, @Data, @Quantità, @Prezzo)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IDPrenotazione", servizioPrenotazione.IDPrenotazione);
                    command.Parameters.AddWithValue("@IDServizio", servizioPrenotazione.IDServizio);
                    command.Parameters.AddWithValue("@Data", servizioPrenotazione.Data);
                    command.Parameters.AddWithValue("@Quantità", servizioPrenotazione.Quantità);
                    command.Parameters.AddWithValue("@Prezzo", servizioPrenotazione.Prezzo);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateServizioPrenotazioneAsync(ServizioPrenotazione servizioPrenotazione)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"UPDATE ServiziPrenotazioni
                               SET IDPrenotazione = @IDPrenotazione,
                                   IDServizio = @IDServizio,
                                   Data = @Data,
                                   Quantità = @Quantità,
                                   Prezzo = @Prezzo
                               WHERE ID = @Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", servizioPrenotazione.ID);
                    command.Parameters.AddWithValue("@IDPrenotazione", servizioPrenotazione.IDPrenotazione);
                    command.Parameters.AddWithValue("@IDServizio", servizioPrenotazione.IDServizio);
                    command.Parameters.AddWithValue("@Data", servizioPrenotazione.Data);
                    command.Parameters.AddWithValue("@Quantità", servizioPrenotazione.Quantità);
                    command.Parameters.AddWithValue("@Prezzo", servizioPrenotazione.Prezzo);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteServizioPrenotazioneAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM ServiziPrenotazioni WHERE ID = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<IEnumerable<ServizioPrenotazione>> GetServiziPrenotazioniByPrenotazioneIdAsync(int prenotazioneId)
        {
            var serviziPrenotazioni = new List<ServizioPrenotazione>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM ServiziPrenotazioni WHERE IDPrenotazione = @prenotazioneId";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@prenotazioneId", prenotazioneId);
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            serviziPrenotazioni.Add(new ServizioPrenotazione
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                IDPrenotazione = Convert.ToInt32(reader["IDPrenotazione"]),
                                IDServizio = Convert.ToInt32(reader["IDServizio"]),
                                Data = Convert.ToDateTime(reader["Data"]),
                                Quantità = Convert.ToInt32(reader["Quantità"]),
                                Prezzo = Convert.ToDecimal(reader["Prezzo"])
                            });
                        }
                    }
                }
            }
            return serviziPrenotazioni;
        }
    }
}