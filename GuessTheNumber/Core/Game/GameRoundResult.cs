namespace GuessTheNumber.Core.Game
{
    public class GameRoundResult
    {
        public bool IsWon { get; set; }
        public int Attempts { get; set; }

        public GameRoundResult(bool isWon, int attempts)
        {
            IsWon = isWon;
            Attempts = attempts;
        }
    }
}