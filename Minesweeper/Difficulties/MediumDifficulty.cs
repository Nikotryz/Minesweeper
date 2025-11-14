namespace Minesweeper.Difficulties
{
    public class MediumDifficulty : IDifficulty
    {
        private readonly string _name = Names.MEDIUM_DIFFICULTY;
        public string Name { get => _name; }

        public float GetMinesRatio() => 0.3f;

        public int GetMapHeight() => 20;

        public int GetMapWidth() => 20;
    }
}
