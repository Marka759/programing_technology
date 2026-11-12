namespace GuessTheNumber.Core.Statistics;

public class SafeFileStatistics : IStatisticsSave
{
    private readonly string _filePath;

    public SafeFileStatistics(string filePath)
    {
        _filePath = filePath;
    }

    public void Save(GameStatistics stats)
    {
        string[] lines =
            {
                $"Всего игр:{stats.GamesPlayed}",
                $"Побед:{stats.Wins}",
                $"Поражений:{stats.Losses}",
                $"Всего попыток:{stats.TotalAttempts}",
                $"Минимальное количество попыток:{stats.MinAttempts}",
                $"Максимальное количество попыток:{stats.MaxAttempts}"
            };

        File.WriteAllLines(_filePath, lines);
    }
}