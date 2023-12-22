using Classes.Enums;
using Classes.Games;
using Classes.Repositories;

namespace Classes.Services
{
  public interface IGameService
  {
    GameBase CreateGame(GameType gameType, string opponentName, GameResult result);

    IEnumerable<GameBase> GetAllGames();

    IEnumerable<GameBase> GetGamesByPlayerId(decimal accountId);
  }

  public class GameService : IGameService
  {
    private readonly IGameRepository gameRepository;

    public GameService(IGameRepository gameRepository)
    {
      this.gameRepository = gameRepository;
    }

    public GameBase CreateGame(GameType gameType, string opponentName, GameResult result)
    {
      return gameRepository.CreateGame(gameType, opponentName, result);
    }

    public IEnumerable<GameBase> GetAllGames()
    {
      return gameRepository.ReadAllGames();
    }

    public IEnumerable<GameBase> GetGamesByPlayerId(decimal accountId)
    {
      return gameRepository.ReadGamesByPlayerId(accountId);
    }
  }
}