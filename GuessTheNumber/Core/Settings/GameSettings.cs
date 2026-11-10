namespace GuessTheNumber.Core.Settings
{
    public class GameSettings
    {
        public int Min { get; }
        public int Max { get; }
        public int MaxInputAttempts { get; }
        public GameSettings(int min, int max, int maxInputAttempts)
        {
            Min = min;
            Max = max;
            MaxInputAttempts = maxInputAttempts;
        }
    }
}