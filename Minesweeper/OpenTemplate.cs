namespace Minesweeper
{
    /// <summary>
    /// Представляет шаблон для открытия клеток при генерации поля.
    /// Позволяет обеспечить случайное открытие ячеек в начале игры.
    /// </summary>
    public class OpenTemplate
    {
        private readonly int height;
        private readonly int width;
        public int Height { get => height; }
        public int Width { get => width; }

        public OpenTemplate(int height, int width)
        {
            this.height = height;
            this.width = width;
        }
    }
}
