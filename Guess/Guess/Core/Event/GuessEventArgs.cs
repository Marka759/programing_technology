using GuessTheNumber.Core.Game;

namespace GuessTheNumber.Core.Event;
public class GuessEventArgs : EventArgs
{
    public int Guess { get; }
    public int Number { get; }
    public ComparisonResult Result { get; }
    public GuessEventArgs(int guess, int number, ComparisonResult result)
    {
        Guess = guess;
        Number = number;
        Result = result;
    }
}
