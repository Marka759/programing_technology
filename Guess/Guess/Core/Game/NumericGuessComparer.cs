namespace GuessTheNumber.Core.Game;
//SRP
public class NumericGuessComparer : IGuessComparer
{
    public ComparisonResult Compare(int guess, int random)
        => guess > random ? ComparisonResult.TooHigh
        : guess < random ? ComparisonResult.TooLow
        : ComparisonResult.Correct;
}
