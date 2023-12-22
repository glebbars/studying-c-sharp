using Classes.Accounts;
using Classes.Enums;
using Classes.Repositories;

namespace Classes.GameServices
{
  public interface IGameService2
  {
    void PlayGame(GameAccountBase player1, GameAccountBase player2, GameType gameType, GameResult result);

    string GetPlayerStats(GameAccountBase player);
  }

  public class GameService2 : IGameService2
  {
    private readonly IGameAccountRepository playerRepository;
    private readonly IGameRepository gameRepository;

    public GameService2(IGameAccountRepository playerRepository, IGameRepository gameRepository)
    {
      this.playerRepository = playerRepository;
      this.gameRepository = gameRepository;
    }

    public void PlayGame(GameAccountBase player1, GameAccountBase player2, GameType gameType, GameResult result)
    {
      if (result == GameResult.Win)
      {
        player1.WinGame(gameRepository.CreateGame(gameType, player2.UserName, GameResult.Win));
        player2.LoseGame(gameRepository.CreateGame(gameType, player1.UserName, GameResult.Loss));
      }
      else
      {
        player1.LoseGame(gameRepository.CreateGame(gameType, player2.UserName, GameResult.Loss));
        player2.WinGame(gameRepository.CreateGame(gameType, player1.UserName, GameResult.Win));
      }
    }

    public string GetPlayerStats(GameAccountBase player)
    {
      return player.GetStats();
    }
  }
}