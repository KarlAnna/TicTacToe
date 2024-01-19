using System.Text;

namespace Classes.User
{

  public class User
  {
    private readonly List<Game.Game> _games = new List<Game.Game>();
    private readonly string _password;
    public readonly int Id;
    public readonly string Name;
    private decimal _rating;

    public User(int id, string name, decimal initialRating, string password)
    {
      Id = id;
      Name = name;
      _rating = initialRating;
      _password = password;
    }


    public bool CheckPassword(string? password)
    {
      return password != null && password == _password;
    }

    public void WinGame(Game.Game game)
    {
      _games.Add(game);
      _rating += game.Rating;
    }

    public void LoseGame(Game.Game game)
    {
      _games.Add(game);
      _rating = Math.Max(_rating - game.Rating, 0);
    }

    public void DrawGame(Game.Game game)
    {
      _games.Add(game);
    }

    public string GetStats()
    {
      Console.WriteLine($"Your rating: {_rating}");
      var history = new StringBuilder();

      history.AppendLine($"Total current rating: {_rating}\n");
      history.AppendLine("Result\tRate\tIndex");

      for (var i = 0; i < _games.Count; i++)
        history.AppendLine($"{i}\t{_games[i].Result}\t{_games[i].Rating}");

      return history.ToString();
    }
  }
}