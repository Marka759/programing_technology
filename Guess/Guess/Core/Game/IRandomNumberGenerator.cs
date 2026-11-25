using GuessTheNumber.Core.Settings;

namespace GuessTheNumber.Core.Game;
//DIP
public interface IRandomNumberGenerator
{
    int Next(GameSettings settings);
}
