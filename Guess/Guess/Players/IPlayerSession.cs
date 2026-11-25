namespace GuessTheNumber.Core.Players;

public interface IPlayerSession
{
    Player? CurrentPlayer { get; }
    bool IsLoggedIn { get; }
    void Login(Player player);
    void Logout();
}