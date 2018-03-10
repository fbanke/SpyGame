using System;
using System.Linq;
using System.Text;

namespace SpyLib
{
    public struct Board
    {
        public int[] board;
        public int n;

        public Board(int[] board, int n)
        {
            this.board = board;
            this.n = n;
        }

        public override bool Equals(Object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var b = (Board)obj;

            bool isEqual = Enumerable.SequenceEqual(board, b.board);
            
            if (b.n == n && isEqual)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            var output = new StringBuilder();
            output.AppendLine(n.ToString());

            var last = board[board.Length - 1];
            foreach (var pos in board)
            {
                output.AppendFormat(pos.ToString());
                if(pos != last)
                {
                    output.Append(" ");
                }
            }

            output.AppendLine("");
            return output.ToString();
        }

        public override int GetHashCode()
        {
            return board.GetHashCode() ^ n;
        }
    }
}