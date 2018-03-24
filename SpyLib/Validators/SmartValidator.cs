using System;
using System.Diagnostics;
using System.Text;

namespace SpyLib
{
    public class SmartValidator : IBoardValidator
    {
        private Stopwatch _validationTime;
        private int _boardsValidated = 0;

        public SmartValidator()
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

        public virtual bool IsInDiagonal(Board board)
        {
            // test all (x,y) coordinates
            var x = board.n;
            var y = board.board[x-1]; // only look at the newest point
            // (x, y) - coordinate is (i, pos)
            for (var x2 = 1; x2 < board.n; x2++)
            {
                var y2 = board.board[x2-1];
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
        
        public virtual bool IsOnLine(Board board)
        {
            var x = board.n;
            var y = board.board[x - 1];
            // from first point to the second last point 
            for (var x2 = 1; x2 < board.n; x2++)
            {
                var y2 = board.board[x2 - 1];
                // solve equation for line between the two points
                var a = (double) (y - y2) / (x - x2);
                var b = y - a * x;
                // from the second point to the third last
                for (var x3 = x2+1; x3 < board.n; x3++)
                {
                    var y3 = board.board[x3 - 1];
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
