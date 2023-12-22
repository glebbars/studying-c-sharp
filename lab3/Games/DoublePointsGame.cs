using Classes.Constants;
using Classes.Enums;

namespace Classes.Games
{
  public class DoublePointsGame : GameBase
  {
    public DoublePointsGame(string opponentName, GameResult result) : base(opponentName, result)
    {
    }

    public override decimal CalculateRating()
    {
      return GlobalConstants.StandardGameRating * 2;
    }
  }
}