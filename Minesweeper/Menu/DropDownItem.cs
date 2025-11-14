namespace Minesweeper.Menu
{
    public class DropDownItem : MenuItem
    {
        private List<MenuItem> children = new List<MenuItem>();

        public DropDownItem(string name, Action onClick = null) : base(name, onClick) { }

        public void Add(MenuItem subItem)
        {
            subItem.SetParent(this);
            children.Add(subItem);
        }

        public void Remove(MenuItem subItem)
        {
            children.Remove(subItem);
        }

        public List<MenuItem> GetChildren() => children;
    }
}
