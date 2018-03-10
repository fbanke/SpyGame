using System.Collections.Generic;
using System.Diagnostics;
using SpyLib;
using NUnit.Framework;
using System;

namespace SpyLibTest
{
    [TestFixture]
    public class BoardTest
    {

        [Test]
        public void TestThat_OutputFormatConformesToRequirement()
        {
            var board = new Board(new int[] { 1, 2, 3 }, 3);

            Assert.AreEqual(
                "3"+Environment.NewLine+
                "1 2 3"+Environment.NewLine, 
                board.ToString()
                );
        }

        [Test]
        public void TestThat_EqualsBoardsAreEqual()
        {
            var board1 = new Board(new int[] { 1, 2, 3 }, 3);
            var board2 = new Board(new int[] { 1, 2, 3 }, 3);

            Assert.AreEqual(board1, board2);
        }

        [Test]
        public void TestThat_BoardsOfDifferentSizeAreNotEqual()
        {
            var board1 = new Board(new int[] { 1, 2, 3 }, 3);
            var board2 = new Board(new int[] { 1, 2, 3 }, 2);

            Assert.AreNotEqual(board1, board2);
        }

        [Test]
        public void TestThat_BoardsWithDifferentValuesAreNotEquals()
        {
            var board1 = new Board(new int[] { 1, 2, 3 }, 3);
            var board2 = new Board(new int[] { 3, 2, 1 }, 3);

            Assert.AreNotEqual(board1, board2);
        }
    }
}
