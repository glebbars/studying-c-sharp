using Classes.Constants;
using Classes.Data;
using Classes.Enums;
using Classes.Repositories;
using Classes.Services;

namespace Classes
{
  class Program
  {
    private static void Main()
    {
      var dbContext = new DbContext();
      var gameAccountRepository = new GameAccountRepository(dbContext);
      var gameRepository = new GameRepository(dbContext);

      var gameService = new GameService(gameRepository);
      var gameAccountService = new GameAccountService(gameAccountRepository);

      var firstGameAccount = gameAccountService.CreateAccount(GameAccountType.Standard, "John Smiths", GameConstants.MinRating);
      var secondGameAccount = gameAccountService.CreateAccount(GameAccountType.WinStreak, "Mark Stone", GameConstants.MinRating);
      var thirdGameAccount =
        gameAccountService.CreateAccount(GameAccountType.DoublePointsForLoss, "Liam Beale", GameConstants.MinRating);
      var forthGameAccount = gameAccountService.CreateAccount(GameAccountType.Standard, "Emma Parks", GameConstants.MinRating);

      // Game 1
      firstGameAccount.WinGame(gameService.CreateGame(GameType.Standard, secondGameAccount.UserName, GameResult.Win));
      secondGameAccount.LoseGame(gameService.CreateGame(GameType.Standard, firstGameAccount.UserName, GameResult.Loss));

      // Game 2
      secondGameAccount.WinGame(gameService.CreateGame(GameType.Training, GameConstants.TrainingGameOpponentName, GameResult.Win));

      // Game 3
      forthGameAccount.WinGame(gameService.CreateGame(GameType.DoublePoints, thirdGameAccount.UserName, GameResult.Win));
      thirdGameAccount.LoseGame(gameService.CreateGame(GameType.DoublePoints, forthGameAccount.UserName, GameResult.Loss));

      // Game 4
      secondGameAccount.WinGame(gameService.CreateGame(GameType.Standard, forthGameAccount.UserName, GameResult.Win));
      forthGameAccount.LoseGame(gameService.CreateGame(GameType.Standard, secondGameAccount.UserName, GameResult.Loss));


      Console.WriteLine("List of all game accounts:");
      foreach (var gameAccount in gameAccountService.GetAllAccounts())
        Console.WriteLine($"{gameAccount.AccountId}: {gameAccount.UserName}, Rating: {gameAccount.CurrentRating}");

      Console.WriteLine("\nList of all games:");
      foreach (var game in gameService.GetAllGames())
        Console.WriteLine($"{game.OpponentName}, Result: {game.Result}, Rating: {game.CalculateRating()}");

      // Retrieve and display gameAccount stats using the GameService
      Console.WriteLine(
        $"\nGame History and Stats of first game accounts - {firstGameAccount.UserName} with {firstGameAccount.GetAccountType()}:");
      Console.WriteLine(gameAccountService.GetAccountById(firstGameAccount.AccountId).GetStats());

      Console.WriteLine(
        $"\nGame History and Stats of second game accounts - {secondGameAccount.UserName} with {firstGameAccount.GetAccountType()}:");
      Console.WriteLine(gameAccountService.GetAccountById(secondGameAccount.AccountId).GetStats());
    }
  }
}