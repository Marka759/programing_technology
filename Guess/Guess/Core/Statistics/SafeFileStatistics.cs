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
        string minText = stats.MinAttempts?.ToString() ?? "нет данных";
        string maxText = stats.MaxAttempts?.ToString() ?? "нет данных";

        string[] lines =
        {
            $"Всего игр: {stats.GamesPlayed}",
            $"Побед: {stats.Wins}",
            $"Поражений: {stats.Losses}",
            $"Всего попыток: {stats.TotalAttempts}",
            $"Минимальное количество попыток: {minText}",
            $"Максимальное количество попыток: {maxText}"
        };

        File.WriteAllLines(_filePath, lines);
    }
}