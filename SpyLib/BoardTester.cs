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
            for (var n = _start; n < _end; n += 2)
            {
                //var generator = new AllBoardGenerator();
                //var validator = new BruteForceValidator();
            
                //var generator = new SmartBoardGenerator();
                var validator = new SmartValidator();
            
                var generator = new RandomSmartBoardGenerator();
                //var validator = new CachingValidator();
                
                var sw = new Stopwatch();

//                var foo = new[] {16, 3, 11, 15, 18, 9, 14};
//                Console.WriteLine(new SmartValidator().IsOnLine(foo, foo.Length));
//                break;

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
                Console.WriteLine("Validation time={0}", validator.GetStopwatch().Elapsed);
                Console.WriteLine("Elapsed={0}", sw.Elapsed);
//                Console.WriteLine("Cache hits={0}", validator.cacheHit);
//                Console.WriteLine("Cache miss={0}", validator.cacheMiss);
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
