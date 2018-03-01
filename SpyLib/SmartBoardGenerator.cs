using System.Linq;
using System.Collections.Generic;

namespace SpyLib
{
    public class SmartBoardGenerator : IBoardGenerator
    {
        private IBoardValidator _validator;
        
        public IEnumerable<IEnumerable<int>> Generate(IBoardValidator validator, int n)
        {
            _validator = validator;
            
            var elementsAvailable = new List<int>(Enumerable.Range(1, n));
            var currentPermutation = new int[n];

            Perms(ref currentPermutation, elementsAvailable, 0);

            yield return currentPermutation;
        }

        private bool Perms(ref int[] currentPermutation, List<int> elementsAvailable, int k)
        {
            var elementsToNext = new List<int>(elementsAvailable);
            
            foreach (var elm in elementsAvailable)
            {
                // add to currentPermutation
                currentPermutation[k] = elm;

                if (_validator.IsValid(currentPermutation, k+1))
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
