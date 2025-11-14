namespace Minesweeper.Difficulties
{
    public interface IDifficulty
    {
        string Name { get; }
        float GetMinesRatio();
        int GetMapHeight();
        int GetMapWidth();
    }
}
