using Minesweeper.Difficulties;
using Spectre.Console;
using System.Runtime.InteropServices;

namespace Minesweeper
{
    internal class Program
    {
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_MAXIMIZE = 3;

        static void MaximizeWindow()
        {
            IntPtr handle = GetConsoleWindow();
            ShowWindow(handle, SW_MAXIMIZE);
        }
        static void Main(string[] args)
        {
            //MaximizeWindow();
            Console.Title = "Сапер";

            string startMenu = $"""
            Выберите уровень сложности:
            [[1]] - {Names.EASY_DIFFICULTY}
            [[2]] - {Names.MEDIUM_DIFFICULTY}
            [[3]] - {Names.HARD_DIFFICULTY}
            [[4]] - {Names.CUSTOM_DIFFICULTY}
            """;

            AnsiConsole.MarkupLine($"[white]{startMenu}[/]");

            var pressedKey = Console.ReadKey(true);
            IDifficulty difficulty = new EasyDifficulty();

            switch (pressedKey.Key)
            {
                case ConsoleKey.D1: difficulty = new EasyDifficulty(); break;
                case ConsoleKey.D2: difficulty = new MediumDifficulty(); break;
                case ConsoleKey.D3: difficulty = new HardDifficulty(); break;
                case ConsoleKey.D4: difficulty = CreateCustomDifficulty(); break;
            }

            Game game = new Game(difficulty);
            game.StartGame();
        }

        static CustomDifficulty CreateCustomDifficulty()
        {
            AnsiConsole.Clear();

            AnsiConsole.Markup($"[white]Введите высоту поля: [/]");
            int height = Convert.ToInt32(Console.ReadLine());

            AnsiConsole.Markup($"[white]Введите ширину поля: [/]");
            int width = Convert.ToInt32(Console.ReadLine());

            AnsiConsole.Markup($"[white]Введите соотношение бомб (например: 0,5): [/]");
            float bombsRatio = (float)Convert.ToDouble(Console.ReadLine());

            return new CustomDifficulty(height, width, bombsRatio);
        }
    }
}
