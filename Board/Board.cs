using Classes.Game;

namespace Classes.Board
{
  public class Board
  {

    private readonly User.User _currentUser;
    private readonly char[] _gameBoard = { '1', '2', '3', '4', '5', '6', '7', '8', '9' };

    public Board(User.User currentUser)
    {
      _currentUser = currentUser;
    }

    private void RegisterResult(GameStatus gameStatus, bool isUserMove)
    {
      if (gameStatus == GameStatus.FinishedWithDraw)
      {
        Console.WriteLine("Play ended drawn.");
        _currentUser.DrawGame(new Game.Game(Result.Draw, 100));

        return;
      }

      if (isUserMove)
      {
        Console.WriteLine($"Player {_currentUser.Name} won!");
        _currentUser.WinGame(new Game.Game(Result.Win, 100));
      }
      else
      {
        Console.WriteLine($"Player {_currentUser.Name} lost!");
        _currentUser.LoseGame(new Game.Game(Result.Lose, 100));
      }
    }

    private bool IsChoiceOutOfRange(int choice)
    {
      return choice < 1 || choice > 9;
    }

    private bool IsChoiceAlreadyTaken(int choice)
    {
      return _gameBoard[choice - 1] == 'X' || _gameBoard[choice - 1] == 'O';
    }

    public void PlayGame()
    {
      GameStatus gameStatus;
      var isUsersMove = true;

      do
      {
        Console.Clear();
        Console.WriteLine($"Player 1 ({_currentUser.Name}): X and Player 2: O\n");
        Console.WriteLine(isUsersMove ? $"Player 1 ({_currentUser.Name}) is playing" : "Player 2 is playing\n");

        DrawBoard();

        bool validInput;
        int markNumber;
        do
        {
          Console.Write("Choose the cell number (1-9): ");
          var input = Console.ReadLine();
          validInput = int.TryParse(input, out markNumber);

          if (!validInput)
          {
            Console.WriteLine("Enter a valid number.");
          }
          else if (IsChoiceOutOfRange(markNumber))
          {
            Console.WriteLine("Enter a number between 1 and 9.");
          }
          else if (IsChoiceAlreadyTaken(markNumber))
          {
            Console.WriteLine("Choose an empty cell.");
          }
        } while (!validInput || IsChoiceOutOfRange(markNumber) || IsChoiceAlreadyTaken(markNumber));

        _gameBoard[markNumber - 1] = isUsersMove ? 'X' : '0';

        gameStatus = CheckGameStatus();

        if (gameStatus == GameStatus.InProgress)
        {
          isUsersMove = !isUsersMove;
        }
      } while (gameStatus == GameStatus.InProgress);

      Console.Clear();
      DrawBoard();
      RegisterResult(gameStatus, isUsersMove);
      Console.ReadLine();
    }


    private void DrawBoard()
    {
      for (int row = 0; row < 3; row++)
      {
        Console.WriteLine("     |     |      ");
        Console.WriteLine($"  {_gameBoard[row * 3]}  |  {_gameBoard[row * 3 + 1]}  |  {_gameBoard[row * 3 + 2]}");

        if (row < 2)
        {
          Console.WriteLine("_____|_____|_____ ");
        }
      }
      Console.WriteLine("     |     |      ");
    }


    private GameStatus CheckGameStatus()
    {
      #region Horzontal Winning Condtion

      if (_gameBoard[0] == _gameBoard[1] && _gameBoard[1] == _gameBoard[2])
      {
        return GameStatus.FinishedWithVictory;
      }

      if (_gameBoard[3] == _gameBoard[4] && _gameBoard[4] == _gameBoard[5])
      {
        return GameStatus.FinishedWithVictory;
      }

      if (_gameBoard[6] == _gameBoard[7] && _gameBoard[7] == _gameBoard[8])
      {
        return GameStatus.FinishedWithVictory;
      }

      #endregion

      #region Vertical Winning Condtion

      if (_gameBoard[0] == _gameBoard[3] && _gameBoard[3] == _gameBoard[6])
      {
        return GameStatus.FinishedWithVictory;
      }

      if (_gameBoard[1] == _gameBoard[4] && _gameBoard[4] == _gameBoard[7])
      {
        return GameStatus.FinishedWithVictory;
      }

      if (_gameBoard[2] == _gameBoard[5] && _gameBoard[5] == _gameBoard[8])
      {
        return GameStatus.FinishedWithVictory;
      }

      #endregion

      #region Diagonal Winning Condtion

      if (_gameBoard[0] == _gameBoard[4] && _gameBoard[4] == _gameBoard[8])
      {
        return GameStatus.FinishedWithVictory;
      }

      if (_gameBoard[2] == _gameBoard[4] && _gameBoard[4] == _gameBoard[6])
      {
        return GameStatus.FinishedWithVictory;
      }

      #endregion

      #region Check For Draw

      if (_gameBoard[0] != '1' && _gameBoard[1] != '2' && _gameBoard[2] != '3' && _gameBoard[3] != '4' &&
          _gameBoard[4] != '5' && _gameBoard[5] != '6' && _gameBoard[6] != '7' && _gameBoard[7] != '8' && _gameBoard[8] != '9')
      {
        return GameStatus.FinishedWithDraw;
      }

      #endregion

      return GameStatus.InProgress;
    }

    private enum GameStatus
    {
      InProgress,
      FinishedWithVictory,
      FinishedWithDraw
    }
  }
}