namespace GuessTheNumber.Core.Settings
{
    public class GameSettings
    {
        public int Min { get; }
        public int Max { get; }
        public int MaxInputAttempts { get; }
        public GameSettings(int min, int max, int maxInputAttempts)
        {
            if (min >= max)
            {
                throw new ArgumentException("Минимальное число должно быть меньше максимального!");
            }

            Min = min;
            Max = max;
            MaxInputAttempts = maxInputAttempts;
        }
    }
}