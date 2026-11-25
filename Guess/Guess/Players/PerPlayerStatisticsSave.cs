using GuessTheNumber.Core.Players;

namespace GuessTheNumber.Core.Statistics;

public class PerPlayerStatisticsSave : IStatisticsSave
{
    private readonly string _directory;

    public PerPlayerStatisticsSave(string directory)
    {
        _directory = directory;
    }

    public void Save(GameStatistics stats)
    {
        throw new NotSupportedException();
    }

    public void SaveFor(Player player)
    {
        Directory.CreateDirectory(_directory);
        string path = GetPath(player.Username);

        string minText = player.Statistics.Wins > 0
            ? player.Statistics.MinAttempts.ToString()
            : "нет данных";
        string maxText = player.Statistics.Wins > 0
            ? player.Statistics.MaxAttempts.ToString()
            : "нет данных";

        string[] lines =
        {
            $"Игрок: {player.Username}",
            $"Всего игр: {player.Statistics.GamesPlayed}",
            $"Побед: {player.Statistics.Wins}",
            $"Поражений: {player.Statistics.Losses}",
            $"Всего попыток: {player.Statistics.TotalAttempts}",
            $"Минимальное количество попыток: {minText}",
            $"Максимальное количество попыток: {maxText}"
        };

        File.WriteAllLines(path, lines);
    }

   
    public bool TryLoadFor(Player player)
    {
        string path = GetPath(player.Username);
        if (!File.Exists(path))
            return false;

        var values = new Dictionary<string, string>();
        foreach (string line in File.ReadAllLines(path))
        {
            int colon = line.IndexOf(':');
            if (colon <= 0) continue;
            string key = line[..colon].Trim();
            string value = line[(colon + 1)..].Trim();
            values[key] = value;
        }

        if (!values.TryGetValue("Всего игр", out string? games) ||
            !values.TryGetValue("Побед", out string? wins) ||
            !values.TryGetValue("Поражений", out string? losses) ||
            !values.TryGetValue("Всего попыток", out string? total))
            return false;

        int? min = ParseNullable(values.GetValueOrDefault("Минимальное количество попыток"));
        int? max = ParseNullable(values.GetValueOrDefault("Максимальное количество попыток"));

        player.Statistics.Restore(
            gamesPlayed: int.Parse(games),
            wins: int.Parse(wins),
            losses: int.Parse(losses),
            totalAttempts: int.Parse(total),
            minAttempts: min,
            maxAttempts: max);

        return true;
    }

    public void DeleteFor(Player player)
    {
        string path = GetPath(player.Username);
        if (File.Exists(path))
            File.Delete(path);
    }
    public IEnumerable<string> EnumerateUsernames()
    {
        if (!Directory.Exists(_directory))
            yield break;

        foreach (string file in Directory.EnumerateFiles(_directory, "*.txt"))
            yield return Path.GetFileNameWithoutExtension(file);
    }

    private static int? ParseNullable(string? text)
    {
        if (string.IsNullOrEmpty(text) || text == "нет данных")
            return null;
        return int.TryParse(text, out int value) ? value : null;
    }

    private string GetPath(string username)
        => Path.Combine(_directory, $"{MakeSafeFileName(username)}.txt");

    private static string MakeSafeFileName(string username)
    {
        foreach (char c in Path.GetInvalidFileNameChars())
            username = username.Replace(c, '_');
        return username;
    }
}