using System;
using System.Diagnostics;

namespace SpyLib
{
    public class BoardTester
    {
        private int _start;
        private int _end;
        private IBoardGenerator _generator;
        private IBoardValidator _validator;
        
        public BoardTester(int start, int end, IBoardGenerator generator, IBoardValidator validator)
        {
            _start = start;
            _end = end;
            _validator = validator;
            _generator = generator;
        }

        public void Run()
        {
            for (var n = _start; n < _end; n += 2)
            {
                var genrationTime = new Stopwatch();

                Console.WriteLine("Finding valid {0} boards", n);
                genrationTime.Start();
                foreach (var board in _generator.Generate(_validator, n))
                {
                    if (_validator.IsValid(board))
                    {
                        Console.Write(board);
                        break;
                    }
                }

                genrationTime.Stop();
                Console.WriteLine("Elapsed time: {0}", genrationTime.Elapsed);
                Console.Write(_validator.GetDebug());
            }
        }
    }
}
