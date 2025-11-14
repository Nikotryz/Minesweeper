namespace Minesweeper.Cells
{
    public abstract class Cell
    {
        protected bool isOpened = false;
        protected bool isFlagged = false;

        public bool IsOpened() => isOpened;

        public bool IsFlagged() => isFlagged;

        public void Open() => isOpened = true;

        public void Flag() => isFlagged = !isFlagged;

        public virtual string GetView()
        {
            if (isFlagged)
                return $"[red]{VisualElements.FLAG_CELL}[/]";

            if (!isOpened)
                return VisualElements.CLOSED_CELL;

            return string.Empty;
        }
    }
}
