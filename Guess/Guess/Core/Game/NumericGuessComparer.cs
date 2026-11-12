namespace GuessTheNumber.Core.Game;

public class NumericGuessComparer : IGuessComparer
{
    public ComparisonResult Compare(int guess, int random)
        => guess > random ? ComparisonResult.TooHigh
        : guess < random ? ComparisonResult.TooLow
        : ComparisonResult.Correct;
}
