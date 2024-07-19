using System.Data.Common;
using System.Data.SqlClient;

namespace EsVenerdi.Services
{
    public class SqlServerServiceBase
    {
        private readonly string _connectionString;

        public SqlServerServiceBase(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        protected SqlCommand GetCommand(string commandText, SqlConnection connection)
        {
            return new SqlCommand(commandText, connection);
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}