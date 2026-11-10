using GuessTheNumber.Core.Game;

namespace GuessTheNumber.Core.Statistics
{
    public class GameStatistics
    {
        private int _gamesPlayed;
        private int _totalAttempts;
        private int _minAttempts = int.MaxValue;
        private int _maxAttempts;
        private int _wins;
        private int _losses;

        public void Register(GameRoundResult result)
        {
            _gamesPlayed++;
            if (!result.IsWon)
            {
                _wins++;
                _totalAttempts += result.Attempts;

                if (result.Attempts < _minAttempts)
                    _minAttempts = result.Attempts;

                if (result.Attempts > _maxAttempts)
                    _maxAttempts = result.Attempts;

            }
            else { _losses++; }

        }

        public void PrintReport()
        {
            Console.WriteLine("\n Статистика игр ");
            Console.WriteLine($"Всего игр: {_gamesPlayed}");
            Console.WriteLine($"Побед: {_wins}");
            Console.WriteLine($"Поражений: {_losses}");

            if (_wins > 0)
            {
                double average = (double)_totalAttempts / _wins;
                Console.WriteLine($"Мин. попыток: {_minAttempts}");
                Console.WriteLine($"Макс. попыток: {_maxAttempts}");
                Console.WriteLine($"Среднее кол-во попыток: {average:F2}");
            }
            else
            {
                Console.WriteLine("Успешных игр пока нет.");
            }
        }
    }
        
}