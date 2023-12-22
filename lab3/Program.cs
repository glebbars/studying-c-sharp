using Classes.Constants;
using Classes.Data;
using Classes.Enums;
using Classes.GameServices;
using Classes.Repositories;
using Classes.Services;

namespace Classes
{
  class Program
  {
    private static void Main()
    {
      // var dbContext = new DbContext();
      // var gameAccountRepository = new GameAccountRepository(dbContext);
      // var gameRepository = new GameRepository(dbContext);
      //
      // var accountService = new GameAccountService(gameAccountRepository);
      // var gameService = new GameService(gameRepository);
      //
      // var firstPlayer = accountService.CreateAccount("John Smiths", GlobalConstants.MinRating);
      // var secondPlayer = accountService.CreateAccount("Mark Stone", GlobalConstants.MinRating);
      // var thirdPlayer = accountService.CreateAccount("Liam Beale", GlobalConstants.MinRating);
      // var forthPlayer = accountService.CreateAccount("Emma Parks", GlobalConstants.MinRating);
      //
      // gameService.CreateGame(GameType.Standard, secondPlayer.UserName, GameResult.Win);
      // gameService.CreateGame(GameType.Standard, firstPlayer.UserName, GameResult.Loss);
      // gameService.CreateGame(GameType.DoublePoints, thirdPlayer.UserName, GameResult.Win);
      // gameService.CreateGame(GameType.DoublePoints, forthPlayer.UserName, GameResult.Loss);
      // gameService.CreateGame(GameType.Training, firstPlayer.UserName, GameResult.Win);
      // gameService.CreateGame(GameType.Training, thirdPlayer.UserName, GameResult.Loss);
      // gameService.CreateGame(GameType.DoublePoints, secondPlayer.UserName, GameResult.Win);
      // gameService.CreateGame(GameType.DoublePoints, forthPlayer.UserName, GameResult.Loss);
      //
      // Console.WriteLine("List of all players:");
      // foreach (var player in accountService.GetAllAccounts())
      //   Console.WriteLine($"{player.AccountId}: {player.UserName}, Rating: {player.CurrentRating}");
      //
      // Console.WriteLine("\nList of all games:");
      // foreach (var game in gameService.GetAllGames())
      //   Console.WriteLine($"{game.OpponentName}, Result: {game.Result}, Rating: {game.CalculateRating()}");
      //
      // Console.WriteLine(
      //   $"\nGame History and Stats of first player - {firstPlayer.UserName} with {firstPlayer.GetAccountType()}:");
      // foreach (var game in gameService.GetGamesByPlayerId(firstPlayer.AccountId))
      //   Console.WriteLine($"{game.OpponentName}: {game.Result}, Rating: {game.CalculateRating()}");
      //
      // Console.WriteLine(firstPlayer.GetStats());

      var dbContext = new DbContext();
      var playerRepository = new GameAccountRepository(dbContext);
      var gameRepository = new GameRepository(dbContext);
      var gameService = new GameService2(playerRepository, gameRepository);

      var firstPlayer = playerRepository.CreatePlayer("John Smiths", GlobalConstants.MinRating);
      var secondPlayer = playerRepository.CreatePlayer("Mark Stone", GlobalConstants.MinRating);
      var thirdPlayer = playerRepository.CreatePlayer("Liam Beale", GlobalConstants.MinRating);
      var forthPlayer = playerRepository.CreatePlayer("Emma Parks", GlobalConstants.MinRating);

      // Play games using the GameService
      gameService.PlayGame(firstPlayer, secondPlayer, GameType.Standard, GameResult.Win);
      gameService.PlayGame(firstPlayer, secondPlayer, GameType.Standard, GameResult.Loss);
      gameService.PlayGame(thirdPlayer, forthPlayer, GameType.DoublePoints, GameResult.Win);
      gameService.PlayGame(thirdPlayer, forthPlayer, GameType.DoublePoints, GameResult.Loss);
      gameService.PlayGame(firstPlayer, thirdPlayer, GameType.Training, GameResult.Win);
      gameService.PlayGame(firstPlayer, thirdPlayer, GameType.Training, GameResult.Loss);
      gameService.PlayGame(secondPlayer, forthPlayer, GameType.DoublePoints, GameResult.Win);
      gameService.PlayGame(secondPlayer, forthPlayer, GameType.DoublePoints, GameResult.Loss);

      Console.WriteLine("List of all players:");
      foreach (var player in playerRepository.ReadAllPlayers())
        Console.WriteLine($"{player.AccountId}: {player.UserName}, Rating: {player.CurrentRating}");

      Console.WriteLine("\nList of all games:");
      foreach (var game in gameRepository.ReadAllGames())
        Console.WriteLine($"{game.OpponentName}, Result: {game.Result}, Rating: {game.CalculateRating()}");

      // Retrieve and display player stats using the GameService
      Console.WriteLine(
        $"\nGame History and Stats of first player - {firstPlayer.UserName} with {firstPlayer.GetAccountType()}:");
      Console.WriteLine(gameService.GetPlayerStats(firstPlayer));
    }
  }
}