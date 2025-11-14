using Minesweeper.Cells;
using Minesweeper.Difficulties;
using Spectre.Console;
using System.Runtime.InteropServices;

namespace Minesweeper.GameService
{
    /// <summary>
    /// Представляет саму игру.
    /// </summary>
    public class Game
    {
        private readonly IDifficulty difficulty;
        private readonly Map map;
        private readonly int mapWidth;
        private readonly int mapHeight;

        private int timer = 0;
        private int movesCount = 0;
        private bool isGameRunning = false;

        private Point realCursorPos;
        private Point visibleCursorPos;

        public Game(IDifficulty difficulty) 
        {
            this.difficulty = difficulty;

            mapWidth = difficulty.GetMapWidth();
            mapHeight = difficulty.GetMapHeight();

            map = new Map(
                rows: mapHeight,
                columns: mapWidth
            );

            realCursorPos = new Point(
                x: 0, 
                y: 0, 
                xMaxValue: mapWidth - 1, 
                yMaxValue: mapHeight - 1
            );

            visibleCursorPos = new Point(
                x: 0,
                y: 0,
                xMaxValue: mapWidth * 2 - 2,
                yMaxValue: mapHeight - 1
            );
        }

        private void ConfigureMap()
        {
            int minesCount = (int)(mapWidth * mapHeight * difficulty.GetMinesRatio());

            map.OpenStartField(startRow: realCursorPos.Y, startColumn: realCursorPos.X);

            map.PlaceMines(minesCount);

            map.CalculateNumbersOfMines();
        }

        private void OpenMap()
        {
            List<int[]> closedCells = map.GetClosedCells();

            Random.Shared.Shuffle(CollectionsMarshal.AsSpan(closedCells));

            int openingDelay = 1000 / (mapHeight * mapWidth);

            foreach (int[] cords in closedCells)
            {
                int x = cords[0];
                int y = cords[1];

                map.OpenCell(row: y, column: x);

                Console.SetCursorPosition(x*2, y + 2);
                AnsiConsole.Markup($"[blue on grey35]{map.GetCell(row: y, column: x).GetView()}[/]");

                Thread.Sleep(openingDelay);
            }
        }

        private void HandleInput(ConsoleKeyInfo pressedKey)
        {
            switch (pressedKey.Key)
            {
                case ConsoleKey.W: MoveCursor(moveY: -1); break;
                case ConsoleKey.A: MoveCursor(moveX: -1); break;
                case ConsoleKey.S: MoveCursor(moveY: 1); break;
                case ConsoleKey.D: MoveCursor(moveX: 1); break;
                case ConsoleKey.Enter: OpenCell(); break;
                case ConsoleKey.F: FlagCell(); break;
                case ConsoleKey.Escape: StopGame(); break;
            }
        }

        private void MoveCursor(int moveX = 0, int moveY = 0)
        {
            ClearCursor();

            realCursorPos.X += moveX;
            realCursorPos.Y += moveY;
            visibleCursorPos.X += moveX * 2;
            visibleCursorPos.Y += moveY;

            PrintCursor();
        }

        private void OpenCell()
        {
            if (movesCount == 0)
            {
                ConfigureMap();
                AnsiConsole.Clear();
                PrintFrame();
            }
            if (!GetCurrentCell().IsFlagged() && map.Is<Mine>(row: realCursorPos.Y, column: realCursorPos.X))
            {
                isGameRunning = false;
                OpenMap();
                Console.SetCursorPosition(0, mapHeight + 4);
                AnsiConsole.Markup("Поражение! Вы открыли ячейку с бомбой.\n");
            }
            map.OpenCell(row: realCursorPos.Y, column: realCursorPos.X);
            if (GetCurrentCell().IsOpened())
                movesCount++;
        }

        private Cell GetCurrentCell() => map.GetCell(row: realCursorPos.Y, column: realCursorPos.X);

        private void FlagCell()
        {
            if (movesCount == 0)
                return;

            map.FlagCell(row: realCursorPos.Y, column: realCursorPos.X);
        }

        private void PrintFrame()
        {
            PrintInfo();
            AnsiConsole.Markup("\n\n");
            string printableMap = map.GetMapView();
            AnsiConsole.Markup($"[white on grey35]{printableMap}[/]");
            PrintCursor();
        }

        private void PrintInfo()
        {
            Console.SetCursorPosition(0, 0);
            AnsiConsole.Markup($"[white on grey35]Время: {timer} - [/]");
            AnsiConsole.Markup($"[white on grey35]Уровень сложности: {difficulty.Name} - [/]");
            AnsiConsole.Markup($"[white on grey35]Ходов: {movesCount}[/]");
        }

        private void PrintCursor(int cursorYOffset = 2)
        {
            Console.SetCursorPosition(visibleCursorPos.X, visibleCursorPos.Y + cursorYOffset);
            AnsiConsole.Markup($"[blue on grey35]{GetCurrentCell().GetView()}[/]");
        }

        private void ClearCursor(int cursorYOffset = 2)
        {
            Console.SetCursorPosition(visibleCursorPos.X, visibleCursorPos.Y + cursorYOffset);
            AnsiConsole.Markup($"[white on grey35]{GetCurrentCell().GetView()}[/]");
        }

        private void StartTimer()
        {
            var thread = new Thread(() =>
            {
                while (isGameRunning)
                {
                    timer++;
                    PrintInfo();
                    Thread.Sleep(1000);
                }
            });
            thread.Start();
        }

        public void StartGame()
        {
            var thread = new Thread(() =>
            {
                isGameRunning = true;
                AnsiConsole.Clear();
                PrintFrame();
                StartTimer();
                while (isGameRunning)
                {
                    PrintCursor();
                    ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                    HandleInput(pressedKey);
                }
            });
            thread.Start();
        }

        public void StopGame()
        {
            isGameRunning = false;
            AnsiConsole.Clear();
        }
    }
}
