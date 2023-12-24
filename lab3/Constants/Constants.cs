namespace Classes.Constants
{
  public static class GameConstants
  {
    public const int MinRating = 1;
    public const short StandardGameRating = 100;
    public const string TrainingGameOpponentName = "Training Game";
  }

  public static class ExceptionMessages
  {
    public const string NegativeRatingExceptionMessage = "Rating cannot be negative";
    public const string IncorrectGameResultArgumentMessage = "Passed result does not correspond with the invoked method";
    public const string NotFoundGameAccountExceptionMessage = "GameAccount withh such name does not exist";
  }
}