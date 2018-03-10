using System.Linq;
using System.Collections.Generic;

namespace SpyLib
{
    /// <summary>
    /// Implements a board generator that creates boards from size 1 and up, but only follows paths that gives valid boards,
    /// will stop at the first valid board, and return a list with 1 element, the valid board
    /// if no valid boards are found an empty list is returned
    /// </summary>
    public class SmartBoardGenerator : IBoardGenerator
    {
        protected IBoardValidator _validator;

        public IEnumerable<Board> Generate(IBoardValidator validator, int n)
        {
            _validator = validator;

            var elementsAvailable = new List<int>(Enumerable.Range(1, n));
            var currentPermutation = new int[n];

            // find the first valid configuration
            if(Perms(ref currentPermutation, elementsAvailable, 0))
            {
                return new List<Board> { new Board(currentPermutation, n) };
            }

            return new List<Board>();
        }

        protected bool Perms(ref int[] currentPermutation, List<int> elementsAvailable, int k)
        {
            var elementsToNext = new List<int>(elementsAvailable);
            
            foreach (var elm in elementsAvailable)
            {
                // add to currentPermutation
                currentPermutation[k] = elm;

                if (_validator.IsValid(new Board(currentPermutation, k+1)))
                {
                    // go to next element
                    elementsToNext.Remove(elm);
                    if ( ! elementsToNext.Any())
                    {
                        // all elements used and permutation is valid
                        return true;
                    }
                    
                    if (Perms(ref currentPermutation, elementsToNext, k+1))
                    {
                        return true;
                    }
                    
                    // the element was not valid so it should be available for the next iteration
                    elementsToNext.Add(elm);
                }
            }

            return false;
        }
    }
}
