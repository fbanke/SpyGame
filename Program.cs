using System;
using SpyLib;

namespace SpyGame
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            var tester = new BoardTester(11, 21);
            tester.Run();
        }
    }
}
