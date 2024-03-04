using AwesomeWeatherChallenge.Abstraction;
using Microsoft.Data.SqlClient;
using System.Data;

namespace AwesomeWeatherChallenge.Persistence;

public class DbConnetionFactory(string connectionString) : IDbConnetionFactory
{
    public IDbConnection CreateConnetion()
    {
        return new SqlConnection(connectionString);
    }
}
