using GuessTheNumber.Core.Event;
using GuessTheNumber.Core.Game;
using GuessTheNumber.Core.Input;
using GuessTheNumber.Core.Output;
using GuessTheNumber.Core.Settings;
using GuessTheNumber.Core.Statistics;

namespace GuessTheNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IGameOutput output = new ConsoleGameOutput();
            output.ShowMessage("Выберите сложность:");
            output.ShowMessage("1 - Легкий");
            output.ShowMessage("2 - Средний (по умолчанию)");
            output.ShowMessage("3 - Сложный");
            output.ShowMessage("4 - Свои значения");
            Console.Write("Ваш выбор: ");


            string choice = Console.ReadLine();
            Difficulty difficulty = Difficulty.Medium;
            GameSettings settings;


            if (choice == "1")
            {
                difficulty = Difficulty.Easy;
                settings = SettingValue.GetSettings(difficulty);
            }
            else if (choice == "3")
            {
                difficulty = Difficulty.Hard;
                settings = SettingValue.GetSettings(difficulty);
            }
            else if (choice == "4")
            {
                output.ShowMessage("Введите минимальное число:");
                int min = int.Parse(Console.ReadLine());

                output.ShowMessage("Введите максимальное число:");
                int max = int.Parse(Console.ReadLine());

                output.ShowMessage("Введите количество попыток:");
                int att = int.Parse(Console.ReadLine());

                settings = new GameSettings(min, max,att);
            }
            else
            {
                output.ShowMessage("Выбран средний режим сложности по умолчанию");
                difficulty = Difficulty.Medium;
                settings = SettingValue.GetSettings(difficulty);
            }
            IInputReader input = new ConsoleInputReader();

            IRandomNumberGenerator generator = new RandomSecretNumberGenerator();
            IGuessComparer comparer = new NumericGuessComparer();

            GameEngine engine = new GameEngine(settings, generator, comparer, input, output);
            GuessEventHandler eventHandler = new GuessEventHandler();
            engine.GuessMade += eventHandler.OnGuessMade;
            GameStatistics stats = new GameStatistics();

            string answer;
            do
            {
                GameRoundResult result = engine.PlayRound();
                stats.Register(result);

                output.ShowMessage("Хотите сыграть еще? (Y/N)");
                answer = Console.ReadLine();

                if (answer != null)
                {
                    answer = answer.Trim().ToUpper();
                }
            }
            while (answer == "Y");

            engine.GuessMade -= eventHandler.OnGuessMade;

            stats.PrintReport();

            IStatisticsSave file = new SafeFileStatistics("statistics.txt");
            file.Save(stats);

            output.ShowMessage("Статистика успешно сохранена в файл statistics.txt!");
            Console.ReadKey();
        }
    }
}