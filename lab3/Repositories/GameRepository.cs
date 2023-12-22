using Classes.Accounts;
using Classes.Constants;
using Classes.Data;
using Classes.Enums;
using Classes.Games;

namespace Classes.Repositories
{
  public interface IGameRepository
  {
    GameBase CreateGame(GameType gameType, string opponentName, GameResult result);

    IEnumerable<GameBase> ReadAllGames();

    IEnumerable<GameBase> ReadGamesByPlayerId(decimal accountId);
  }

  public class GameRepository : IGameRepository
  {
    private readonly DbContext dbContext;

    public GameRepository(DbContext dbContext)
    {
      this.dbContext = dbContext;
    }

    public GameBase CreateGame(GameType gameType, string opponentName, GameResult result)
    {
      var playerId = dbContext.GameAccounts.Count + 1;
      var player = new StandardGameAccount(playerId, opponentName, GlobalConstants.MinRating);
      // var player = new StandardGameAccount(opponentName, Constants.MinRating);
      dbContext.GameAccounts.Add(player);

      var game = dbContext.GameFactory.CreateGame(gameType, opponentName, result);
      dbContext.Games.Add(game);

      return game;
    }

    public IEnumerable<GameBase> ReadAllGames()
    {
      return dbContext.Games;
    }

    public IEnumerable<GameBase> ReadGamesByPlayerId(decimal accountId)
    {
      return dbContext.Games.Where(g =>
        g.OpponentName == dbContext.GameAccounts.FirstOrDefault(p => p.AccountId == accountId)?.UserName);
    }
  }
}