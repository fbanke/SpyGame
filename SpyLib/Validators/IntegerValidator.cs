using System;

namespace SpyLib
{
    public class IntegerValidator : SmartValidator
    {
        public bool IsInDiagonal(int[] board, int n)
        {
            // https://math.stackexchange.com/questions/1194565/how-to-know-if-two-points-are-diagonally-aligned
            var x = n;
            var y = board[x - 1]; // only look at the newest point - the rest is already validated
            // (x, y) - coordinate is (i, pos)
            for (var x2 = 1; x2 < n; x2++)
            {
                var y2 = board[x2 - 1];
                if (Math.Abs(x - x2) == Math.Abs(y - y2))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsOnLine(int[] board, int n)
        {
            // http://mathworld.wolfram.com/Collinear.html
            var x = n;
            var y = board[x - 1];

            // from first point to the second last point 
            for (var x2 = 1; x2 < n; x2++)
            {
                var y2 = board[x2 - 1];
                for (var x3 = x2 + 1; x3 < n; x3++)
                {
                    var y3 = board[x3 - 1];
                    if (x * (y2 - y3) + x2 * (y3 - y) + x3 * (y - y2) == 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
