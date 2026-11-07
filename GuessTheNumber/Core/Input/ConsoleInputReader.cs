using GuessTheNumber.Core.Settings;
using System.Threading.Channels;

namespace GuessTheNumber.Core.Input
{
    public class ConsoleInputReader : IInputReader
    {
        public bool ReadNumber(GameSettings settings, out int value)
        {
            for (int i = 0; i<settings.MaxInputAttempts; i++)
            {
                var raw = Console.ReadLine();
                if (int.TryParse(raw, out value) && value>=settings.Min && value <= settings.Max)
                    { return true; }
                Console.WriteLine($"Input number between {settings.Min} and {settings.Max}");
            }

            value = 0;
            return false;   
        }
    }
}
