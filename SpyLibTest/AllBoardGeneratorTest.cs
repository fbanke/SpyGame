using System.Collections.Generic;
using System.Diagnostics;
using SpyLib;
using NUnit.Framework;

namespace SpyLibTest
{
    [TestFixture]
    public class AllBoardGeneratorTest
    {

        [Test]
        public void TestThat_GeneratingAllBoardsFor3()
        {
            var generator = new AllBoardGenerator();
            var boards = new List<int[]>{
                new []{1,2,3},
                new []{2,1,3},
                new []{2,3,1},
                new []{3,1,2},
                new []{1,3,2},
                new []{3,2,1}
            };
            
            foreach (var board in generator.Generate(new MockValidator(), 3))
            {
                CollectionAssert.Contains(boards, board);  
            }

        }
        
        [Test]
        public void TestThat_GeneratingAllBoardsFor2()
        {
            var generator = new AllBoardGenerator();
            var boards = new List<int[]>
            {
                new []{1,2},
                new []{2,1}
            };
            
            foreach (var board in generator.Generate(new MockValidator(), 2))
            {
                CollectionAssert.Contains(boards, board);  
            }

        }
    }

    public class MockValidator : IBoardValidator
    {
        public Stopwatch GetStopwatch()
        {
            return new Stopwatch();
        }
        public bool IsValid(int[] board, int n)
        {
            return true;
        }
    }
}
