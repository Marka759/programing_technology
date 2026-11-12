namespace GuessTheNumber.Core.Game;

public interface IGuessComparer
{
    ComparisonResult Compare(int guess, int random);
}
