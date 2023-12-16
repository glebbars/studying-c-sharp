namespace Classes
{
  public enum GameResult
  {
    Win,
    Loss
  }

  public enum GameType
  {
    Standard,
    DoublePoints,
    Training
  }

  public static class Constants
  {
    public const int MinRating = 1;
    public const string NegativeRatingExceptionMessage = "Рейтинг не може бути негативним";
    public const string IncorrectGameResultArgumentMessage = "Переданий результат ігри не відповідає визваному методі";
    public const decimal StandardGameRating = 100;
  }

  // Базова абстрактна гра
  public abstract class GameBase
  {
    public string OpponentName { get; }
    public GameResult Result { get; }
    public GameBase(string opponentName, GameResult result)
    {
      OpponentName = opponentName;
      Result = result;
    }

    public abstract decimal CalculateRating();
  }

  // Стандартна гра
  public class StandardGame : GameBase
  {

    public StandardGame(string opponentName, GameResult result) : base(opponentName, result)
    {
    }

    public override decimal CalculateRating()
    {
      return Constants.StandardGameRating;
    }
  }

  // Тренвуальна гра
  public class TrainingGame : GameBase
  {
    public TrainingGame(string opponentName, GameResult result) : base(opponentName, result)
    {
    }

    public override decimal CalculateRating()
    {
      return 0;
    }
  }

  // Гра на подвійні бали
  public class DoublePointsGame : GameBase
  {

    public DoublePointsGame(string opponentName, GameResult result) : base(opponentName, result)
    {
    }

    public override decimal CalculateRating()
    {
      return Constants.StandardGameRating * 2;
    }
  }

  // Факторі створення різних типів ігор
  public class GameFactory
  {
    public GameBase CreateGame(GameType gameType, string opponentName, GameResult result)
    {
      switch (gameType)
      {
        case GameType.Standard:
          return new StandardGame(opponentName, result);
        case GameType.Training:
          return new TrainingGame(opponentName, result);
        case GameType.DoublePoints:
          return new DoublePointsGame(opponentName, result);
        default:
          throw new ArgumentException("Invalid game type");
      }
    }
  }

  // Безовий абстрактний аккунт
  public abstract class GameAccountBase
  {
    public string UserName { get; }
    public decimal CurrentRating { get; set; }

    public List<GameBase> Games { get; } = new List<GameBase>();

    protected GameAccountBase(string userName, decimal currentRating)
    {
      UserName = userName;
      CurrentRating = currentRating;
    }

    public abstract string GetAccountType();

    private void ValidatePositiveRating(decimal rating)
    {
      if (rating < 0) // рейтинг гри не може бути менше 0
      {
        throw new ArgumentOutOfRangeException(nameof(rating), $"{Constants.NegativeRatingExceptionMessage}");
      }
    }

    private void VaidateGameResult(GameResult gameResult, GameResult correctGameResult)
    {
      if (gameResult != correctGameResult) // ігрок не може виграти з результатом не Win
      {
        throw new ArgumentException(nameof(gameResult), $"{Constants.IncorrectGameResultArgumentMessage}");
      }
    }
    public decimal ValidateRatingAfterLoss(decimal lostRating)
    {
      var currentgRatingAfterLoss = this.CurrentRating - lostRating;
      if (currentgRatingAfterLoss <= Constants.MinRating) // рейтинг не може стати менше 1
      {
        return Constants.MinRating;
      }
      else
      {
        return currentgRatingAfterLoss;
      }
    }

    public virtual void WinGame(GameBase game)
    {
      var rating = game.CalculateRating();

      this.ValidatePositiveRating(rating);
      this.VaidateGameResult(game.Result, GameResult.Win); // ігрок не може виграти з результатом не Win

      Games.Add(game);
    }

    public virtual void LoseGame(GameBase game)
    {
      var rating = game.CalculateRating();

      this.ValidatePositiveRating(rating);
      this.VaidateGameResult(game.Result, GameResult.Loss); // ігрок не може виграти з результатом не Win

      Games.Add(game);
    }

    public string GetStats()
    {
      var history = new System.Text.StringBuilder();

      history.AppendLine($"Total current rating: {this.CurrentRating}\n");
      history.AppendLine("Opponent\tResult\tRate\tIndex");

      for (int i = 0; i < Games.Count; i++)
        history.AppendLine($"{Games[i].OpponentName}\t{Games[i].Result}\t{Games[i].CalculateRating()}\t{i}");

      return history.ToString();
    }
  }

  // Стандартий аккаунт
  public class StandardGameAccount : GameAccountBase
  {
    public StandardGameAccount(string userName, decimal currentRating) : base(userName, currentRating)
    {
    }

    public override string GetAccountType()
    {
      return "Standard account";
    }

    public override void WinGame(GameBase game)
    {
      base.WinGame(game);
      this.CurrentRating += game.CalculateRating();
    }

    public override void LoseGame(GameBase game)
    {
      base.LoseGame(game);
      this.CurrentRating = this.ValidateRatingAfterLoss(game.CalculateRating());
    }
  }

  // Аккаунт, в якого знімається в 2 раз більше балів при програші
  public class DoublePointsForLossAccount : GameAccountBase
  {
    public DoublePointsForLossAccount(string userName, decimal currentRating) : base(userName, currentRating)
    {
    }

    public override string GetAccountType()
    {
      return "Double points account";
    }

    public override void WinGame(GameBase game)
    {
      base.WinGame(game);
      this.CurrentRating += game.CalculateRating();
    }

    public override void LoseGame(GameBase game)
    {
      base.LoseGame(game);
      this.CurrentRating = this.ValidateRatingAfterLoss(game.CalculateRating() * 2);
    }
  }

  // Аккаунт з нарахуванням додаткових балів за серію перемог
  public class WinStreakGameAccount : GameAccountBase
  {
    private int consecutiveWins;

    public WinStreakGameAccount(string userName, decimal currentRating) : base(userName, currentRating)
    {
    }

    public override string GetAccountType()
    {
      return "Win streak account";
    }


    public override void WinGame(GameBase game)
    {
      base.WinGame(game);
      this.consecutiveWins += 1; // інкрементація перемог підряд
      this.CurrentRating += game.CalculateRating() * this.consecutiveWins;
    }

    public override void LoseGame(GameBase game)
    {
      base.LoseGame(game);

      this.consecutiveWins = 0; // перемоги підряд онулюються
      this.CurrentRating = this.ValidateRatingAfterLoss(game.CalculateRating());
    }
  }

  class Program
  {
    static void Main(string[] args)
    {
      // Створення різних акаунтів
      GameAccountBase firstPlayer = new StandardGameAccount("John Smiths", Constants.MinRating);
      GameAccountBase secondPlayer = new StandardGameAccount("Mark Stone", Constants.MinRating);
      GameAccountBase thirdPlayer = new WinStreakGameAccount("Liam Breklo", Constants.MinRating);
      GameAccountBase forthPlayer = new DoublePointsForLossAccount("Emma Parks", Constants.MinRating);

      // Створення факторі ігор
      GameFactory gameFactory = new GameFactory();

      // Ігра 1: перший ігрок перемогає другого в стандартній грі
      firstPlayer.WinGame(gameFactory.CreateGame(GameType.Standard, secondPlayer.UserName, GameResult.Win));
      secondPlayer.LoseGame(gameFactory.CreateGame(GameType.Standard, firstPlayer.UserName, GameResult.Loss));

      // Ігра 2: третій ігрок перемогає другого в грі на подвійні бали
      thirdPlayer.WinGame(gameFactory.CreateGame(GameType.DoublePoints, secondPlayer.UserName, GameResult.Win));
      secondPlayer.LoseGame(gameFactory.CreateGame(GameType.DoublePoints, thirdPlayer.UserName, GameResult.Loss));

      // Ігра 3: третій ігрок перемогає першого в треновачній грі
      thirdPlayer.WinGame(gameFactory.CreateGame(GameType.Training, firstPlayer.UserName, GameResult.Win));
      firstPlayer.LoseGame(gameFactory.CreateGame(GameType.Training, thirdPlayer.UserName, GameResult.Loss));

      // Ігра 4: четвертий ігрок перемогає другого в грі на подвійні бали
      forthPlayer.WinGame(gameFactory.CreateGame(GameType.DoublePoints, secondPlayer.UserName, GameResult.Win));
      secondPlayer.LoseGame(gameFactory.CreateGame(GameType.DoublePoints, forthPlayer.UserName, GameResult.Loss));

      // Виведення історій всіх ігроків
      Console.WriteLine($"\nGame History and Stats of first player - {firstPlayer.UserName} with {firstPlayer.GetAccountType()}:");

      Console.WriteLine(firstPlayer.GetStats());

      Console.WriteLine($"Game History and Stats of second player - {secondPlayer.UserName} with {secondPlayer.GetAccountType()}:");
      Console.WriteLine(secondPlayer.GetStats());

      Console.WriteLine($"Game History and Stats of third player - {thirdPlayer.UserName} with {thirdPlayer.GetAccountType()}:");
      Console.WriteLine(thirdPlayer.GetStats());

      Console.WriteLine($"Game History and Stats of forth player - {forthPlayer.UserName} with {forthPlayer.GetAccountType()}:");
      Console.WriteLine(forthPlayer.GetStats());
    }
  }
}