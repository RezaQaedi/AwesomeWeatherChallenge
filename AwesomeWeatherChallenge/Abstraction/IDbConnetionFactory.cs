using System.Data;

namespace AwesomeWeatherChallenge.Abstraction;

public interface IDbConnetionFactory
{
    public IDbConnection CreateConnetion();
}
