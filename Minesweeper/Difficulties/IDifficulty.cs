namespace Minesweeper.Difficulties
{
    public interface IDifficulty
    {
        string Name { get; }
        float GetBombsRatio();
        int GetMapHeight();
        int GetMapWidth();
    }
}
