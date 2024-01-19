using Classes.Repositories;
using Classes.Utils;
using Classes.Services;
using Classes.UI;

namespace Classes
{
  class Program
  {
    static void Main()
    {
      Run();
    }
    static void Run()
    {
      var dbContext = new DbContext();
      var usersRepository = new UsersRepository(dbContext);
      var usersService = new UsersService(usersRepository);

      var authUI = new AuthUI(usersService);
      var nextUserUI = (User.User activeUser) => new UserUI(activeUser).ShowUI();

      Dictionary<int, (string commandInfo, Action command)> uiCommands = new Dictionary<int, (string, Action)>
      {
        { 1, ("Registration", () => authUI.RegisterUser(nextUserUI)) },
        { 2, ("Login", () => authUI.Login(nextUserUI)) },
        { 3, ("Exit", () => Environment.Exit(0)) }
      };

      Console.WriteLine("Welcome!");
      ConsoleUI.ShowMenu(() => true, uiCommands);
    }
  }
}