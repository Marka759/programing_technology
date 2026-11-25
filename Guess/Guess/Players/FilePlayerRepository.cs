using GuessTheNumber.Core.Statistics;

namespace GuessTheNumber.Core.Players;
//LSP,OCP
public class FilePlayerRepository : IPlayerRepository
{
    private readonly Dictionary<string, Player> _players =
        new(StringComparer.OrdinalIgnoreCase);

    private readonly PerPlayerStatisticsSave _storage;

    public FilePlayerRepository(PerPlayerStatisticsSave storage)
    {
        _storage = storage;
        LoadAll();
    }

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





        _storage.SaveFor(player);
        return player;
    }

    public bool Exists(string username)
        => _players.ContainsKey(username);

    public void SaveAll()
    {
        foreach (Player player in _players.Values)
            _storage.SaveFor(player);
    }

    private void LoadAll()
    {
        foreach (string username in _storage.EnumerateUsernames())
        {
            var player = new Player(username);
            _storage.TryLoadFor(player);   // восстановит статистику, если файл валиден
            _players[username] = player;
        }
    }
}