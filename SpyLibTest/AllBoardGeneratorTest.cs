using System.Collections.Generic;
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
            var boards = new List<Board>{
                new Board(new []{1,2,3}, 3),
                new Board(new []{2,1,3}, 3),
                new Board(new []{2,3,1}, 3),
                new Board(new []{3,1,2}, 3),
                new Board(new []{1,3,2}, 3),
                new Board(new []{3,2,1}, 3)
            };
            
            foreach (var board in generator.Generate(new MockValidatorAlwaysTrue(), 3))
            { 
                CollectionAssert.Contains(boards, board);  
            }
        }
        
        [Test]
        public void TestThat_GeneratingAllBoardsFor2()
        {
            var generator = new AllBoardGenerator();
            var boards = new List<Board>
            {
                new Board(new []{1,2}, 2),
                new Board(new []{2,1}, 2)
            };
            
            foreach (var board in generator.Generate(new MockValidatorAlwaysTrue(), 2))
            {
                CollectionAssert.Contains(boards, board);  
            }
        }
    }
}
