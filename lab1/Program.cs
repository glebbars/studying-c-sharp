namespace Classes;

public enum GameResult
{
  Win,
  Loss
}

public static class Constants
{
  public const int MinRating = 1; // рейтинг не може стати менше 1
  public const string NegativeRatingExceptionMessage = "Рейтинг не може бути негативним";
}

public class Game
{
  public string OpponentName { get; }
  public decimal Rating { get; }
  public GameResult Result { get; }

  public Game(string opponentName, decimal rating, GameResult result)
  {
    this.OpponentName = opponentName;
    this.Rating = rating;
    this.Result = result;
  }
}


public class GameAccount
{
  public string UserName { get; }
  public decimal CurrentRating { get; set; }

  public decimal GamesCount { get; }

  public List<Game> Games = new List<Game>();

  public GameAccount(string userName, decimal currentRating, decimal gamesCount)
  {
    this.UserName = userName;
    this.CurrentRating = currentRating;
    this.GamesCount = gamesCount;
  }


  public void WinGame(string opponentName, decimal rating)
  {
    // рейтинг, на який грають не може бути від'ємним - кидаємо помилку
    if (rating < 0)
    {
      throw new ArgumentOutOfRangeException(nameof(rating), $"{Constants.NegativeRatingExceptionMessage}");
    }

    this.CurrentRating += rating;

    var game = new Game(opponentName, rating, GameResult.Win);
    Games.Add(game);

  }

  public void LoseGame(string opponentName, decimal rating)
  {
    // рейтинг, на який грають не може бути від'ємним - кидаємо помилку
    if (rating < 0)
    {
      throw new ArgumentOutOfRangeException(nameof(rating), $"{Constants.NegativeRatingExceptionMessage}");
    }

    var currentgRatinAfterLoss = this.CurrentRating - rating;
    if (currentgRatinAfterLoss <= Constants.MinRating) // рейтинг не може стати менше 1
    {
      this.CurrentRating = Constants.MinRating;
    }
    else
    {
      this.CurrentRating = currentgRatinAfterLoss;
    }

    var game = new Game(opponentName, rating, GameResult.Loss);
    Games.Add(game);
  }

  public string GetStats()
  {
    var history = new System.Text.StringBuilder();

    history.AppendLine("Opponent\tResult\tRate\tIndex");

    for (int i = 0; i < Games.Count; i++)
    {
      history.AppendLine($"{Games[i].OpponentName}\t{Games[i].Result}\t{Games[i].Rating}\t{i}");
    }

    return history.ToString();
  }
}


class Program
{
  static void Main(string[] args)
  {
    // Create a games accounts
    GameAccount firstPlayer = new GameAccount("John Biolo", 0, Constants.MinRating);
    GameAccount secondPlayer = new GameAccount("Alex Brits", 0, Constants.MinRating);

    // Game 1: first win
    firstPlayer.WinGame(secondPlayer.UserName, 120);
    secondPlayer.LoseGame(firstPlayer.UserName, 120);

    // Game 2: second win
    firstPlayer.LoseGame(secondPlayer.UserName, 350);
    secondPlayer.WinGame(firstPlayer.UserName, 350);

    // Game 3: second win
    firstPlayer.LoseGame(secondPlayer.UserName, 500);
    secondPlayer.WinGame(firstPlayer.UserName, 500);

    // Display games history in the console:
    // first player
    Console.WriteLine($"Game History of first player - {firstPlayer.UserName}:");
    Console.WriteLine(firstPlayer.GetStats());

    // second player
    Console.WriteLine($"Game History of second player - {secondPlayer.UserName}:");
    Console.WriteLine(secondPlayer.GetStats());
  }
}