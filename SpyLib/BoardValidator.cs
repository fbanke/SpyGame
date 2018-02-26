using System;
using System.Linq;
using System.Collections.Generic;

namespace SpyLib
{
    public class BoardValidator
    {
        public void OutputBoard(int[] board)
        {
            Console.WriteLine("");
            foreach (var i in board)
            {
                Console.Write(i);
            }
        }

        public bool IsValid(int[] board)
        {
            if (HasSameColumnsInConfiguration(board))
            {
                return false;
            }

            if (IsInDiagonal(board))
            {
                return false;
            }

            if (IsOnLine(board))
            {
                return false;
            }

            return true;
        }

        public bool IsInDiagonal(int[] board)
        {
            int n = board.Count();
            var diagonals45 = GenerateDiagonals45(board.Count());
            var diagonals135 = GenerateDiagonals135(board.Count());

            var diagonals = new int[diagonals45.Count() + diagonals135.Count()][];
            Array.Copy(diagonals45, diagonals, diagonals45.Length);
            Array.Copy(diagonals135, 0, diagonals, diagonals45.Length, diagonals135.Length);

            foreach (int[] diagonal in diagonals)
            {
                int counts = 0;
                for (int i = 0; i < n; i++)
                {
                    if (diagonal[i] == 0) continue;
                    if (board[i] == diagonal[i])
                    {
                        counts++;
                    }
                }

                if (counts > 1)
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsOnLine(int[] board)
        {
            var n = board.Count();
            // generate all (x,y) coordinates

            for (var x = 1; x <= n; x++)
            {
                var y = board[x-1];
                // (x, y) - coordinate is (i, pos)
                for (var x2 = x+1; x2 <= n; x2++)
                {
                    var y2 = board[x2-1];
                    // solve equasion for line between the two points
                    double a = (double)(y - y2) / (x - x2);
                    double b = y - a * x;

                    for (var x3 = x2+1; x3 <= n; x3++)
                    {
                        var y3 = board[x3-1];
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

        // chech if any of the spies are in the same column
        public bool HasSameColumnsInConfiguration(int[] configuration)
        {
            // remove columns where no spy is placed
            var cleanConfig = configuration.Where(num => num != 0);

            return (cleanConfig.Count() != cleanConfig.Distinct().Count());
        }

        public int[][] GenerateDiagonals135(int n)
        {
            var diagonals = new List<int[]>();

            // generate the top left diagonals including the middle diagonal
            for (int i = 0; i < n - 1; i++)
            {
                var diagonal = new int[n];
                var diag = Enumerable.Range(1, n - i).Reverse().ToArray();

                // copy diagonal
                var diagLength = diag.Count();
                for (int j = 0; j < diagLength; j++)
                {
                    diagonal[j] = diag[j];
                }

                diagonals.Add(diagonal);
            }

            // generate lower left diagonals, excluding middle diagonal
            for (int i = 1; i < n - 1; i++)
            {
                var diagonal = new int[n];

                var diag = Enumerable.Range(i + 1, n - i).Reverse().ToArray();
                var diagCount = diag.Count();
                var zeroColumns = n - diagCount;
                // copy diagonal
                for (int j = 0; j < diagCount; j++)
                {
                    diagonal[j + zeroColumns] = diag[j];
                }

                diagonals.Add(diagonal);
            }

            return diagonals.ToArray();
        }

        public int[][] GenerateDiagonals45(int n)
        {
            var diagonals = new List<int[]>();

            for (int i = 1; i < n; i++)
            {
                var diagonal = new int[n];
                var diag = Enumerable.Range(i, n - i + 1).ToArray();

                // copy diagonal
                var diagLength = diag.Count();
                for (int j = 0; j < diagLength; j++)
                {
                    diagonal[j] = diag[j];
                }

                diagonals.Add(diagonal);
            }

            for (int i = 1; i < n - 1; i++)
            {
                var diagonal = new int[n];

                var diag = Enumerable.Range(1, n - i).ToArray();
                var diagCount = diag.Count();
                var zeroColumns = n - diagCount;
                // copy diagonal
                for (int j = 0; j < diagCount; j++)
                {
                    diagonal[j + zeroColumns] = diag[j];
                }

                diagonals.Add(diagonal);
            }

            return diagonals.ToArray();
        }
    }
}
