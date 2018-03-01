using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.Linq;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace SpyLib
{
    public class BoardTester
    {
        private readonly int _start;
        private readonly int _end;
        
        public BoardTester(int start, int end)
        {
            _start = start;
            _end = end;
        }

        public void Run()
        {
            var generator = new AllBoardGenerator();
            var validator = new BruteForceValidator();
            
            for (var n = _start; n < _end; n += 2)
            {
                var sw = new Stopwatch();
                
                Console.WriteLine("Finding valid {0} boards", n);
                sw.Start();
                foreach (var board in generator.Generate(validator, n))
                {
                    if (validator.IsValid(board.ToArray(), n))
                    {
                        OutputBoard(board.ToArray(), n);
                        break;
                    }
                }
                
                sw.Stop();
                Console.WriteLine("Elapsed={0}", sw.Elapsed);
                Console.WriteLine("");
            }
        }

        private void OutputBoard(int[] board, int n)
        {
            Console.WriteLine(n);
            foreach (var pos in board)
            {
                Console.Write(pos+" ");
            }

            Console.WriteLine("");  
        }
    }
}
