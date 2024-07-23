using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace _1BW_BE.Service
{
    public class CamereService : ICamereService
    {
        private readonly string _connectionString;

        public CamereService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Camera>> GetAllCamereAsync()
        {
            var camere = new List<Camera>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Camere";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            camere.Add(new Camera
                            {
                                Numero = Convert.ToInt32(reader["Numero"]),
                                Descrizione = reader["Descrizione"].ToString(),
                                Tipologia = reader["Tipologia"].ToString()
                            });
                        }
                    }
                }
            }
            return camere;
        }

        public async Task<Camera> GetCameraByIdAsync(int id)
        {
            Camera camera = null;
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Camere WHERE Numero = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            camera = new Camera
                            {
                                Numero = Convert.ToInt32(reader["Numero"]),
                                Descrizione = reader["Descrizione"].ToString(),
                                Tipologia = reader["Tipologia"].ToString()
                            };
                        }
                    }
                }
            }
            return camera;
        }

        public async Task AddCameraAsync(Camera camera)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "INSERT INTO Camere (Numero, Descrizione, Tipologia) VALUES (@Numero, @Descrizione, @Tipologia)";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Numero", camera.Numero);
                    command.Parameters.AddWithValue("@Descrizione", camera.Descrizione);
                    command.Parameters.AddWithValue("@Tipologia", camera.Tipologia);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateCameraAsync(Camera camera)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE Camere SET Descrizione = @Descrizione, Tipologia = @Tipologia WHERE Numero = @Numero";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Numero", camera.Numero);
                    command.Parameters.AddWithValue("@Descrizione", camera.Descrizione);
                    command.Parameters.AddWithValue("@Tipologia", camera.Tipologia);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteCameraAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Camere WHERE Numero = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<bool> CameraExistsAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                string sql = "SELECT COUNT(*) FROM Camere WHERE Numero = @id";
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    await connection.OpenAsync();
                    int count = Convert.ToInt32(await command.ExecuteScalarAsync());
                    return count > 0;
                }
            }
        }
    }
}
