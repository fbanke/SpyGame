using System;
using System.Collections.Generic;
using SpyLib;
using NUnit.Framework;

namespace SpyLibTest
{
    [TestFixture]
    public class BoardGeneratorTest
    {

        [Test]
        public void TestThat_GenerateNextBoards()
        {
            List<int[]> boards = new List<int[]>();
            boards.Add(new int[]{1,2,3});

            foreach (var board in BoardGenerator.QuickPermFromExisting(4, boards))
            {
                
            }
        }
    }
}
