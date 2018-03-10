using System.Linq;
using System.Collections.Generic;

namespace SpyLib
{
    /// <summary>
    /// Implements a board generator that creates all possible permutations of lenght n
    /// It is implemented as a generator to prevent large memory consumption on large n
    /// 
    /// Uses the QuickPerm algorithm explained here http://www.quickperm.org/
    /// 
    /// Implementation is copied from here https://stackoverflow.com/questions/11208446/generating-permutations-of-a-set-most-efficiently
    /// </summary>
    public class AllBoardGenerator : IBoardGenerator
    {
        public IEnumerable<Board> Generate(IBoardValidator validator, int n)
        {
            var positions = Enumerable.Range(1, n);

            return QuickPerm(positions);
        }

        private IEnumerable<Board> QuickPerm(IEnumerable<int> set)
        {
            var N = set.Count();
            var a = new int[N];
            var p = new int[N];

            var yieldRet = new int[N];

            var list = new List<int>(set);

            int i, j, tmp; // Upper Index i; Lower Index j

            for (i = 0; i < N; i++)
            {
                // initialize arrays; a[N] can be any type
                a[i] = i + 1; // a[i] value is not revealed and can be arbitrary
                p[i] = 0; // p[i] == i controls iteration and index boundaries for i
            }
            yield return new Board(list.ToArray(), N);
            //display(a, 0, 0);   // remove comment to display array a[]
            i = 1; // setup first swap points to be 1 and 0 respectively (i & j)
            while (i < N)
            {
                if (p[i] < i)
                {
                    j = i % 2 * p[i]; // IF i is odd then j = p[i] otherwise j = 0
                    tmp = a[j]; // swap(a[j], a[i])
                    a[j] = a[i];
                    a[i] = tmp;

                    //MAIN!

                    for (var x = 0; x < N; x++)
                    {
                        yieldRet[x] = list[a[x] - 1];
                    }
                    yield return new Board(yieldRet.ToArray(), N);
                    //display(a, j, i); // remove comment to display target array a[]

                    // MAIN!

                    p[i]++; // increase index "weight" for i by one
                    i = 1; // reset index i to 1 (assumed)
                }
                else
                {
                    // otherwise p[i] == i
                    p[i] = 0; // reset p[i] to zero
                    i++; // set new index value for i (increase by one)
                } // if (p[i] < i)
            } // while(i < N)
        }
    }
}
