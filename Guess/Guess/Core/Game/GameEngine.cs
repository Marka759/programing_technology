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
            int inputErrors = 0;

            _output.ShowMessage($"Загадано число от {_settings.Min} до {_settings.Max}. У вас {_settings.GuessesLeft} попыток.");

            while (true)
            {
                if (!_input.ReadNumber(_settings, out int guess))
                {
                    inputErrors++; 
                    _output.ShowMessage($"Некорректный ввод. Ошибок подряд: {inputErrors}/3");

                    if (inputErrors >= 3)
                    {
                        _output.ShowMessage("Вы 3 раза подряд ввели некорректные данные.");
                        return new GameRoundResult(false, attempts);
                    }

                    continue; 
                }
                inputErrors = 0;
                attempts++;

                ComparisonResult result = _comparer.Compare(guess, secretNumber);

                OnGuessMade(new GuessEventArgs(guess, secretNumber, result));

                if (result == ComparisonResult.Correct)
                {
                    _output.ShowMessage($"Вы угадали за {attempts} попыток!");
                    return new GameRoundResult(true, attempts);
                }

                if (attempts >= _settings.GuessesLeft)
                {
                    _output.ShowMessage($"Вы проиграли! Загаданное число было: {secretNumber}");
                    return new GameRoundResult(false, attempts);
                }

                _output.ShowMessage($"Осталось ходов: {_settings.GuessesLeft - attempts}");
            }
        }
        protected virtual void OnGuessMade(GuessEventArgs e)
        {
            GuessMade?.Invoke(this, e);
        }


    }
}