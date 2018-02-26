using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SpyLib
{
    public class BoardTester
    {
        public BoardTester()
        {
            int n = 11;

            Console.WriteLine("Finding valid {0} boards", n);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var validBoards = GetValidBoards(n);

            sw.Stop();
            Console.WriteLine("");
            Console.WriteLine("Elapsed={0}", sw.Elapsed);


            Console.WriteLine("Found {0} valid boards", validBoards.Count);

            foreach (var board in validBoards)
            {
                Console.WriteLine("");
                foreach (var pos in board)
                {
                    Console.Write(pos);
                }
            }
            Console.WriteLine("");
            
        }

        public List<int[]> GetValidBoards(int n, List<int[]> validBoardsNMinus1)
        {
            var validator = new BoardValidator();
            var validBoards = new List<int[]>();
            var boardCount = 0;
            foreach (var boardList in BoardGenerator.GenerateBoards(n, validBoardsNMinus1))
            {
                var board = new int[n];
                int i = 0;
                foreach (var pos in boardList)
                {
                    board[i++] = pos;
                }
                if (boardCount % 10000 == 0)
                {
                    Console.Write(".");
                }

                if (validator.IsValid(board))
                {
                    Console.Write(",");

                    validBoards.Add(board);
                }

                boardCount++;
            }

            return validBoards;
        }
        
        public List<int[]> GetValidBoards(int n)
        {
            return GetValidBoards(n, new List<int[]>());
        }
    }
}
