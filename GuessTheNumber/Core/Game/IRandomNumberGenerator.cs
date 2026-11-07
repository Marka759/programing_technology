using GuessTheNumber.Core.Settings;

namespace GuessTheNumber.Core.Game;

public interface IRandomNumberGenerator
{
    int Next(GameSettings settings);
}
