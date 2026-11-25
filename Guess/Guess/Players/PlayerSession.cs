namespace GuessTheNumber.Core.Players;

public class PlayerSession : IPlayerSession
{
    public Player? CurrentPlayer { get; private set; }
    public bool IsLoggedIn => CurrentPlayer != null;

    public void Login(Player player)
    {
        if (player == null) throw new ArgumentNullException();
        CurrentPlayer = player;
    }

    public void Logout()
    {
        CurrentPlayer = null;
    }
}