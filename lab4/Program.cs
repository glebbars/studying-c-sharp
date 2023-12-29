using Classes.Constants;
using Classes.Data;
using Classes.Enums;
using Classes.Repositories;
using Classes.Services;

namespace Classes;

internal class Program
{
    private static void CreateGameAccount(GameAccountService gameAccountService)
    {
        Console.Write("\n1-2 Type the name the player: ");
        var name = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name)) throw new Exception(ExceptionMessages.TypedPlayerNameIsNull);

        Console.Write(
            "\n2-2 Choose type of game account\n1. Standard\n2. Win strike account (additional points are given for the strike of wins)\n3. Double points for loss account.\n(in case of choosing something else, Standard account will be automatically selected): ");
        var accountType = Console.ReadLine();

        var mappedReceivedAccountTypeToRealOne = accountType switch
        {
            "2" => GameAccountType.WinStreak,
            "3" => GameAccountType.DoublePointsForLoss,
            _ => GameAccountType.Standard
        };

        var gameAccount =
            gameAccountService.CreateAccount(mappedReceivedAccountTypeToRealOne, name, GameConstants.MinRating);
        Console.WriteLine(
            $"\n{Symbols.CheckMark}  The game account was successfully created\n{gameAccount.AccountId}: {gameAccount.UserName}, Rating: {gameAccount.CurrentRating} (min rating)\n");
    }

    private static void CreateGame(GameService gameService, GameAccountService gameAccountService)
    {
        while (true)
        {
            GameType? selectedGameType = null;


            var allGameAccounts = gameAccountService.GetAllAccounts();
            var allGameAccountsCount = allGameAccounts.Count();

            switch (allGameAccountsCount)
            {
                case 0:
                    Console.WriteLine(
                        "\nYou do not have any created accounts. Please create an account before creating a game");
                    CreateGameAccount(gameAccountService);
                    continue;
                case 1:
                    Console.WriteLine(
                        "\nYou have just one game account. Hence, only training type of game is available. Are you sure you want to play that y/n\n ");
                    if (Console.ReadLine() == "y") selectedGameType = GameType.Training;
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

                firstGameAccount.WinGame(gameService.CreateGame(GameType.Training,
                    GameConstants.TrainingGameOpponentName, GameResult.Win));

                Console.WriteLine($"\n{Symbols.CheckMark} The game has been played");
                Console.WriteLine(
                    $"\nGame History and Stats of first game accounts - {firstGameAccount.UserName} with {firstGameAccount.GetAccountType()}:");
                Console.WriteLine(gameAccountService.GetAccountById(firstGameAccount.AccountId).GetStats());
            }
            else if (selectedGameType != null)
            {
                Console.Write("\nThe list of created game accounts: \n");
                foreach (var gameAccount in allGameAccounts)
                    Console.WriteLine(
                        $"{gameAccount.AccountId}: {gameAccount.UserName}, Rating: {gameAccount.CurrentRating}");

                Console.WriteLine("\nPlease choose the WINNER from the list by account id");
                var winnerAccountId = Console.ReadLine();

                try
                {
                    var parsedWinnerAccountId = int.Parse(winnerAccountId ?? "1");
                    var winnerGameAccount = gameAccountService.GetAccountById(parsedWinnerAccountId);

                    Console.WriteLine("\nPlease choose the LOSER from the list by account id");
                    var loserAccountId = Console.ReadLine();

                    try
                    {
                        var parsedLoserAccountId = int.Parse(loserAccountId ?? "2");
                        var loserGameAccount = gameAccountService.GetAccountById(parsedLoserAccountId);


                        winnerGameAccount.WinGame(gameService.CreateGame(selectedGameType.Value,
                            winnerGameAccount.UserName,
                            GameResult.Win));
                        loserGameAccount.LoseGame(gameService.CreateGame(selectedGameType.Value,
                            loserGameAccount.UserName,
                            GameResult.Loss));

                        Console.WriteLine($"\n{Symbols.CheckMark} The game has been played");
                        Console.WriteLine(
                            $"\nGame History and Stats of WINNER game account - {winnerGameAccount.UserName} with {winnerGameAccount.GetAccountType()}:");
                        Console.WriteLine(gameAccountService.GetAccountById(winnerGameAccount.AccountId).GetStats());

                        Console.WriteLine(
                            $"\nGame History and Stats of LOSER game account - {loserGameAccount.UserName} with {loserGameAccount.GetAccountType()}:");
                        Console.WriteLine(gameAccountService.GetAccountById(loserGameAccount.AccountId).GetStats());
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


    private static void Main()
    {
        var dbContext = new DbContext();
        var gameAccountRepository = new GameAccountRepository(dbContext);
        var gameRepository = new GameRepository(dbContext);

        var gameService = new GameService(gameRepository);
        var gameAccountService = new GameAccountService(gameAccountRepository);

        Console.WriteLine("Welcome to the game!");

        while (true)
        {
            Console.WriteLine(
                "\n-----------------\n1. Create a game account\n2. Play a game\n4. Print the list of players\n5. Print the list of games\n3. Exit\n\nChoose an option: ");

            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreateGameAccount(gameAccountService);
                    break;
                case "2":
                    CreateGame(gameService, gameAccountService);
                    break;
                case "3":
                    Environment.Exit(0);
                    break;
                case "4":
                    var allGameAccounts = gameAccountService.GetAllAccounts();
                    if (!allGameAccounts.Any())
                    {
                        Console.WriteLine("\nYou have not created any games accounts yet");
                    }
                    else
                    {
                        Console.WriteLine("List of all game accounts:");
                        foreach (var gameAccount in gameAccountService.GetAllAccounts())
                            Console.WriteLine(
                                $"{gameAccount.AccountId}: {gameAccount.UserName}, Rating: {gameAccount.CurrentRating}");
                    }

                    break;
                case "5":
                    var allGames = gameService.GetAllGames();
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

                    break;
                default:
                    Console.WriteLine("\nWrong input. Try something else.");
                    break;
            }
        }
    }
}