namespace Minesweeper.Difficulties
{
    public class EasyDifficulty : IDifficulty
    {
        private readonly string _name = Names.EASY_DIFFICULTY;
        public string Name { get => _name; }

        public float GetBombsRatio() => 0.2f;

        public int GetMapHeight() => 10;

        public int GetMapWidth() => 10;
    }
}
