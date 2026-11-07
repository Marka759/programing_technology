using GuessTheNumber.Core.Input;
using GuessTheNumber.Core.Output;
using GuessTheNumber.Core.Settings;
using System.Security;

namespace GuessTheNumber.Core.Game;

public class GameEngine
{
    private readonly GameSettings _settings;
    private readonly IRandomNumberGenerator _generator;
    private readonly IGuessComparer _comparer;
    private readonly IInputReader _input;
    private readonly IGameOutput _output;

    public GameEngine(
        GameSettings settings,
        IRandomNumberGenerator generator,
        IGuessComparer comparer ,
        IInputReader input,
        IGameOutput output )
        {
            _settings = settings;
            _generator = generator;
            _comparer = comparer;
            _input = input;
            _output = output;
        }

    public (bool Win, int Attempts) PlayRound()
    {
        int random = _generator.Next(_settings);
        int attempts = 0;

        _output.ShowMessage($"Input number from [{_settings.Min};{_settings.Max}]");

        while (true)
        {

            if (!_input.ReadNumber(_settings, out int guess))
            {
                _output.ShowMessage("No input attempts left.");
                return (false, attempts);
            }
            attempts++;

            var result = _comparer.Compare(guess, random);
            if (result == ComparisonResult.Correct)
            {
                _output.ShowMessage($"You won in {attempts} attempts!");
                return (true, attempts);
            }

            _output.ShowHint(result);
        }

    }

}
