using GuessTheNumber.Core.Game;

namespace GuessTheNumber.Core.Event;

public class GuessEventHandler
{
    public void OnGuessMade(object? sender, GuessEventArgs e)
    {
        switch (e.Result)
        {
            case ComparisonResult.TooLow:
                Console.WriteLine("Ваше число меньше");
                break;
            case ComparisonResult.TooHigh:
                Console.WriteLine("Ваше число больше");
                break;
            case ComparisonResult.Correct:

                break;
        }
    }
}
