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
            GameSettings settings = new GameSettings(1, 100, 3);
            IGameOutput output = new ConsoleGameOutput();
            IInputReader input = new ConsoleInputReader();

            IRandomNumberGenerator generator = new RandomSecretNumberGenerator();
            IGuessComparer comparer = new NumericGuessComparer();

            GameEngine engine = new GameEngine(settings, generator, comparer, input, output);
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

            stats.PrintReport();
        }
    }
}