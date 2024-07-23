using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace _1BW_BE.Service
{
    public class PrenotazioniService : IPrenotazioniService
    {
        private readonly string _connectionString;

        public PrenotazioniService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Prenotazione>> GetAllPrenotazioniAsync()
        {
            var prenotazioni = new List<Prenotazione>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Prenotazioni";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            prenotazioni.Add(new Prenotazione
                            {
                                IDPrenotazione = Convert.ToInt32(reader["IDPrenotazione"]),
                                CodiceFiscaleCliente = reader["CodiceFiscaleCliente"].ToString(),
                                NumeroCamera = Convert.ToInt32(reader["NumeroCamera"]),
                                DataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]),
                                NumeroProgressivo = Convert.ToInt32(reader["NumeroProgressivo"]),
                                Anno = Convert.ToInt32(reader["Anno"]),
                                DataInizioSoggiorno = Convert.ToDateTime(reader["DataInizioSoggiorno"]),
                                DataFineSoggiorno = Convert.ToDateTime(reader["DataFineSoggiorno"]),
                                Caparra = Convert.ToDecimal(reader["Caparra"]),
                                Tariffa = Convert.ToDecimal(reader["Tariffa"]),
                                Dettagli = reader["Dettagli"].ToString()
                            });
                        }
                    }
                }
            }
            return prenotazioni;
        }

        public async Task<Prenotazione> GetPrenotazioneByIdAsync(int id)
        {
            Prenotazione prenotazione = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Prenotazioni WHERE IDPrenotazione = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            prenotazione = new Prenotazione
                            {
                                IDPrenotazione = Convert.ToInt32(reader["IDPrenotazione"]),
                                CodiceFiscaleCliente = reader["CodiceFiscaleCliente"].ToString(),
                                NumeroCamera = Convert.ToInt32(reader["NumeroCamera"]),
                                DataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]),
                                NumeroProgressivo = Convert.ToInt32(reader["NumeroProgressivo"]),
                                Anno = Convert.ToInt32(reader["Anno"]),
                                DataInizioSoggiorno = Convert.ToDateTime(reader["DataInizioSoggiorno"]),
                                DataFineSoggiorno = Convert.ToDateTime(reader["DataFineSoggiorno"]),
                                Caparra = Convert.ToDecimal(reader["Caparra"]),
                                Tariffa = Convert.ToDecimal(reader["Tariffa"]),
                                Dettagli = reader["Dettagli"].ToString()
                            };
                        }
                    }
                }
            }
            return prenotazione;
        }

        public async Task AddPrenotazioneAsync(Prenotazione prenotazione)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO Prenotazioni (CodiceFiscaleCliente, NumeroCamera, DataPrenotazione, NumeroProgressivo, Anno, DataInizioSoggiorno, DataFineSoggiorno, Caparra, Tariffa, Dettagli)
                               VALUES (@CodiceFiscaleCliente, @NumeroCamera, @DataPrenotazione, @NumeroProgressivo, @Anno, @DataInizioSoggiorno, @DataFineSoggiorno, @Caparra, @Tariffa, @Dettagli)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CodiceFiscaleCliente", prenotazione.CodiceFiscaleCliente);
                    command.Parameters.AddWithValue("@NumeroCamera", prenotazione.NumeroCamera);
                    command.Parameters.AddWithValue("@DataPrenotazione", prenotazione.DataPrenotazione);
                    command.Parameters.AddWithValue("@NumeroProgressivo", prenotazione.NumeroProgressivo);
                    command.Parameters.AddWithValue("@Anno", prenotazione.Anno);
                    command.Parameters.AddWithValue("@DataInizioSoggiorno", prenotazione.DataInizioSoggiorno);
                    command.Parameters.AddWithValue("@DataFineSoggiorno", prenotazione.DataFineSoggiorno);
                    command.Parameters.AddWithValue("@Caparra", prenotazione.Caparra);
                    command.Parameters.AddWithValue("@Tariffa", prenotazione.Tariffa);
                    command.Parameters.AddWithValue("@Dettagli", prenotazione.Dettagli);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdatePrenotazioneAsync(Prenotazione prenotazione)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"UPDATE Prenotazioni
                               SET CodiceFiscaleCliente = @CodiceFiscaleCliente,
                                   NumeroCamera = @NumeroCamera,
                                   DataPrenotazione = @DataPrenotazione,
                                   NumeroProgressivo = @NumeroProgressivo,
                                   Anno = @Anno,
                                   DataInizioSoggiorno = @DataInizioSoggiorno,
                                   DataFineSoggiorno = @DataFineSoggiorno,
                                   Caparra = @Caparra,
                                   Tariffa = @Tariffa,
                                   Dettagli = @Dettagli
                               WHERE IDPrenotazione = @IDPrenotazione";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@IDPrenotazione", prenotazione.IDPrenotazione);
                    command.Parameters.AddWithValue("@CodiceFiscaleCliente", prenotazione.CodiceFiscaleCliente);
                    command.Parameters.AddWithValue("@NumeroCamera", prenotazione.NumeroCamera);
                    command.Parameters.AddWithValue("@DataPrenotazione", prenotazione.DataPrenotazione);
                    command.Parameters.AddWithValue("@NumeroProgressivo", prenotazione.NumeroProgressivo);
                    command.Parameters.AddWithValue("@Anno", prenotazione.Anno);
                    command.Parameters.AddWithValue("@DataInizioSoggiorno", prenotazione.DataInizioSoggiorno);
                    command.Parameters.AddWithValue("@DataFineSoggiorno", prenotazione.DataFineSoggiorno);
                    command.Parameters.AddWithValue("@Caparra", prenotazione.Caparra);
                    command.Parameters.AddWithValue("@Tariffa", prenotazione.Tariffa);
                    command.Parameters.AddWithValue("@Dettagli", prenotazione.Dettagli);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeletePrenotazioneAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Prenotazioni WHERE IDPrenotazione = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> PrenotazioneExistsAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT COUNT(*) FROM Prenotazioni WHERE IDPrenotazione = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    await connection.OpenAsync();
                    int count = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return count > 0;
                }
            }
        }

        public async Task<IEnumerable<Prenotazione>> GetPrenotazioniByClienteCodiceFiscaleAsync(string codiceFiscale)
        {
            var prenotazioni = new List<Prenotazione>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Prenotazioni WHERE CodiceFiscaleCliente = @codiceFiscale";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@codiceFiscale", codiceFiscale);
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            prenotazioni.Add(new Prenotazione
                            {
                                IDPrenotazione = Convert.ToInt32(reader["IDPrenotazione"]),
                                CodiceFiscaleCliente = reader["CodiceFiscaleCliente"].ToString(),
                                NumeroCamera = Convert.ToInt32(reader["NumeroCamera"]),
                                DataPrenotazione = Convert.ToDateTime(reader["DataPrenotazione"]),
                                NumeroProgressivo = Convert.ToInt32(reader["NumeroProgressivo"]),
                                Anno = Convert.ToInt32(reader["Anno"]),
                                DataInizioSoggiorno = Convert.ToDateTime(reader["DataInizioSoggiorno"]),
                                DataFineSoggiorno = Convert.ToDateTime(reader["DataFineSoggiorno"]),
                                Caparra = Convert.ToDecimal(reader["Caparra"]),
                                Tariffa = Convert.ToDecimal(reader["Tariffa"]),
                                Dettagli = reader["Dettagli"].ToString()
                            });
                        }
                    }
                }
            }
            return prenotazioni;
        }

        public async Task<int> GetNumeroPrenotazioniPensioneCompletaAsync()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT COUNT(*) FROM Prenotazioni WHERE Dettagli LIKE '%pensione completa%'";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    await connection.OpenAsync();
                    return Convert.ToInt32(await command.ExecuteScalarAsync());
                }
            }
        }
    }
}
