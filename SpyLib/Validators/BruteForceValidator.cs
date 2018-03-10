using System;
using System.Diagnostics;
using System.Text;

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
        private Stopwatch _validationTime;
        private int _boardsValidated = 0;

        public BruteForceValidator()
        {
            _validationTime = new Stopwatch();
        }

        public string GetDebug()
        {
            var output = new StringBuilder();
            output.AppendFormat("Validation time: {0}" + Environment.NewLine, _validationTime.Elapsed);
            output.AppendFormat("Boards validated: {0}" + Environment.NewLine, _boardsValidated);

            return output.ToString();
        }

        public bool IsValid(Board board)
        {
            _boardsValidated++;
            _validationTime.Start();

            if (IsInDiagonal(board))
            {
                _validationTime.Stop();
                return false;
            }

            if (IsOnLine(board))
            {
                _validationTime.Stop();
                return false;
            }

            _validationTime.Stop();
            return true;
        }

        public bool IsInDiagonal(Board board)
        {
            // test all (x,y) coordinates
            for (var x = 1; x <= board.n; x++)
            {
                var y = board.board[x-1];
                // (x, y) - coordinate is (i, pos)
                for (var x2 = x; x2 <= board.n; x2++)
                {
                    var y2 = board.board[x2-1];
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

        public bool IsOnLine(Board board)
        {
            // test all (x,y) coordinates

            for (var x = 1; x <= board.n; x++)
            {
                var y = board.board[x - 1];
                // (x, y) - coordinate is (i, pos)
                for (var x2 = x+1; x2 <= board.n; x2++)
                {
                    var y2 = board.board[x2 - 1];
                    // solve equation for line between the two points
                    double a = (double) (y - y2) / (x - x2);
                    double b = y - a * x;

                    for (var x3 = x2+1; x3 <= board.n; x3++)
                    {
                        var y3 = board.board[x3 - 1];
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
