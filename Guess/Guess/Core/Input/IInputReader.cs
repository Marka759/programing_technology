using GuessTheNumber.Core.Settings;

namespace GuessTheNumber.Core.Input;

public interface IInputReader
{
    bool ReadNumber(GameSettings settings, out int value);
}
