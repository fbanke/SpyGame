using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpyLib
{
    /// <summary>
    /// Implements a board validator that can validate if a board is valid
    /// 
    /// It tests for the rules that no 2 spies must be on the same diagonal line
    /// and that any 3 spies must not be on any straight line
    /// 
    /// </summary>
    public class BruteForceValidator : IBoardValidator
    {
        public BruteForceValidator()
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

        public bool IsInDiagonal(int[] board, int n)
        {
            // test all (x,y) coordinates
            for (var x = n; x >= 1; x--)
            {
                var y = board[x-1];
                // (x, y) - coordinate is (i, pos)
                for (var x2 = x-1; x2 >= 1; x2--)
                {
                    var y2 = board[x2-1];
                    // solve equation for line between the two points
                    double slope = (double)(y - y2) / (x - x2);
                    // if slope is 1 they are on a diagonal
                    if (slope == 1 || slope == -1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool IsOnLine(int[] board, int n)
        {
            // test all (x,y) coordinates

            for (var x = n; x >= 1; x--)
            {
                var y = board[x - 1];
                // (x, y) - coordinate is (i, pos)
                for (var x2 = x-1; x2 >= 1; x2--)
                {
                    var y2 = board[x2 - 1];
                    // solve equation for line between the two points
                    double a = (double) (y - y2) / (x - x2);
                    double b = y - a * x;

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
            }

            return false;
        }
    }
}
