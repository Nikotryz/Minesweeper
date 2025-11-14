namespace Minesweeper.Difficulties
{
    public class HardDifficulty : IDifficulty
    {
        private readonly string _name = Names.HARD_DIFFICULTY;
        public string Name { get => _name; }

        public float GetBombsRatio() => 0.4f;

        public int GetMapHeight() => 30;

        public int GetMapWidth() => 30;
    }
}
