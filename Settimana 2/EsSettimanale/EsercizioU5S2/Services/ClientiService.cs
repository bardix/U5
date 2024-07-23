using System.Data.SqlClient;

public class ClientiService : IClientiService
{
    private readonly string _connectionString;

    public ClientiService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public async Task<IEnumerable<Cliente>> GetAllClientiAsync()
    {
        var clienti = new List<Cliente>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Clienti";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        clienti.Add(new Cliente
                        {
                            CodiceFiscale = reader["CodiceFiscale"].ToString(),
                            Cognome = reader["Cognome"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            Città = reader["Città"].ToString(),
                            Provincia = reader["Provincia"].ToString(),
                            Email = reader["Email"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Cellulare = reader["Cellulare"].ToString()
                        });
                    }
                }
            }
        }
        return clienti;
    }

    public async Task<Cliente> GetClienteByIdAsync(string id)
    {
        Cliente cliente = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT * FROM Clienti WHERE CodiceFiscale = @id";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                await connection.OpenAsync();
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        cliente = new Cliente
                        {
                            CodiceFiscale = reader["CodiceFiscale"].ToString(),
                            Cognome = reader["Cognome"].ToString(),
                            Nome = reader["Nome"].ToString(),
                            Città = reader["Città"].ToString(),
                            Provincia = reader["Provincia"].ToString(),
                            Email = reader["Email"].ToString(),
                            Telefono = reader["Telefono"].ToString(),
                            Cellulare = reader["Cellulare"].ToString()
                        };
                    }
                }
            }
        }
        return cliente;
    }

    public async Task AddClienteAsync(Cliente cliente)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "INSERT INTO Clienti (CodiceFiscale, Cognome, Nome, Città, Provincia, Email, Telefono, Cellulare) VALUES (@CodiceFiscale, @Cognome, @Nome, @Città, @Provincia, @Email, @Telefono, @Cellulare)";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@CodiceFiscale", cliente.CodiceFiscale);
                command.Parameters.AddWithValue("@Cognome", cliente.Cognome);
                command.Parameters.AddWithValue("@Nome", cliente.Nome);
                command.Parameters.AddWithValue("@Città", cliente.Città);
                command.Parameters.AddWithValue("@Provincia", cliente.Provincia);
                command.Parameters.AddWithValue("@Email", cliente.Email);
                command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                command.Parameters.AddWithValue("@Cellulare", cliente.Cellulare);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task UpdateClienteAsync(Cliente cliente)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "UPDATE Clienti SET Cognome = @Cognome, Nome = @Nome, Città = @Città, Provincia = @Provincia, Email = @Email, Telefono = @Telefono, Cellulare = @Cellulare WHERE CodiceFiscale = @CodiceFiscale";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@CodiceFiscale", cliente.CodiceFiscale);
                command.Parameters.AddWithValue("@Cognome", cliente.Cognome);
                command.Parameters.AddWithValue("@Nome", cliente.Nome);
                command.Parameters.AddWithValue("@Città", cliente.Città);
                command.Parameters.AddWithValue("@Provincia", cliente.Provincia);
                command.Parameters.AddWithValue("@Email", cliente.Email);
                command.Parameters.AddWithValue("@Telefono", cliente.Telefono);
                command.Parameters.AddWithValue("@Cellulare", cliente.Cellulare);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteClienteAsync(string id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "DELETE FROM Clienti WHERE CodiceFiscale = @id";
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task<bool> ClienteExistsAsync(string id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string sql = "SELECT COUNT(*) FROM Clienti WHERE CodiceFiscale = @id";
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
