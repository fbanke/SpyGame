using SpyLib;
using NUnit.Framework;

namespace SpyLibTest
{
    [TestFixture]
    public class IntegerBoardValidatorTest
    {
        static object[] DiagonalCases =
        {
            // x # #
            // # # x
            // # x #
            // the last two elements are on a diagonal
            new object[] { new Board(new[] { 1, 3, 2 }, 3), true},

            // # x #
            // x # #
            // # # x
            // the first two elements are on a diagonal, this can't be detected by the validator
            new object[] { new Board(new[] { 2, 1, 3 }, 3), false},
        };

        [Test]
        [TestCaseSource("DiagonalCases")]
        public void Test_Diagonals(Board board, bool returnValue)
        {
            var validator = new SmartValidator();

            Assert.AreEqual(returnValue, validator.IsInDiagonal(board));
        }

        static object[] OnLineCases =
        {
            // x # # # #
            // # # x # #
            // # # # # x
            // # x # # #
            // # # # x #
            // the first 3 elements are on the same line, this can't be detected by the validator
            new object[] { new Board(new []{1, 3, 5, 2, 4}, 5), false },

            // # x # # #
            // # # # x #
            // x # # # #
            // # # x # #
            // # # # # x
            // the last three elements are on the same line
            new object[] { new Board(new []{2, 4, 1, 3, 5}, 5), true }
        };

        [Test]
        [TestCaseSource("OnLineCases")]
        public void Test_OnLine(Board board, bool returnValue)
        {
            var validator = new SmartValidator();

            Assert.AreEqual(returnValue, validator.IsOnLine(board));
        }
    }
}
