using GuessTheNumber.Core.Settings;

namespace GuessTheNumber.Core.Input
{
    public class ConsoleInputReader : IInputReader
    {
        public bool ReadNumber(GameSettings settings, out int value)
        {
            Console.Write($"Введите число от {settings.Min} до {settings.Max}: ");
            string raw = Console.ReadLine();

            if (int.TryParse(raw, out value) && value >= settings.Min && value <= settings.Max)
            {
                return true;
            }

            value = 0;
            return false;
        }
    }
}