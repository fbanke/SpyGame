using System;
using SpyLib;
using NUnit.Framework;

namespace SpyLibTest
{
    [TestFixture]
    public class BoardValidatorTest
    {

        [Test]
        public void TestThat_DistinctColumnsGivesFalse()
        {
            int[] board = { 1, 2, 3, 4, 5 };

            var validator = new BoardValidator();
            var result = validator.HasSameColumnsInConfiguration(board);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestThat_DuplicatedColumsGivesTrue()
        {
            int[] board = { 2, 2, 3, 4, 5 };

            var validator = new BoardValidator();
            var result = validator.HasSameColumnsInConfiguration(board);

            Assert.IsTrue(result);
        }

        [Test]
        public void TestThat_SpiesAreOnLineGivesTrue()
        {
            // 8, 6 ,4 are on the same line
            int[] board = { 2, 5, 7, 1, 3, 8, 6, 4, 9 };
            var validator = new BoardValidator();

            Assert.IsTrue(validator.IsOnLine(board));
        }
        
        [Test]
        public void TestThat_SpiesAreNotOnLineGivesFalse()
        {
            int[] board = { 2, 4, 7, 1, 8, 11, 5, 3, 9, 6, 10};
            var validator = new BoardValidator();
            
            Assert.IsFalse(validator.IsOnLine(board));
        }

        /*
         * [3, 2, 1]
         * # # x
         * # x #
         * x # #
         *
         * [2, 1, 0]
         * # x #
         * x # #
         * # # #
         *
         * [0, 3, 2]
         * # # #
         * # # x
         * # x #
        */
        [Test]
        public void TestThat_Diagonal135IsGeneratedCorrectly3()
        {
            var validator = new BoardValidator();

            int[][] diagonals = {
                new int[]{3, 2, 1},
                new int[]{2, 1, 0},
                new int[]{0, 3, 2}
            };

            Assert.AreEqual(diagonals, validator.GenerateDiagonals135(3));
        }

        /*
         * [5, 4, 3, 2, 1]
         * # # # # x
         * # # # x #
         * # # x # #
         * # x # # #
         * x # # # #
         *
         * [4, 3, 2, 1, 0]
         * # # # x #
         * # # x # #
         * # x # # #
         * x # # # #
         * # # # # #
         *
         * [3, 2, 1, 0, 0]
         * # # x # #
         * # x # # #
         * x # # # #
         * # # # # #
         * # # # # #
         *
         * [2, 1, 0, 0, 0]
         * # x # # #
         * x # # # #
         * # # # # #
         * # # # # #
         * # # # # #
         *
         * [0, 5, 4, 3, 2]
         * # # # # #
         * # # # # x
         * # # # x #
         * # # x # #
         * # x # # #
         *
         * [0, 0, 5, 4, 3]
         * # # # # #
         * # # # # #
         * # # # # x
         * # # # x #
         * # # x # #
         *
         * [0, 0, 0, 5, 4]
         * # # # # #
         * # # # # #
         * # # # # #
         * # # # # x
         * # # # x #
         */
        [Test]
        public void TestThat_Diagonal135IsGeneratedCorrectly5()
        {
            var validator = new BoardValidator();

            int[][] diagonals = {
                new int[]{5, 4, 3, 2, 1},
                new int[]{4, 3, 2, 1, 0},
                new int[]{3, 2, 1, 0, 0},
                new int[]{2, 1, 0, 0, 0},
                new int[]{0, 5, 4, 3, 2},
                new int[]{0, 0, 5, 4, 3},
                new int[]{0, 0, 0, 5, 4},
            };

            Assert.AreEqual(diagonals, validator.GenerateDiagonals135(5));
        }

        /*
         * [1, 2, 3]
         * x # #
         * # x #
         * # # x
         *
         * [2, 3, 0]
         * # x #
         * # # x
         * # # #
         *
         * [0, 1, 2]
         * # # #
         * x # #
         * # x #
         */
        [Test]
        public void TestThat_diagonal45IsGeneratedCorrectly3()
        {

            var validator = new BoardValidator();

            int[][] diagonals = {
                new int[]{1,2,3},
                new int[]{2,3,0},
                new int[]{0,1,2},
            };

            Assert.AreEqual(diagonals, validator.GenerateDiagonals45(3));
        }

        /*
         * [1, 2, 3, 4, 5]
         * x # # # #
         * # x # # #
         * # # x # #
         * # # # x #
         * # # # # x
         *
         * [2, 3, 4, 5, 0]
         * # x # # #
         * # # x # #
         * # # # x #
         * # # # # x
         * # # # # #
         *
         * [3, 4, 5, 0, 0]
         * # # x # #
         * # # # x #
         * # # # # x
         * # # # # #
         * # # # # #
         *
         * [4, 5, 0, 0, 0]
         * # # # x #
         * # # # # x
         * # # # # #
         * # # # # #
         * # # # # #
         *
         * [0, 1, 2, 3, 4]
         * # # # # #
         * x # # # #
         * # x # # #
         * # # x # #
         * # # # x #
         *
         * [0, 0, 1, 2, 3]
         * # # # # #
         * # # # # #
         * x # # # #
         * # x # # #
         * # # x # #
         *
         * [0, 0, 0, 1, 2]
         * # # # # #
         * # # # # #
         * # # # # #
         * x # # # #
         * # x # # #
         */
        [Test]
        public void TestThat_diagonal45IsGeneratedCorrectly5()
        {

            var validator = new BoardValidator();

            int[][] diagonals = {
                new int[]{1, 2, 3, 4, 5},
                new int[]{2, 3, 4, 5, 0},
                new int[]{3, 4, 5, 0, 0},
                new int[]{4, 5, 0, 0, 0},
                new int[]{0, 1, 2, 3, 4},
                new int[]{0, 0, 1, 2, 3},
                new int[]{0, 0, 0, 1, 2},
            };

            Assert.AreEqual(diagonals, validator.GenerateDiagonals45(5));
        }

        [Test]
        public void TestThat_IsInDiagonal()
        {
            int[] board = { 1, 2, 3, 4, 5};

            var validator = new BoardValidator();
            var result = validator.IsInDiagonal(board);

            Assert.IsTrue(result);
        }

        [Test]
        public void TestThat_IsNotInDiagonal()
        {
            int[] board = { 1, 1, 1, 1, 1 };

            var validator = new BoardValidator();
            var result = validator.IsInDiagonal(board);

            Assert.IsFalse(result);
        }

        [Test]
        public void TestThat_ValidBoard11FromExampleIsTrue(){
            int[] board = { 2, 4, 7, 1, 8, 11, 5, 3, 9, 6, 10};

            var validator = new BoardValidator();

            Assert.IsTrue(validator.IsValid(board));
        }

        [Test]
        public void TestThat_ValidBoard13FromExampleIsTrue()
        {
            int[] board = { 1, 3, 12, 10, 7, 2, 11, 5, 8, 13, 9, 4, 6 };

            var validator = new BoardValidator();
            
            Assert.IsTrue(validator.IsValid(board));
        }

        [Test]
        public void TestThat_InValidBoard7FromExampleIsFalse()
        {
            int[] board = { 1, 3, 5, 7, 2, 4, 6 };

            var validator = new BoardValidator();

            Assert.IsFalse(validator.IsValid(board));
        }

        [Test]
        public void TestThat_InValidBoard9FromExampleIsFalse()
        {
            int[] board = { 5,3,1,6,8,2,4,7,9 }; // the 3 first numbers are on a line

            var validator = new BoardValidator();

            Assert.IsFalse(validator.IsValid(board));
        }
    }


}
