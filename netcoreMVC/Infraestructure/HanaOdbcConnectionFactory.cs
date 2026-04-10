
using System.Data.Odbc;
using Microsoft.Extensions.Configuration;

namespace netcoreMVC.Infraestructure
{

    public class HanaOdbcConnectionFactory
    {

        private readonly string _connectionString;

        public HanaOdbcConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("HanaOdbc")
                ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'HanaOdbc'.");
        }

        public OdbcConnection CreateConnection()
        {
            return new OdbcConnection(_connectionString);
        }

    }

}
