using Classes.Accounts;
using Classes.Games;

namespace Classes.Data
{
  public class DbContext
  {
    public List<GameAccountBase> GameAccounts { get; } = new List<GameAccountBase>();
    public List<GameBase> Games { get; } = new List<GameBase>();
    public GameFactory GameFactory { get; } = new GameFactory();
  }
}