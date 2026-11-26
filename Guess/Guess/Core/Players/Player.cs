using GuessTheNumber.Core.Statistics;
//SRP
namespace Guess.Core.Players;

public class Player
{
    public string Username { get; }
    public GameStatistics Statistics { get; }

    public Player(string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            throw new ArgumentException("Имя игрока не может быть пустым.", nameof(username));

        Username = username;
        Statistics = new GameStatistics();
    }
}