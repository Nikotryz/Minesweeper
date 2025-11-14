namespace Minesweeper.Cells
{
    public class EmptyCell : Cell
    {
        private int countOfMines = 0;
        private Dictionary<int, CountColors> countColors = new Dictionary<int, CountColors>()
        {
            {1, CountColors.blue},
            {2, CountColors.green},
            {3, CountColors.red},
            {4, CountColors.darkblue},
            {5, CountColors.darkorange3_1},
            {6, CountColors.turquoise2},
            {7, CountColors.black},
            {8, CountColors.white}
        };

        public EmptyCell(int countOfMines = 0) => this.countOfMines = countOfMines;

        public int GetCountOfMines() => countOfMines;

        public int SetCountOfMines(int countOfMines) => this.countOfMines = countOfMines;

        public override string ToString()
        {
            return $"EmptyCell({countOfMines})";
        }

        public override string GetView()
        {
            string view = base.GetView();

            if (view != string.Empty)
                return view;

            if (countOfMines == 0)
                return " ";

            return $"[{countColors[countOfMines]} on grey35]{countOfMines}[/]";
        }
    }

    public enum CountColors
    {
        blue, green, red, darkblue, darkorange3_1, turquoise2, black, white
    }
}
