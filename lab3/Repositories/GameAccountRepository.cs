using Classes.Accounts;
using Classes.Data;

namespace Classes.Repositories
{
  public interface IGameAccountRepository
  {
    GameAccountBase CreatePlayer(string userName, decimal currentRating);

    GameAccountBase PlayerById(decimal accountId);

    IEnumerable<GameAccountBase> ReadAllPlayers();
  }

  public class GameAccountRepository : IGameAccountRepository
  {
    private readonly DbContext dbContext;

    public GameAccountRepository(DbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    public GameAccountBase CreatePlayer(string userName, decimal currentRating)
    {
      var playerId = dbContext.GameAccounts.Count + 1;
      var player = new StandardGameAccount(playerId, userName, currentRating);
      dbContext.GameAccounts.Add(player);

      return player;
    }

    public GameAccountBase PlayerById(decimal accountId)
    {
      return dbContext.GameAccounts.FirstOrDefault(p => p.AccountId == accountId);
    }

    public IEnumerable<GameAccountBase> ReadAllPlayers()
    {
      return dbContext.GameAccounts;
    }
  }
}