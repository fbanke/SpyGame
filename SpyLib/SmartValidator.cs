﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpyLib
{
    public class SmartValidator : IBoardValidator
    {
        public SmartValidator()
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
            var x = n;
            var y = board[x-1]; // only look at the newest point
            // (x, y) - coordinate is (i, pos)
            for (var x2 = 1; x2 < n; x2++)
            {
                var y2 = board[x2-1];
                // solve equation for line between the two points
                var slope = (double)(y - y2) / (x - x2);
                // if slope is 1 they are on a diagonal
                if (Math.Abs(slope - 1) < TOLERANCE || Math.Abs(slope - (-1)) < TOLERANCE)
                {
                    return true;
                }
            }

            return false;
        }

        private const double TOLERANCE = 0.001;
        
        public bool IsOnLine(int[] board, int n)
        {
            var x = n;
            var y = board[x - 1];
            //Console.WriteLine(y);
            // from first point to the second last point 
            for (var x2 = 1; x2 < n; x2++)
            {
                var y2 = board[x2 - 1];
                //Console.WriteLine("A: "+y2);
                // solve equation for line between the two points
                var a = (double) (y - y2) / (x - x2);
                var b = y - a * x;
                // from the second point to the third last
                for (var x3 = x2+1; x3 < n; x3++)
                {
                    var y3 = board[x3 - 1];
                    //Console.WriteLine("B: "+y3);
                    //Console.WriteLine("C: "+(a * x3 + b));
                    // check if the third point are on the line 
                    if (Math.Abs(y3 - (a * x3 + b)) < TOLERANCE)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
