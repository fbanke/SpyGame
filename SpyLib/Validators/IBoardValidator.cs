namespace SpyLib
{
    public interface IBoardValidator
    {
        bool IsValid(Board board);
        string GetDebug();
    }
}
