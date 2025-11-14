namespace Minesweeper.Difficulties
{
    public class CustomDifficulty : IDifficulty
    {
        private readonly int height;
        private readonly int width;
        private readonly float bombsRatio;

        public string Name => Names.CUSTOM_DIFFICULTY;

        public CustomDifficulty(int height, int width, float bombsRatio)
        {
            this.height = height;
            this.width = width;
            this.bombsRatio = bombsRatio;
        }

        public float GetBombsRatio() => bombsRatio;

        public int GetMapHeight() => height;

        public int GetMapWidth() => width;
    }
}
