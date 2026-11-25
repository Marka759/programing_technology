namespace GuessTheNumber.Core.Game;
//DIP
public interface IGuessComparer
{
    ComparisonResult Compare(int guess, int random);
}
