using Classes.Accounts;
using Classes.Repositories;

namespace Classes.Services
{
  public interface IGameAccountService
  {
    GameAccountBase CreateAccount(string userName, decimal currentRating);

    GameAccountBase GetAccountById(decimal accountId);

    IEnumerable<GameAccountBase> GetAllAccounts();
  }

  public class GameAccountService : IGameAccountService
  {
    private readonly IGameAccountRepository gameAccountRepository;
    public GameAccountService(IGameAccountRepository gameAccountRepository)
    {
      this.gameAccountRepository = gameAccountRepository;
    }

    public GameAccountBase CreateAccount(string userName, decimal currentRating)
    {
      return gameAccountRepository.CreatePlayer(userName, currentRating);
    }

    public GameAccountBase GetAccountById(decimal accountId)
    {
      return gameAccountRepository.PlayerById(accountId);
    }

    public IEnumerable<GameAccountBase> GetAllAccounts()
    {
      return gameAccountRepository.ReadAllPlayers();
    }
  }
}