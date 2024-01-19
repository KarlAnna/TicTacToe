using Classes.Repositories;

namespace Classes.Services
{
  interface IUsersService
  {
    User.User? GetUserByName(string name);

    User.User CreateUser(string name, decimal initialRating, string password);
  }

  public class UsersService : IUsersService
  {
    private readonly UsersRepository _repository;

    public UsersService(UsersRepository repository)
    {
      _repository = repository;
    }

    public User.User? GetUserByName(string name)
    {
      return _repository.GetUserByName(name);
    }

    public User.User CreateUser(string name, decimal initialRating, string password)
    {
      return _repository.CreateUser(name, initialRating, password);
    }
  }
}