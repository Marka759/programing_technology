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
            var settings = new GameSettings(Min: 1, Max: 100, MaxInputAttempts: 3);

            var output = new ConsoleGameOutput();

            var engine = new GameEngine(
                settings,
                new RandomSecretNumberGenerator(),
                new NumericGuessComparer(),
                new ConsoleInputReader(),
                output);

            var statistics = new GameStatistics();

            string answer;
            do
            {

                var (isWon, attempts) = engine.PlayRound();

                statistics.Register(isWon, attempts);

                output.ShowMessage("Do you want to play again? (Y/N)");


                answer = (Console.ReadLine() ?? "").ToUpper();
            }
            while (answer == "Y");

            output.ShowMessage(statistics.ToReport());
        }
    }
}

