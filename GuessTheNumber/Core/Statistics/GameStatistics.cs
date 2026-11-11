using GuessTheNumber.Core.Game;

namespace GuessTheNumber.Core.Statistics
{
    public class GameStatistics
    {
        public int GamesPlayed { get; private set; }
        public int TotalAttempts { get; private set; }
        public int MinAttempts { get; private set; } = int.MaxValue;
        public int MaxAttempts { get; private set; }
        public int Wins { get; private set; }
        public int Losses { get; private set; }

        public void Register(GameRoundResult result)
        {
            GamesPlayed++;
            if (!result.IsWon)
            {
                Wins++;
                TotalAttempts += result.Attempts;

                if (result.Attempts < MinAttempts)
                    MinAttempts = result.Attempts;

                if (result.Attempts > MaxAttempts)
                    MaxAttempts = result.Attempts;

            }
            else { Losses++; }

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