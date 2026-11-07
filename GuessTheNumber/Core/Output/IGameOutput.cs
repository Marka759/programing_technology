using GuessTheNumber.Core.Game;

namespace GuessTheNumber.Core.Output;

public interface IGameOutput
{
    void ShowMessage(string message);
    void ShowHint(ComparisonResult result);
}
