using Classes.Constants;
using Classes.Enums;
using Classes.Services;

namespace Classes.UI
{
  public class GameUI
  {
    private readonly GameAccountService _gameAccountService;
    private readonly GameService _gameService;


    public GameUI(GameService gameService, GameAccountService gameAccountService)
    {
      _gameService = gameService;
      _gameAccountService = gameAccountService;
    }


    public void ShowAllGames()
    {
      var allGames = _gameService.GetAllGames();

      if (!allGames.Any())
      {
        Console.WriteLine("\nYou have not created any games yet");
      }
      else
      {
        Console.WriteLine("\nList of all games:");
        foreach (var game in allGames)
          Console.WriteLine(
            $"{game.OpponentName}, Result: {game.Result}, Rating: {game.CalculateRating()}");
      }
    }

    public void CreateGame()
    {
      while (true)
      {
        GameType? selectedGameType = null;


        var allGameAccounts = _gameAccountService.GetAllAccounts();
        var allGameAccountsCount = allGameAccounts.Count();

        switch (allGameAccountsCount)
        {
          case 0:
            Console.WriteLine(
              "\nYou do not have any created accounts. Please create an account before creating a game");

            break;
          case 1:
            Console.WriteLine(
              "\nYou have just one game account. Hence, only training type of game is available. Are you sure you want to play that y/n\n ");
            if (Console.ReadLine() == "y")
            {
              selectedGameType = GameType.Training;
            }

            break;
          default:
            Console.WriteLine(
              "\nChoose what type of game you want: \n1. Standard\n2. Double points\n3. Training game\nIf something else is typed, Standard (1) game will be auto selected");
            var gameType = Console.ReadLine();
            selectedGameType = gameType switch
            {
              "2" => GameType.DoublePoints,
              "3" => GameType.Training,
              _ => GameType.Standard
            };

            break;
        }

        if (selectedGameType == GameType.Training)
        {
          var firstGameAccount = allGameAccounts.First();

          firstGameAccount.WinGame(_gameService.CreateGame(GameType.Training,
            GameConstants.TrainingGameOpponentName, GameResult.Win));

          Console.WriteLine(
            $"\nGame History and Stats of first game accounts - {firstGameAccount.UserName} with {firstGameAccount.GetAccountType()}:");
          Console.WriteLine(_gameAccountService.GetAccountById(firstGameAccount.AccountId).GetStats());
        }
        else if (selectedGameType != null)
        {
          Console.Write("\nThe list of created game accounts: \n");
          foreach (var gameAccount in allGameAccounts)
            Console.WriteLine(
              $"{gameAccount.AccountId}: {gameAccount.UserName} with {gameAccount.GetAccountType()} account type, Rating: {gameAccount.CurrentRating}");

          Console.WriteLine("\nPlease choose the WINNER from the list by account id");
          var winnerAccountId = Console.ReadLine();

          try
          {
            var parsedWinnerAccountId = int.Parse(winnerAccountId ?? "1");
            var winnerGameAccount = _gameAccountService.GetAccountById(parsedWinnerAccountId);

            Console.WriteLine("\nPlease choose the LOSER from the list by account id");
            var loserAccountId = Console.ReadLine();

            try
            {
              var parsedLoserAccountId = int.Parse(loserAccountId ?? "2");
              var loserGameAccount = _gameAccountService.GetAccountById(parsedLoserAccountId);

              if (parsedWinnerAccountId == parsedLoserAccountId)
              {
                Console.WriteLine("Sorry but winner and Loser cannot be the same account");

                return;
              }


              winnerGameAccount.WinGame(_gameService.CreateGame(selectedGameType.Value,
                winnerGameAccount.UserName,
                GameResult.Win));
              loserGameAccount.LoseGame(_gameService.CreateGame(selectedGameType.Value,
                loserGameAccount.UserName,
                GameResult.Loss));

              Console.WriteLine(
                $"\nGame History and Stats of WINNER game account - {winnerGameAccount.UserName} with {winnerGameAccount.GetAccountType()}:");
              Console.WriteLine(_gameAccountService.GetAccountById(winnerGameAccount.AccountId).GetStats());

              Console.WriteLine(
                $"\nGame History and Stats of LOSER game account - {loserGameAccount.UserName} with {loserGameAccount.GetAccountType()}:");
              Console.WriteLine(_gameAccountService.GetAccountById(loserGameAccount.AccountId).GetStats());
            }
            catch (FormatException)
            {
              Console.WriteLine("\nYou have entered invalid number");
            }
          }
          catch (FormatException)
          {
            Console.WriteLine("\nYou have entered invalid number");
          }
        }

        break;
      }
    }
  }
}