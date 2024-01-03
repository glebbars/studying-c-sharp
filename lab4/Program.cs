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

      Dictionary<int, (string commandInfo, Action command)> uiCommands = new Dictionary<int, (string, Action)>
      {
        { 1, ("Create a game account", GameAccountsUI.CreateGameAccount) },
        { 2, ("Play a game", GameUI.CreateGame) },
        { 3, ("Print the list of players", GameAccountsUI.ShowAllGameAccounts) },
        { 4, ("Print the list of games", GameUI.ShowAllGames) },
        { 5, ("Exit", () => Environment.Exit(0)) }
      };

      while (true)
      {
        Console.WriteLine(
          "\n-----------------\n1. Create a game account\n2. Play a game\n3. Print the list of players\n4. Print the list of games\n5. Exit\n\nChoose an option: ");

        var choice = Console.ReadLine();

        if (int.TryParse(choice, out var option) && uiCommands.ContainsKey(option))
        {
          uiCommands[option].command();
        }
        else
        {
          Console.WriteLine("\nWrong input. Try something else.");
        }
      }
    }
  }
}