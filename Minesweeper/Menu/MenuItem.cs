using System.Runtime.CompilerServices;

namespace Minesweeper.Menu
{
    public abstract class MenuItem
    {
        protected string name;
        protected MenuItem? parent;

        private Action onClick;

        public Action OnClick { get; set; }

        public MenuItem(string name, Action onClick = null)
        {
            this.name = name;
            this.onClick = onClick;
        }

        public void SetParent(MenuItem parent) => this.parent = parent;

        //public abstract void Add(MenuItem subItem);
        //public abstract void Remove(MenuItem subItem);

        public virtual string Display() => name;
    }
}
