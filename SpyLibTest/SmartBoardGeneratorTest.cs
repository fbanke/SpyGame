using System.Collections.Generic;
using SpyLib;
using NUnit.Framework;

namespace SpyLibTest
{
    [TestFixture]
    public class SmartBoardGeneratorTest
    {
        [Test]
        // if all boards are valid all permutations should be created
        public void TestThat_GeneratingAllBoardsFor3WhenAllAreValid()
        {
            var generator = new SmartBoardGenerator();
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
        public void TestThat_GeneratingOnlyValidBoards11()
        {
            var generator = new SmartBoardGenerator();
            var boards = new List<Board>{
                new Board(new []{2, 4, 7, 1, 8, 11, 5, 3, 9, 6, 10}, 11), // first valid 11 size board
            };

            foreach (var board in generator.Generate(new BruteForceValidator(), 11))
            {
                CollectionAssert.Contains(boards, board);
            }
        }

        [Test]
        public void TestThat_NoValidBoardsReturnsEmptyList()
        {
            var generator = new SmartBoardGenerator();
            // there exists no valid boards below size 7x7
            CollectionAssert.IsEmpty(generator.Generate(new BruteForceValidator(), 6));
        }
    }
}
