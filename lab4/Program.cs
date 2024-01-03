using Classes.Constants;
using Classes.Data;
using Classes.Enums;
using Classes.Repositories;
using Classes.Services;
using Classes.UI;

namespace Classes
{
  class Program
  {

    private static void AddMocks(GameAccountService gameAccountService, GameService gameService)
    {
      var firstGameAccount = gameAccountService.CreateAccount(GameAccountType.Standard, "John Smiths", GameConstants.MinRating);
      var secondGameAccount = gameAccountService.CreateAccount(GameAccountType.WinStreak, "Mark Stone", GameConstants.MinRating);
      gameAccountService.CreateAccount(GameAccountType.DoublePointsForLoss, "Liam Beale", GameConstants.MinRating);
      gameAccountService.CreateAccount(GameAccountType.Standard, "Emma Parks", GameConstants.MinRating);

      // Game 1
      firstGameAccount.WinGame(gameService.CreateGame(GameType.Standard, secondGameAccount.UserName, GameResult.Win));
      secondGameAccount.LoseGame(gameService.CreateGame(GameType.Standard, firstGameAccount.UserName, GameResult.Loss));

      // Game 2
      secondGameAccount.WinGame(gameService.CreateGame(GameType.Training, GameConstants.TrainingGameOpponentName, GameResult.Win));
    }
    private static void Main()
    {
      var dbContext = new DbContext();
      var gameAccountRepository = new GameAccountRepository(dbContext);
      var gameRepository = new GameRepository(dbContext);

      var gameService = new GameService(gameRepository);
      var gameAccountService = new GameAccountService(gameAccountRepository);

      var GameUI = new GameUI(gameService, gameAccountService);
      var GameAccountsUI = new GameAccountsUI(gameAccountService);


      AddMocks(gameAccountService, gameService);


      Console.WriteLine("\n\nWelcome to the game UI!");

      while (true)
      {
        Console.WriteLine(
          "\n-----------------\n1. Create a game account\n2. Play a game\n3. Print the list of players\n4. Print the list of games\n5. Exit\n\nChoose an option: ");

        var choice = Console.ReadLine();

        switch (choice)
        {
          case "1":
            GameAccountsUI.CreateGameAccount();

            break;
          case "2":
            GameUI.CreateGame();

            break;
          case "3":
            GameAccountsUI.ShowAllGameAccounts();

            break;
          case "4":
            GameUI.ShowAllGames();

            break;
          case "5":
            Environment.Exit(0);

            break;
          default:
            Console.WriteLine("\nWrong input. Try something else.");

            break;
        }
      }
    }
  }
}