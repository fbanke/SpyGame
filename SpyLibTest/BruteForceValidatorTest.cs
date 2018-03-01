using System.Collections.Generic;
using SpyLib;
using NUnit.Framework;

namespace SpyLibTest
{
    [TestFixture]
    public class BruteForceValidatorTest
    {

        [Test]
        public void TestThat_IsInDiagonalReturnsTrueWhen2ElementsAreInADiagonal()
        {
            // x # #
            // # # x
            // # x #
            var board = new []{1, 3, 2}; 
            var validator = new BruteForceValidator();
            
            Assert.IsTrue(validator.IsInDiagonal(board, 3));
        }
        
        [Test]
        public void TestThat_IsInDiagonalReturnsFalseWhenNoElementsAreInADiagonal()
        {
            // x # # # #
            // # # x # #
            // # # # # x
            // # x # # #
            // # # # x #
            var board = new []{1, 3, 5, 2, 4}; 
            var validator = new BruteForceValidator();
            
            Assert.IsFalse(validator.IsInDiagonal(board, 5));
        }
        
        [Test]
        public void TestThat_IsInOnLineReturnsTrueWhen3ElementsAreOnALine()
        {
            // x # # # #
            // # # x # #
            // # # # # x
            // # x # # #
            // # # # x #
            var board = new []{1, 3, 5, 2, 4};  
            var validator = new BruteForceValidator();
            
            Assert.IsTrue(validator.IsOnLine(board, 5));
        }
        
        [Test]
        public void TestThat_IsInOnLineReturnsFalseWhen2ElementsAreOnALine()
        {
            // x # # # #
            // # # x # #
            // # # # x #
            // # x # # #
            // # # # # x
            var board = new []{1, 3, 4, 2, 5};  
            var validator = new BruteForceValidator();
            
            Assert.IsFalse(validator.IsOnLine(board, 5));

        }
    }
}
