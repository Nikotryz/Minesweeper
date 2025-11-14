namespace Minesweeper.Difficulties
{
    public class MediumDifficulty : IDifficulty
    {
        private readonly string _name = Names.MEDIUM_DIFFICULTY;
        public string Name { get => _name; }

        public float GetBombsRatio() => 0.3f;

        public int GetMapHeight() => 20;

        public int GetMapWidth() => 20;
    }
}
