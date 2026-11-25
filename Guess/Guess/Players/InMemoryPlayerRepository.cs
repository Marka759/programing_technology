namespace GuessTheNumber.Core.Players;

public class InMemoryPlayerRepository : IPlayerRepository
{
    private readonly Dictionary<string, Player> _players =
        new(StringComparer.OrdinalIgnoreCase);

    public Player? Find(string username)
    {
        _players.TryGetValue(username, out Player? player);
        return player;
    }

    public Player Create(string username)
    {
        if (Exists(username))
            throw new InvalidOperationException($"Игрок '{username}' уже существует.");

        var player = new Player(username);
        _players[username] = player;
        return player;
    }

    public bool Exists(string username)
        => _players.ContainsKey(username);
}