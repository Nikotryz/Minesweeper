namespace Minesweeper.Menu
{
    public class ClickableItem : MenuItem
    {
        public ClickableItem(string name, Action onClick = null) : base(name, onClick) { }

        //public override void Add(MenuItem subItem)
        //{
        //    throw new NotImplementedException();
        //}

        //public override void Remove(MenuItem subItem)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
