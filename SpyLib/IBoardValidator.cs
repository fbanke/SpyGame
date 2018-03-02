using System.Diagnostics;

namespace SpyLib
{
    public interface IBoardValidator
    {
        bool IsValid(int[] board, int n);
        Stopwatch GetStopwatch();
    }
}
