using GuessTheNumber.Core.Settings;

namespace GuessTheNumber.Core.Input;
//ISP
public interface IInputReader
{
    bool ReadNumber(GameSettings settings, out int value);
}
