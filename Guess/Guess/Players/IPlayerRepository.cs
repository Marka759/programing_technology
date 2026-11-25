namespace GuessTheNumber.Core.Players;

public interface IPlayerRepository
{
    Player? Find(string username);
    Player Create(string username);
    bool Exists(string username);
}