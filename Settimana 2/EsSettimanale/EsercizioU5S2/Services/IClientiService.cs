public interface IClientiService
{
    Task<IEnumerable<Cliente>> GetAllClientiAsync();
    Task<Cliente> GetClienteByIdAsync(string id);
    Task AddClienteAsync(Cliente cliente);
    Task UpdateClienteAsync(Cliente cliente);
    Task DeleteClienteAsync(string id);
    Task<bool> ClienteExistsAsync(string id);
}
