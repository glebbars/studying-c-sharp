using Classes.Enums;

namespace Classes.Games;

public abstract class GameBase
{
    protected GameBase(string opponentName, GameResult result)
    {
        OpponentName = opponentName;
        Result = result;
    }

    public GameResult Result { get; }
    public string OpponentName { get; }

    public abstract decimal CalculateRating();
}