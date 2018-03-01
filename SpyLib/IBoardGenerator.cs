using System.Linq;
using System.Collections.Generic;

namespace SpyLib
{
    public interface IBoardGenerator
    {
        IEnumerable<IEnumerable<int>> Generate(IBoardValidator validator, int n);
    }
}