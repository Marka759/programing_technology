using GuessTheNumber.Core.Settings;

namespace GuessTheNumber.Core.Game;

public class RandomSecretNumberGenerator : IRandomNumberGenerator
{
    private readonly Random _random = new();
    public int Next(GameSettings settings)
        => _random.Next(settings.Min, settings.Max+1);
}
