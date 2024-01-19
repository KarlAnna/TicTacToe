using Classes.Services;

namespace Classes.UI
{

  public class AuthUI
  {
    private readonly UsersService _usersService;

    public AuthUI(UsersService usersService)
    {
      _usersService = usersService;
    }

    public void RegisterUser(Action<User.User> next)
    {
      Console.Write("Enter your username: ");
      var username = Console.ReadLine();

      if (username == null)
      {
        Console.WriteLine("Please enter a valid username.");

        return;
      }

      Console.Write("Enter your password: ");
      var password = Console.ReadLine();

      if (password == null)
      {
        Console.WriteLine("You have been successfully registered!");

        return;
      }

      var createdUser = _usersService.CreateUser(username, 1000, password);
      Console.WriteLine("You have been successfully registered!");

      next(createdUser);
    }

    public void Login(Action<User.User> next)
    {
      Console.Write("Enter your username: ");
      var username = Console.ReadLine();

      if (username == null)
      {
        Console.WriteLine("User with this username does not exist. Please register first.");

        return;
      }

      var foundUser = _usersService.GetUserByName(username);

      if (foundUser == null)
      {
        Console.WriteLine("User with the specified username was not found.");

        return;
      }

      Console.Write("Enter your password: ");
      var password = Console.ReadLine();

      if (!foundUser.CheckPassword(password))
      {
        Console.WriteLine("Invalid password.");

        return;
      }

      Console.WriteLine($"You have logged in as {foundUser.Name}.");

      next(foundUser);
    }
  }
}