using GuessTheNumber.Core.Event;
using GuessTheNumber.Core.Input;
using GuessTheNumber.Core.Output;
using GuessTheNumber.Core.Settings;

namespace GuessTheNumber.Core.Game
{
    public class GameEngine
    {
        private readonly GameSettings _settings;
        private readonly IRandomNumberGenerator _generator;
        private readonly IGuessComparer _comparer;
        private readonly IInputReader _input;
        private readonly IGameOutput _output;
        public event EventHandler<GuessEventArgs>? GuessMade;

        public GameEngine(
            GameSettings settings,
            IRandomNumberGenerator generator,
            IGuessComparer comparer,
            IInputReader input,
            IGameOutput output)
        {
            _settings = settings;
            _generator = generator;
            _comparer = comparer;
            _input = input;
            _output = output;
        }


        public GameRoundResult PlayRound()
        {
            int secretNumber = _generator.Next(_settings);
            int attempts = 0;
            const int MaxGuesses = 3;

            while (true)
            {
 
                if (!_input.ReadNumber(_settings, out int guess))
                {
                    _output.ShowMessage("Попытки ввода закончились (ошибки при наборе).");
                    return new GameRoundResult(false, attempts);
                }
                attempts++;

                ComparisonResult result = _comparer.Compare(guess, secretNumber);

                OnGuessMade(new GuessEventArgs(guess, secretNumber, result));

                if (result == ComparisonResult.Correct)
                {
                    _output.ShowMessage($"Вы угадали за {attempts} попыток!");
                    return new GameRoundResult(true, attempts);
                }

                if (attempts >= MaxGuesses)
                {
                    _output.ShowMessage($"Вы проиграли! Загаданное число было: {secretNumber}");
                    return new GameRoundResult(false, attempts);
                }

                //_output.ShowHint(result);
            }
        }
        protected virtual void OnGuessMade(GuessEventArgs e)
        {
            GuessMade?.Invoke(this, e);
        }


    }
}