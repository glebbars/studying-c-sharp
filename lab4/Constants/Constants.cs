namespace Classes.Constants;

public static class Symbols
{
    public const string CheckMark = "\u2705";
}

public static class GameConstants
{
    public const int MinRating = 1;
    public const short StandardGameRating = 100;
    public const string TrainingGameOpponentName = "Training Opponent";
}

public static class ExceptionMessages
{
    public const string NegativeRatingExceptionMessage = "Rating cannot be negative";
    public const string TypedPlayerNameIsNull = "Typed player name cannot be null or a whitespce";
    public const string IncorrectGameResultArgumentMessage =
        "Passed result does not correspond with the invoked method";

    public const string NotFoundGameAccountExceptionMessage = "GameAccount withh such name does not exist";
}