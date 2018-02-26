using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Linq;

namespace SpyLib
{
    public static class BoardGenerator
    {
        public static IEnumerable<IEnumerable<int>> GenerateBoards(int n, List<int[]> validBoardsNMinus1)
        {
            var positions = Enumerable.Range(1, n);

            if (validBoardsNMinus1.Count() == 0)
            {
                return QuickPerm(positions);
            }
            else
            {
                return QuickPermFromExisting(n, validBoardsNMinus1);
            }
        }

        public static IEnumerable<IEnumerable<int>> QuickPermFromExisting(int n, List<int[]> validBoardsNMinus1)
        {
            foreach (var board in validBoardsNMinus1)
            {
                // find valid values
                // only the new rows needs to be populated
                var validValues = new List<int>();
                for (int i = 1; i <= n; i++)
                {
                    validValues.Add(i);
                }
                
                // generate all combinations for existing board
                var newBoard = new List<int>();
                for (int i = 0; i < n - 1; i++)
                {
                    newBoard.Add(board[i]);
                }
                foreach (var value in validValues)
                {
                    List<int> list = new List<int>(newBoard);
                    list.Add(value);
                    yield return list;
                }

            }            
        }
        
        public static IEnumerable<IEnumerable<T>> QuickPerm<T>(this IEnumerable<T> set)
        {
            int N = set.Count();
            int[] a = new int[N];
            int[] p = new int[N];

            var yieldRet = new T[N];

            List<T> list = new List<T>(set);

            int i, j, tmp; // Upper Index i; Lower Index j

            for (i = 0; i < N; i++)
            {
                // initialize arrays; a[N] can be any type
                a[i] = i + 1; // a[i] value is not revealed and can be arbitrary
                p[i] = 0; // p[i] == i controls iteration and index boundaries for i
            }
            yield return list;
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

                    for (int x = 0; x < N; x++)
                    {
                        yieldRet[x] = list[a[x] - 1];
                    }
                    yield return yieldRet;
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
