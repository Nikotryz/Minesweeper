using System.Text;

namespace Minesweeper.Menu
{
    public class GameMenu
    {
        //private MenuItem menu;
        private MenuItem currentItem;

        //public MenuItem GetMenu() => menu;

        public GameMenu() 
        {
            var menu = new DropDownItem("Главное меню");
            menu.OnClick = () => SetCurrentItem(menu);

            var play = new DropDownItem("Играть");
            play.OnClick = () => SetCurrentItem(play);

            var settings = new DropDownItem("Настройки");
            settings.OnClick = () => SetCurrentItem(settings);

            var exit = new ClickableItem("Выйти из игры");

            menu.Add(play);
            menu.Add(settings);
            menu.Add(exit);

            var easy = new ClickableItem("Легкий");
            var medium = new ClickableItem("Средний");
            var hard = new ClickableItem("Тяжелый");
            var custom = new ClickableItem("Пользовательский");

            play.Add(easy);
            play.Add(medium);
            play.Add(hard);
            play.Add(custom);

            var controls = new ClickableItem("Управление");
            var visual = new ClickableItem("Изменить визуальные элементы");
            var themes = new ClickableItem("Темы");

            settings.Add(controls);
            settings.Add(visual);
            settings.Add(themes);
        }

        public string PrintMenu(List<MenuItem> items)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in items)
            {
                sb.Append($"{item.Display()}\n");
            }
            return sb.ToString();
        }

        public void SetCurrentItem(MenuItem item)
        {
            currentItem = item;
        }
    }
}
