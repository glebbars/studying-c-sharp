using Classes.Enums;

namespace Classes.Accounts;

public class GameAccountFactory
{
    public GameAccountBase CreateGameAccount(decimal gameAccountId, GameAccountType gameAccountType, string userName,
        decimal currentRating)
    {
        switch (gameAccountType)
        {
            case GameAccountType.Standard:
                return new StandardGameAccount(gameAccountId, userName, currentRating);
            case GameAccountType.DoublePointsForLoss:
                return new DoublePointsForLossAccount(gameAccountId, userName, currentRating);
            case GameAccountType.WinStreak:
                return new WinStreakGameAccount(gameAccountId, userName, currentRating);
            default:
                throw new ArgumentException("Invalid game account type");
        }
    }
}