using SpyLib;

namespace SpyGame
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //var generator = new AllBoardGenerator();
            //var validator = new BruteForceValidator();

            //var generator = new SmartBoardGenerator();
            //var validator = new SmartValidator();

            //var generator = new RandomSmartBoardGenerator();
            //var validator = new CachingValidator();
            var generator = new AllBoardGenerator();
            var validator = new BruteForceValidator();
            var tester = new BoardTester(9, 25, generator, validator);
            tester.Run();
        }
    }
}
