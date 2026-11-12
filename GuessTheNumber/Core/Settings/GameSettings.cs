namespace GuessTheNumber.Core.Settings
{
    public class GameSettings
    {
        public int Min { get; }
        public int Max { get; }
        public int GuessesLeft { get; }
        public GameSettings(int min, int max, int guessesLeft)
        {
            if (min >= max)
            {
                throw new ArgumentException("Минимальное число должно быть меньше максимального!");
            }

            if (guessesLeft <= 0)
                throw new ArgumentOutOfRangeException(nameof(guessesLeft));

            Min = min;
            Max = max;
            GuessesLeft = guessesLeft;
        }
    }
}