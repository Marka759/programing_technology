namespace GuessTheNumber.Core.Statistics;

public class GameStatistics
{
    private int _gamesPlayed;
    private int _totalAttempts;
    private int _minAttempts = int.MaxValue; 
    private int _maxAttempts;

    public void Register(bool isWon, int attempts)
    {
        if (!isWon) return;   
        _gamesPlayed++;
        _totalAttempts += attempts;
        _minAttempts = Math.Min(_minAttempts, attempts);
        _maxAttempts = Math.Max(_maxAttempts, attempts);
    }

    public string ToReport()
    {
        if (_gamesPlayed == 0)
            return "No completed games.";

        double average = _totalAttempts * 1.0 / _gamesPlayed;
        return $"min={_minAttempts} max={_maxAttempts} avg={average:F2}";
    }
}
