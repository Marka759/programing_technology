using GuessTheNumber.Core.Game;

namespace GuessTheNumber.Core.Output;

internal class ConsoleGameOutput : IGameOutput
{
    public void ShowHint(ComparisonResult result)
    {
        switch (result)
        {
            case ComparisonResult.TooLow:
                Console.WriteLine("Your number is less");
                break;
            case ComparisonResult.TooHigh:
                Console.WriteLine("Your number is greater");
                break;
            default:
                Console.WriteLine("");
                break;
        }

    }

    public void ShowMessage(string message)
        => Console.WriteLine(message);
}
