using GuessTheNumber.Core.Game;
//SRP
namespace GuessTheNumber.Core.Statistics
{
    public class GameStatistics
    {
        public int GamesPlayed { get; private set; }
        public int TotalAttempts { get; private set; }
        public int Wins { get; private set; }
        public int Losses { get; private set; }

        private int _minAttempts = int.MaxValue;
        private int _maxAttempts;

        public int? MinAttempts => Wins > 0 ? _minAttempts : null;
        public int? MaxAttempts => Wins > 0 ? _maxAttempts : null;

        public void Register(GameRoundResult result)
        {
            GamesPlayed++;
            if (result.IsWon)
            {
                Wins++;
                TotalAttempts += result.Attempts;

                if (result.Attempts < _minAttempts)
                    _minAttempts = result.Attempts;

                if (result.Attempts > _maxAttempts)
                    _maxAttempts = result.Attempts;
            }
            else
            {
                Losses++;
            }
        }

        public void Reset()
        {
            GamesPlayed = 0;
            TotalAttempts = 0;
            Wins = 0;
            Losses = 0;
            _minAttempts = int.MaxValue;
            _maxAttempts = 0;
        }

        public void Restore(int gamesPlayed, int wins, int losses, int totalAttempts,
                            int? minAttempts, int? maxAttempts)
        {
            GamesPlayed = gamesPlayed;
            Wins = wins;
            Losses = losses;
            TotalAttempts = totalAttempts;
            _minAttempts = minAttempts ?? int.MaxValue;
            _maxAttempts = maxAttempts ?? 0;
        }

        public void PrintReport()
        {
            Console.WriteLine("\n Статистика игр ");
            Console.WriteLine($"Всего игр: {GamesPlayed}");
            Console.WriteLine($"Побед: {Wins}");
            Console.WriteLine($"Поражений: {Losses}");

            if (Wins > 0)
            {
                double average = (double)TotalAttempts / Wins;
                Console.WriteLine($"Мин. попыток: {MinAttempts}");
                Console.WriteLine($"Макс. попыток: {MaxAttempts}");
                Console.WriteLine($"Среднее кол-во попыток: {average:F2}");
            }
            else
            {
                Console.WriteLine("Успешных игр пока нет.");
            }
        }
    }
}