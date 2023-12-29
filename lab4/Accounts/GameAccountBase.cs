using System.Text;
using Classes.Constants;
using Classes.Enums;
using Classes.Games;

namespace Classes.Accounts;

public abstract class GameAccountBase
{
    public GameAccountBase(decimal accountId, string userName, decimal currentRating)
    {
        AccountId = accountId;
        UserName = userName;
        CurrentRating = currentRating;
    }

    public decimal AccountId { get; }
    public string UserName { get; }
    public decimal CurrentRating { get; set; }
    private List<GameBase> Games { get; } = new();

    public abstract string GetAccountType();

    private static void ValidatePositiveRating(decimal rating)
    {
        if (rating < 0)
            throw new ArgumentOutOfRangeException(nameof(rating),
                $"{ExceptionMessages.NegativeRatingExceptionMessage}");
    }

    private static void ValidateGameResult(GameResult gameResult, GameResult correctGameResult)
    {
        if (gameResult != correctGameResult)
            throw new ArgumentException($"{ExceptionMessages.IncorrectGameResultArgumentMessage}", nameof(gameResult));
    }

    protected decimal ValidateRatingAfterLoss(decimal lostRating)
    {
        var currentRatingAfterLoss = CurrentRating - lostRating;

        if (currentRatingAfterLoss <= GameConstants.MinRating) return GameConstants.MinRating;

        return currentRatingAfterLoss;
    }

    public virtual void WinGame(GameBase game)
    {
        var rating = game.CalculateRating();

        ValidatePositiveRating(rating);
        ValidateGameResult(game.Result, GameResult.Win);

        Games.Add(game);
    }

    public virtual void LoseGame(GameBase game)
    {
        var rating = game.CalculateRating();

        ValidatePositiveRating(rating);
        ValidateGameResult(game.Result, GameResult.Loss);

        Games.Add(game);
    }

    public string GetStats()
    {
        var history = new StringBuilder();

        history.AppendLine($"Total current rating: {CurrentRating} {(CurrentRating == 1 ? "(min rating)" : "")}\n");
        history.AppendLine("Opponent\tResult\tRate\tIndex");

        for (var i = 0; i < Games.Count; i++)
            history.AppendLine($"{Games[i].OpponentName}\t{Games[i].Result}\t{Games[i].CalculateRating()}\t{i}");

        return history.ToString();
    }
}