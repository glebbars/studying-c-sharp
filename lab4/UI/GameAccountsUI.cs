using Classes.Constants;
using Classes.Enums;
using Classes.Services;

namespace Classes.UI
{

  public class GameAccountsUI
  {
    private readonly GameAccountService _gameAccountService;


    public GameAccountsUI(GameAccountService gameAccountService)
    {
      _gameAccountService = gameAccountService;
    }


    public void ShowAllGameAccounts()
    {
      var allGameAccounts = _gameAccountService.GetAllAccounts();
      if (!allGameAccounts.Any())
      {
        Console.WriteLine("\nYou have not created any games accounts yet");
      }
      else
      {
        Console.WriteLine("List of all game accounts:");
        foreach (var gameAccount in _gameAccountService.GetAllAccounts())
          Console.WriteLine(
            $"{gameAccount.AccountId}: {gameAccount.UserName}, Rating: {gameAccount.CurrentRating}");
      }
    }

    public void CreateGameAccount()
    {
      Console.Write("\n1-2 Type the name the player: ");
      var name = Console.ReadLine();

      if (string.IsNullOrWhiteSpace(name))
      {
        throw new Exception(ExceptionMessages.TypedPlayerNameIsNull);
      }

      Console.Write(
        "\n2-2 Choose type of game account\n1. Standard\n2. Win strike account (additional points are given for the strike of wins)\n3. Double points for loss account.\n(in case of choosing something else, Standard account will be automatically selected): ");
      var accountType = Console.ReadLine();

      var mappedReceivedAccountTypeToRealOne = accountType switch
      {
        "2" => GameAccountType.WinStreak,
        "3" => GameAccountType.DoublePointsForLoss,
        _ => GameAccountType.Standard
      };

      _gameAccountService.CreateAccount(mappedReceivedAccountTypeToRealOne, name, GameConstants.MinRating);
    }
  }
}