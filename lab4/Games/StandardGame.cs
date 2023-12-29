using Classes.Constants;
using Classes.Enums;

namespace Classes.Games;

public class StandardGame : GameBase
{
    public StandardGame(string opponentName, GameResult result) : base(opponentName, result)
    {
    }

    public override decimal CalculateRating()
    {
        return GameConstants.StandardGameRating;
    }
}