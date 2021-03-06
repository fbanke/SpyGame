﻿using SpyLib;
using NUnit.Framework;

namespace SpyLibTest
{
    [TestFixture]
    public class BruteForceValidatorTest
    {
        static object[] DiagonalCases =
        {
            // x # #
            // # # x
            // # x #
            // the last two elements are on a diagonal
            new object[] { new Board(new[] { 1, 3, 2 }, 3), true},

            // x # # # #
            // # # x # #
            // # # # # x
            // # x # # #
            // # # # x #
            // no elements in a diagonal
            new object[] { new Board(new[] { 1, 3, 5, 2, 4 }, 5), false},
        };

        [Test]
        [TestCaseSource("DiagonalCases")]
        public void Test_Diagonals(Board board, bool returnValue)
        {
            var validator = new BruteForceValidator();
            
            Assert.AreEqual(returnValue, validator.IsInDiagonal(board));
        }

        static object[] OnLineCases =
        {
            // x # # # #
            // # # x # #
            // # # # # x
            // # x # # #
            // # # # x #
            // the first 3 elements are on the same line
            new object[] { new Board(new []{1, 3, 5, 2, 4}, 5), true },

            // x # # # #
            // # # x # #
            // # # # x #
            // # x # # #
            // # # # # x
            // no elements on the same line
            new object[] { new Board(new []{1, 3, 4, 2, 5}, 5), false }
        };

        [Test]
        [TestCaseSource("OnLineCases")]
        public void Test_OnLine(Board board, bool returnValue)
        {
            var validator = new BruteForceValidator();
            
            Assert.AreEqual(returnValue, validator.IsOnLine(board));
        }

        [Test]
        public void TestThat_DebugFormatCountsValidatedBoardsCorrect()
        {
            var validator = new BruteForceValidator();
            var board = new Board(new[] { 1, 2, 3 }, 3);

            validator.IsValid(board);
            validator.IsValid(board);

            StringAssert.Contains("Boards validated: 2", validator.GetDebug());
        }
    }
}
