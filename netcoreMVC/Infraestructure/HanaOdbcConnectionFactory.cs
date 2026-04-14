
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Odbc;

namespace netcoreMVC.Infraestructure
{

    public class HanaOdbcConnectionFactory
    {

        private readonly string _connectionString;

        public HanaOdbcConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("HanaOdbc")
                ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'HanaOdbc'");
        }       

        public OdbcConnection CreateConnection()
        {
            return new OdbcConnection(_connectionString);
        }

        public DataTable ConnectionDataAdapter()
        {

            DataTable dt = new DataTable();

            //var connection = CreateConnection();
            //await connection.OpenAsync();            

            //var query = _configuration["Queries:GetSRGC"];

            //using var command = new OdbcCommand("SELECT * FROM SMX_SRGC", connection);
            //using var reader = await command.ExecuteReaderAsync();

            //var dataTable = new DataTable();
            //dataTable.Load(reader);

            return dt;

        }

    }

}
