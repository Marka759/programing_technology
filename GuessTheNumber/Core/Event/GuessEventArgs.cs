using GuessTheNumber.Core.Game;

namespace GuessTheNumber.Core.Event;
public class GuessEventArgs : EventArgs
{
    public int Guess { get; set; }
    public int Number { get; set; }
    public ComparisonResult Result { get; set; }
}
