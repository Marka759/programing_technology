using Guess.Core.Players;
using GuessTheNumber.Core.Event;
using GuessTheNumber.Core.Game;
using GuessTheNumber.Core.Input;
using GuessTheNumber.Core.Output;
using GuessTheNumber.Core.Settings;

namespace GuessTheNumber
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IGameOutput output = new ConsoleGameOutput();

            var storage = new PerPlayerStatisticsSave("statistics");
            IPlayerRepository players = new FilePlayerRepository(storage);
            IPlayerSession session = new PlayerSession();

            Player player = LoginOrRegister(output, players, session);

            while (true)
            {
                output.ShowMessage($"\nИгрок: {player.Username}");
                output.ShowMessage("1 - Играть");
                output.ShowMessage("2 - Показать мою статистику");
                output.ShowMessage("3 - Сбросить мою статистику");
                output.ShowMessage("0 - Выйти");
                Console.Write("Ваш выбор: ");

                string? menu = Console.ReadLine()?.Trim();
                if (menu == "0") break;

                if (menu == "2")
                {
                    player.Statistics.PrintReport();
                    continue;
                }

                if (menu == "3")
                {
                    ResetStatistics(output, player, storage);
                    continue;
                }

                if (menu == "1")
                    PlayGames(output, player, storage);
            }

            session.Logout();
            output.ShowMessage("До встречи!");
        }

        static void PlayGames(IGameOutput output, Player player, PerPlayerStatisticsSave storage)
        {
            GameSettings settings = ChooseSettings(output);

            IInputReader input = new ConsoleInputReader();
            IRandomNumberGenerator generator = new RandomSecretNumberGenerator();
            IGuessComparer comparer = new NumericGuessComparer();

            GameEngine engine = new GameEngine(settings, generator, comparer, input, output);
            GuessEventHandler eventHandler = new GuessEventHandler();
            engine.GuessMade += eventHandler.OnGuessMade;

            string answer;
            do
            {
                GameRoundResult result = engine.PlayRound();
                player.Statistics.Register(result);   

                output.ShowMessage("Хотите сыграть еще? (Y/N)");
                answer = Console.ReadLine()?.Trim().ToUpper() ?? "";
            }
            while (answer == "Y");

            engine.GuessMade -= eventHandler.OnGuessMade;

            player.Statistics.PrintReport();
            storage.SaveFor(player);   
        }

        static void ResetStatistics(
            IGameOutput output,
            Player player,
            PerPlayerStatisticsSave storage)
        {
            output.ShowMessage($"Сбросить статистику игрока '{player.Username}'? (Y/N)");
            string? confirm = Console.ReadLine()?.Trim().ToUpper();

            if (confirm != "Y")
            {
                output.ShowMessage("Сброс отменён.");
                return;
            }

            player.Statistics.Reset();  
            storage.DeleteFor(player);   
            output.ShowMessage("Статистика сброшена.");
        }

        static Player LoginOrRegister(
            IGameOutput output,
            IPlayerRepository players,
            IPlayerSession session)
        {
            while (true)
            {
                output.ShowMessage("Введите имя игрока (или 'exit' для выхода):");
                string? username = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(username))
                {
                    output.ShowMessage("Имя не может быть пустым.");
                    continue;
                }

                if (username.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    Environment.Exit(0);

                Player? existing = players.Find(username);
                if (existing != null)
                {
                    session.Login(existing);
                    output.ShowMessage($"С возвращением, {existing.Username}!");
                    return existing;
                }

                output.ShowMessage("Вас нет в списке. Хотите создать аккаунт? (Y/N)");
                string? answer = Console.ReadLine()?.Trim().ToUpper();

                if (answer == "Y")
                {
                    Player created = players.Create(username);
                    session.Login(created);
                    output.ShowMessage($"Аккаунт '{created.Username}' создан. Добро пожаловать!");
                    return created;
                }

                output.ShowMessage("Хорошо, попробуйте другое имя или 'exit'.");
            }
        }

        static GameSettings ChooseSettings(IGameOutput output)
        {
            output.ShowMessage("Выберите сложность:");
            output.ShowMessage("1 - Легкий");
            output.ShowMessage("2 - Средний (по умолчанию)");
            output.ShowMessage("3 - Сложный");
            output.ShowMessage("4 - Свои значения");
            Console.Write("Ваш выбор: ");

            string? choice = Console.ReadLine();

            if (choice == "1") return SettingValue.GetSettings(Difficulty.Easy);
            if (choice == "3") return SettingValue.GetSettings(Difficulty.Hard);
            if (choice == "4")
            {
                int min = ReadInt(output, "Введите минимальное число:");
                int max = ReadInt(output, "Введите максимальное число:");
                int att = ReadInt(output, "Введите количество попыток:");
                return new GameSettings(min, max, att);
            }

            output.ShowMessage("Выбран средний режим сложности по умолчанию");
            return SettingValue.GetSettings(Difficulty.Medium);
        }

        static int ReadInt(IGameOutput output, string prompt)
        {
            while (true)
            {
                output.ShowMessage(prompt);
                if (int.TryParse(Console.ReadLine(), out int value))
                    return value;
                output.ShowMessage("Некорректное число, попробуйте снова.");
            }
        }
    }
}