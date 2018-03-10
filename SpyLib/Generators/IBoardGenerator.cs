using System.Collections.Generic;

namespace SpyLib
{
    public interface IBoardGenerator
    {
        IEnumerable<Board> Generate(IBoardValidator validator, int n);
    }
}