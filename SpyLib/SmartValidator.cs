using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpyLib
{
    public class SmartValidator : IBoardValidator
    {
        public bool IsValid(int[] board, int n)
        {
            if (IsInDiagonal(board, n))
            {
                return false;
            }

            if (IsOnLine(board, n))
            {
                return false;
            }
            
            return true;
        }

        public bool IsInDiagonal(int[] board, int n)
        {
            // test all (x,y) coordinates
            
            var y = board[n-1]; // only look at the newest point
            // (x, y) - coordinate is (i, pos)
            for (var x2 = n-1; x2 >= 1; x2--)
            {
                var y2 = board[x2-1];
                // solve equation for line between the two points
                double slope = (double)(y - y2) / (n - x2);
                // if slope is 1 they are on a diagonal
                if (slope == 1 || slope == -1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsOnLine(int[] board, int n)
        {
            // test all (x,y) coordinates

            var y = board[n - 1];
            // (x, y) - coordinate is (i, pos)
            for (var x2 = n-1; x2 >= 1; x2--)
            {
                var y2 = board[x2 - 1];
                // solve equation for line between the two points
                double a = (double) (y - y2) / (n - x2);
                double b = y - a * n;

                for (var x3 = x2-1; x3 >= 1; x3--)
                {
                    var y3 = board[x3 - 1];
                    // check if the third point are on the line 
                    if (y3 == a * x3 + b)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
