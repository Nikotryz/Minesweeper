namespace Minesweeper.Cells
{
    public class Mine : Cell
    {
        public override string ToString()
        {
            return "Bomb";
        }

        public override string GetView()
        {
            string view = base.GetView();

            if (view != string.Empty)
                return view;

            return $"[red]{VisualElements.BOMB_CELL}[/]";
        }
    }
}
