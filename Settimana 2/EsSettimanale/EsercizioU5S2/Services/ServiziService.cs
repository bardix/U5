using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace _1BW_BE.Service
{
    public class ServiziService : IServiziService
    {
        private readonly string _connectionString;

        public ServiziService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Servizio>> GetAllServiziAsync()
        {
            var servizi = new List<Servizio>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Servizi";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            servizi.Add(new Servizio
                            {
                                IDServizio = Convert.ToInt32(reader["IDServizio"]),
                                Nome = reader["Nome"].ToString(),
                                Prezzo = Convert.ToDecimal(reader["Prezzo"])
                            });
                        }
                    }
                }
            }
            return servizi;
        }

        public async Task<Servizio> GetServizioByIdAsync(int id)
        {
            Servizio servizio = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Servizi WHERE IDServizio = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            servizio = new Servizio
                            {
                                IDServizio = Convert.ToInt32(reader["IDServizio"]),
                                Nome = reader["Nome"].ToString(),
                                Prezzo = Convert.ToDecimal(reader["Prezzo"])
                            };
                        }
                    }
                }
            }
            return servizio;
        }

        public async Task AddServizioAsync(Servizio servizio)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO Servizi (Nome, Prezzo)
                               VALUES (@Nome, @Prezzo)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Nome", servizio.Nome);
                    command.Parameters.AddWithValue("@Prezzo", servizio.Prezzo);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateServizioAsync(Servizio servizio)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = @"UPDATE Servizi
                               SET Nome = @Nome,
                                   Prezzo = @Prezzo
                               WHERE IDServizio = @Id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", servizio.IDServizio);
                    command.Parameters.AddWithValue("@Nome", servizio.Nome);
                    command.Parameters.AddWithValue("@Prezzo", servizio.Prezzo);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteServizioAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Servizi WHERE IDServizio = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
