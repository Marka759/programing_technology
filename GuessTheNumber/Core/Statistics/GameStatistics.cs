using GuessTheNumber.Core.Game;

namespace GuessTheNumber.Core.Statistics
{
    public class GameStatistics
    {
        private int _gamesPlayed;
        private int _totalAttempts;
        private int _minAttempts = int.MaxValue;
        private int _maxAttempts;
        public void Register(GameRoundResult result)
        {
            if (!result.IsWon) return;

            _gamesPlayed++;
            _totalAttempts += result.Attempts;

            if (result.Attempts < _minAttempts)
                _minAttempts = result.Attempts;

            if (result.Attempts > _maxAttempts)
                _maxAttempts = result.Attempts;
        }

        public void PrintReport()
        {
            if (_gamesPlayed == 0)
            {
                Console.WriteLine("Нет завершенных игр.");
                return;
            }

            double average = (double)_totalAttempts / _gamesPlayed;
            Console.WriteLine($"Мин: {_minAttempts}, Макс: {_maxAttempts}, Среднее: {average:F2}");
        }
    }
}