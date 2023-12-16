namespace Classes1
{

  //   public enum GameResult
  // {
  //   Win,
  //   Loss
  // }

  // public static class Constants
  // {
  //   public const int MinRating = 1;
  //   public const string NegativeRatingExceptionMessage = "Рейтинг не може бути негативним";
  // }

  // public class Game
  // {
  //   public string OpponentName { get; }
  //   public decimal Rating { get; }
  //   public GameResult Result { get; }

  //   public Game(string opponentName, decimal rating, GameResult result)
  //   {
  //     OpponentName = opponentName;
  //     Rating = rating;
  //     Result = result;
  //   }
  // }

  // public abstract class GameAccountBase
  // {
  //   public string UserName { get; }
  //   public decimal CurrentRating { get; set; }

  //   public decimal GamesCount { get; }

  //   public List<Game> Games = new List<Game>();

  //   protected GameAccountBase(string userName, decimal currentRating, decimal gamesCount)
  //   {
  //     UserName = userName;
  //     CurrentRating = currentRating;
  //     GamesCount = gamesCount;
  //   }

  //   public virtual void UpdateRating(Game game)
  //   {
  //     Games.Add(game);  // Add the game to the list
  //   }

  //   public string GetStats()
  //   {
  //     var history = new System.Text.StringBuilder();

  //     history.AppendLine("Opponent\tResult\tRate\tIndex");

  //     for (int i = 0; i < Games.Count; i++)
  //     {
  //       history.AppendLine($"{Games[i].OpponentName}\t{Games[i].Result}\t{Games[i].Rating}\t{i}");
  //     }

  //     return history.ToString();
  //   }
  // }

  // public class StandardGameAccount : GameAccountBase
  // {
  //   public StandardGameAccount(string userName, decimal currentRating, decimal gamesCount) : base(userName, currentRating, gamesCount)
  //   {
  //   }

  //   public override void UpdateRating(Game game)
  //   {
  //     base.UpdateRating(game);
  //     if (game.Result == GameResult.Win)
  //       CurrentRating += game.Rating;
  //     else if (game.Result == GameResult.Loss)
  //       CurrentRating = Math.Max(Constants.MinRating, CurrentRating - game.Rating);
  //   }
  // }

  // public class ReducedLossGameAccount : GameAccountBase
  // {
  //   public ReducedLossGameAccount(string userName, decimal currentRating, decimal gamesCount)
  //       : base(userName, currentRating, gamesCount)
  //   {
  //   }

  //   public override void UpdateRating(Game game)
  //   {
  //     base.UpdateRating(game);
  //     if (game.Result == GameResult.Win)
  //       CurrentRating += game.Rating;
  //     else if (game.Result == GameResult.Loss)
  //       CurrentRating = Math.Max(Constants.MinRating, CurrentRating - game.Rating / 2);
  //   }
  // }

  // public class WinStreakGameAccount : GameAccountBase
  // {
  //   private int consecutiveWins;

  //   public WinStreakGameAccount(string userName, decimal currentRating, decimal gamesCount) : base(userName, currentRating, gamesCount)
  //   {
  //   }

  //   public override void UpdateRating(Game game)
  //   {
  //     base.UpdateRating(game);
  //     if (game.Result == GameResult.Win)
  //     {
  //       CurrentRating += game.Rating + (consecutiveWins * 50); // Extra points for win streak
  //       consecutiveWins++;
  //     }
  //     else if (game.Result == GameResult.Loss)
  //     {
  //       CurrentRating = Math.Max(Constants.MinRating, CurrentRating - game.Rating);
  //       consecutiveWins = 0;
  //     }
  //   }
  // }

  // public abstract class GameBase
  // {
  //   public abstract decimal CalculateRating();
  // }

  // public class StandardGame : GameBase
  // {
  //   public override decimal CalculateRating()
  //   {
  //     // Implement calculation logic for standard game
  //     return 100;
  //   }
  // }

  // public class TrainingGame : GameBase
  // {
  //   public override decimal CalculateRating()
  //   {
  //     // Implement calculation logic for training game
  //     return 0; // No rating for training game
  //   }
  // }

  // public class SinglePlayerGame : GameBase
  // {
  //   public override decimal CalculateRating()
  //   {
  //     // Implement calculation logic for single player game
  //     return 200;
  //   }
  // }

  // public class GameFactory
  // {
  //   public GameBase CreateGame(string gameType)
  //   {
  //     switch (gameType.ToLower())
  //     {
  //       case "standard":
  //         return new StandardGame();
  //       case "training":
  //         return new TrainingGame();
  //       case "singleplayer":
  //         return new SinglePlayerGame();
  //       default:
  //         throw new ArgumentException("Invalid game type");
  //     }
  //   }
  // }

  // class Program
  // {
  //   static void Main(string[] args)
  //   {
  //     // Create game accounts
  //     GameAccountBase firstPlayer = new StandardGameAccount("John Biolo", 0, Constants.MinRating);
  //     GameAccountBase secondPlayer = new ReducedLossGameAccount("Alex Brits", 0, Constants.MinRating);
  //     GameAccountBase thirdPlayer = new WinStreakGameAccount("Alice", 0, Constants.MinRating);

  //     // Create game factory
  //     GameFactory gameFactory = new GameFactory();

  //     // Game 1: first win with a standard game
  //     GameBase standardGame = gameFactory.CreateGame("standard");
  //     firstPlayer.UpdateRating(new Game(secondPlayer.UserName, standardGame.CalculateRating(), GameResult.Win));
  //     secondPlayer.UpdateRating(new Game(firstPlayer.UserName, standardGame.CalculateRating(), GameResult.Loss));

  //     // Game 2: second win with a training game
  //     GameBase trainingGame = gameFactory.CreateGame("training");
  //     firstPlayer.UpdateRating(new Game(secondPlayer.UserName, trainingGame.CalculateRating(), GameResult.Loss));
  //     secondPlayer.UpdateRating(new Game(firstPlayer.UserName, trainingGame.CalculateRating(), GameResult.Win));

  //     // Game 3: third win with a single player game
  //     GameBase singlePlayerGame = gameFactory.CreateGame("singleplayer");
  //     thirdPlayer.UpdateRating(new Game(thirdPlayer.UserName, singlePlayerGame.CalculateRating(), GameResult.Win));

  //     // Display game history and stats in the console
  //     Console.WriteLine($"Game History and Stats of first player - {firstPlayer.UserName}:");
  //     Console.WriteLine(firstPlayer.GetStats());

  //     Console.WriteLine($"Game History and Stats of second player - {secondPlayer.UserName}:");
  //     Console.WriteLine(secondPlayer.GetStats());

  //     Console.WriteLine($"Game History and Stats of third player - {thirdPlayer.UserName}:");
  //     Console.WriteLine(thirdPlayer.GetStats());
  //   }
  // }
}