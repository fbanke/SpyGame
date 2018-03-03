using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpyLib
{
    public class CachingValidator : IBoardValidator
    {
        public CachingValidator()
        {
            sw = new Stopwatch();            
        }
        public Stopwatch GetStopwatch()
        {
            return sw;
        }
        private Stopwatch sw;
        
        public bool IsValid(int[] board, int n)
        {
            sw.Start();
            if (IsInDiagonal(board, n))
            {
                sw.Stop();
                return false;
            }

            if (IsOnLine(board, n))
            {
                sw.Stop();
                return false;
            }
            sw.Stop();
            return true;
        }
       
        private int[, , ,] cacheDiagonal = new int[35,35,35,35];

        public int cacheHit = 0;
        public int cacheMiss = 0;
        public bool IsInDiagonal(int[] board, int n)
        {
            // test all (x,y) coordinates
            var x = n;
            var y = board[x-1]; // only look at the newest point
            // (x, y) - coordinate is (i, pos)
            for (var x2 = 1; x2 < n; x2++)
            {
                var y2 = board[x2-1];
                if (cacheDiagonal[x-1, y-1, x2-1, y2-1] != 0)
                {
                    cacheHit++;
                    if (cacheDiagonal[x-1, y-1, x2-1, y2-1] == 1)
                        return true;
                    else
                        continue;
                }
                cacheMiss++;
                // solve equation for line between the two points
                double slope = (double)(y - y2) / (x - x2);
                // if slope is 1 they are on a diagonal
                if (slope == 1 || slope == -1)
                {
                    cacheDiagonal[x-1, y-1, x2-1, y2-1] = 1;
                    return true;
                }
                else
                {
                    cacheDiagonal[x-1, y-1, x2-1, y2-1] = -1;
                }
            }

            return false;
        }

        private int[, , , , ,] cacheLine = new int[35,35,35,35,35,35];
        
        public bool IsOnLine(int[] board, int n)
        {
            var x = n;
            var y = board[x - 1];
            
            // from first point to the second last point 
            for (var x2 = 1; x2 < n; x2++)
            {
                var y2 = board[x2 - 1];
                // from the second point to the third last
                for (var x3 = x2+1; x3 < n; x3++)
                {
                    var y3 = board[x3 - 1];

//                    if (cacheLine[x-1, y-1, x2-1, y2-1, x3-1, y3-1] != 0)
//                    {
//                        if (cacheLine[x-1, y-1, x2-1, y2-1, x3-1, y3-1] == 1)
//                            return true;
//                        else
//                            continue;
//                    }
                   
                    // solve equation for line between the two points
                    double a = (double) (y - y2) / (x - x2);
                    double b = y - a * x;

                    // check if the third point are on the line 
                    if (y3 == a * x3 + b)
                    {
//                        cacheLine[x-1, y-1, x2-1, y2-1, x3-1, y3-1] = 1;
                        return true;
                    }
                    else
                    {
//                        cacheLine[x-1, y-1, x2-1, y2-1, x3-1, y3-1] = -1;
                    }
                }
            }

            return false;
        }
    }
}
