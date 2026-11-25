namespace GuessTheNumber.Core.Players;
//DIP,ISP
public interface IPlayerRepository
{
    Player? Find(string username);
    Player Create(string username);
    bool Exists(string username);
}