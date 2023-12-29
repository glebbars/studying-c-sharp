using Classes.Games;

namespace Classes.Accounts;

public class DoublePointsForLossAccount : GameAccountBase
{
    public DoublePointsForLossAccount(decimal accountId, string userName, decimal currentRating) : base(accountId,
        userName,
        currentRating)
    {
    }

    public override string GetAccountType()
    {
        return "Double points account";
    }

    public override void WinGame(GameBase game)
    {
        base.WinGame(game);
        CurrentRating += game.CalculateRating();
    }

    public override void LoseGame(GameBase game)
    {
        base.LoseGame(game);
        CurrentRating = ValidateRatingAfterLoss(game.CalculateRating() * 2);
    }
}