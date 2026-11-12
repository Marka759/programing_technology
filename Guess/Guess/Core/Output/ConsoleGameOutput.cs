using GuessTheNumber.Core.Game;

namespace GuessTheNumber.Core.Output
{
    public class ConsoleGameOutput : IGameOutput
    {
        public void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }
        public void ShowHint(ComparisonResult result)
        {
            switch (result)
            {
                case ComparisonResult.TooLow:
                    Console.WriteLine("Ваше число меньше");
                    break;
                case ComparisonResult.TooHigh:
                    Console.WriteLine("Ваше число больше");
                    break;
            }
        }
    }
}