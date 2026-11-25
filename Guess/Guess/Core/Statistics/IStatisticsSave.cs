namespace GuessTheNumber.Core.Statistics
{//DIP,ISP
    public interface IStatisticsSave
    {
        void Save(GameStatistics stats);
    }
}