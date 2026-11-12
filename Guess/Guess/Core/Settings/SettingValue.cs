namespace GuessTheNumber.Core.Settings;
public class SettingValue
{
    public static GameSettings GetSettings(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                return new GameSettings(min: 1, max: 10, guessesLeft: 5);
            case Difficulty.Medium:
                return new GameSettings(min: 1, max: 100, guessesLeft: 12);
            case Difficulty.Hard:
                return new GameSettings(min: 1, max: 1000, guessesLeft: 25);
            default:
                return new GameSettings(1, 100, 12);
        }
    } 
}

