using System.Data;

namespace Sakura.App
{
  public interface IConnectionFactory
  {
    IDbConnection GetConnection();
    void CloseConnection();
  }
}
