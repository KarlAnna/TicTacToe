namespace Classes.Game
{
  public enum Result
  {
    Win, Draw, Lose
  }

  public class Game
  {
    public readonly int Rating;
    public readonly Result Result;

    public Game(Result result, int rating)
    {
      Result = result;
      Rating = rating;
    }
  }
}