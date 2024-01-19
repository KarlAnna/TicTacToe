using Classes.Utils;

namespace Classes.Repositories
{
  public interface IUsersRepository
  {
    User.User? GetUserByName(string name);

    User.User CreateUser(string name, decimal initialRating, string password);
  }

  public class UsersRepository : IUsersRepository
  {
    private readonly DbContext _dbContext;

    public UsersRepository(DbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public User.User? GetUserByName(string name)
    {
      return _dbContext.Users.Find(user => user.Name == name);
    }

    public User.User CreateUser(string name, decimal initialRating, string password)
    {
      var createdUserId = _dbContext.Users.Count + 1;
      var createdUser = new User.User(createdUserId, name, initialRating, password);
      _dbContext.Users.Add(createdUser);

      return createdUser;
    }
  }
}