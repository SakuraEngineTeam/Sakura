using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Sakura.App;

namespace Sakura.Api
{
  public class ConnectionFactory : IConnectionFactory
  {
    protected readonly IConfiguration Configuration;

    protected IDbConnection Connection;

    public ConnectionFactory(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IDbConnection GetConnection()
    {
      if (Connection == null) {
        string connectionString = Configuration.GetConnectionString("DefaultConnection");
        Connection = new NpgsqlConnection(connectionString);
      }

      if (Connection.State != ConnectionState.Open) {
        Connection.Open();
      }

      return Connection;
    }

    public void CloseConnection()
    {
      if (Connection != null && Connection.State == ConnectionState.Open) {
        Connection.Close();
      }
    }
  }
}
