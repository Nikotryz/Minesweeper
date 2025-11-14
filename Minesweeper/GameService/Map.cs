using Minesweeper.Cells;
using System.Text;

namespace Minesweeper.GameService
{
    /// <summary>
    /// Хранит данные о игровом поле и позволяет с ним взаимодействовать.
    /// </summary>
    public class Map
    {
        private Cell[,] map;
        private readonly int rows;
        private readonly int columns;

        public Map(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            map = new Cell[rows, columns];

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    map[row, column] = new EmptyCell();
                }
            }
        }

        public void OpenStartField(int startRow, int startColumn)
        {
            map[startRow, startColumn].Open();

            Random rnd = new Random();
            int radius = rnd.Next(1, 3);

            for (int row = startRow - radius; row <= startRow + radius; row++)
            {
                for (int column = startColumn - radius; column <= startColumn + radius; column++)
                {
                    if (!InBounds(row, column))
                        continue;
                    if (rnd.NextDouble() < 0.8)
                        map[row, column].Open();
                }
            }
        }

        public void PlaceMines(int mineCount)
        {
            Random rnd = new Random();
            int placedMines = 0;

            while (placedMines < mineCount)
            {
                int row = rnd.Next(rows);
                int column = rnd.Next(columns);

                if (map[row, column].IsOpened())
                    continue;

                if (Is<EmptyCell>(row, column))
                {
                    map[row, column] = new Mine();
                    placedMines++;
                }
            }
        }

        public void CalculateNumbersOfMines()
        {
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (Is<EmptyCell>(row, column))
                    {
                        var cell = map[row, column] as EmptyCell;

                        int minesAround = GetMinesAround(row, column);

                        cell!.SetCountOfMines(minesAround);
                    }
                }
            }
        }

        private int GetMinesAround(int row, int column)
        {
            int minesAround = 0;

            var directions = new (int, int)[]
            {
                (-1, -1), (-1, 0), (-1, 1),
                (0, -1), (0, 1),
                (1, -1), (1, 0), (1, 1)
            };

            foreach (var (drow, dcolumn) in directions)
            {
                int nrow = row + drow;
                int ncolumn = column + dcolumn;

                if (InBounds(nrow, ncolumn) && Is<Mine>(nrow, ncolumn))
                    minesAround++;
            }

            return minesAround;
        }

        public string GetMapView()
        {
            StringBuilder printableMap = new StringBuilder();
            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (column != columns - 1)
                        printableMap.Append($"{map[row, column].GetView()} ");
                    else
                        printableMap.Append(map[row, column].GetView());
                }
                printableMap.Append('\n');
            }
            return printableMap.ToString();
        }

        public List<int[]> GetClosedCells()
        {
            List<int[]> closedCells = [];

            for (int row = 0; row < rows; row++)
            {
                for (int column = 0; column < columns; column++)
                {
                    if (!map[row, column].IsOpened())
                    {
                        int[] cords = { column, row };
                        closedCells.Add(cords);
                    }
                }
            }

            return closedCells;
        }

        public Cell GetCell(int row, int column) => map[row, column];

        public bool Is<T>(int row, int column) where T : Cell => map[row, column] is T;

        public void OpenCell(int row, int column)
        {
            if (InBounds(row, column) && !map[row, column].IsFlagged())
                map[row, column].Open();
        }

        public void FlagCell(int row, int column)
        {
            if (InBounds(row, column) && !map[row, column].IsOpened())
                map[row, column].Flag();
        }

        private bool InBounds(int row, int column) => 
            row >= map.GetLowerBound(0) && row <= map.GetUpperBound(0) &&
            column >= map.GetLowerBound(1) && column <= map.GetUpperBound(1);
    }
}
