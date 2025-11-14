namespace Minesweeper
{
    /// <summary>
    /// Хранит координаты некоторой точки на поле в виде X, Y.
    /// Позволяет задавать максимальные значения для точки, чтобы она не выходила за пределы поля.
    /// </summary>
    public class Point
    {
        private int x = 0;
        private int y = 0;

        private int xMaxValue = 0;
        private int yMaxValue = 0;

        public int XMaxValue { get => xMaxValue; set => xMaxValue = value; }
        public int YMaxValue { get => yMaxValue; set => yMaxValue = value; }

        public int X { get => x; set { if (value >= 0 && value <= xMaxValue) x = value; } }

        public int Y { get => y; set { if (value >= 0 && value <= yMaxValue) y = value; } }

        public Point(int x, int y, int xMaxValue, int yMaxValue)
        {
            this.x = x;
            this.y = y;
            this.xMaxValue = xMaxValue;
            this.yMaxValue = yMaxValue;
        }
    }
}
