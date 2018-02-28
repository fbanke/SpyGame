using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.Linq;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;

namespace SpyLib
{
    public class BoardTester
    {
        private Stopwatch validation = new Stopwatch();
        
        public BoardTester()
        {
            for (int n = 11; n < 100; n += 2)
            {
                Console.WriteLine("Finding valid {0} boards", n);
                Stopwatch sw = new Stopwatch();
                sw.Start();
                
                var elementsAvailable = new List<int>(Enumerable.Range(1, n));
                var validator = new BoardValidator();
                var currentPermutation = new List<int>();

                validation.Reset();
                Perms(validator, ref currentPermutation, elementsAvailable);

                sw.Stop();
                Console.WriteLine("Elapsed={0}", sw.Elapsed);
                Console.WriteLine("ValidationTime={0}", validation.Elapsed);
                Console.WriteLine("Elapsed for validating diagonal={0}", validator.swDiagonal.Elapsed);
                Console.WriteLine("Elapsed for validating online={0}", validator.swOnLine.Elapsed);

                Console.WriteLine("");

                Console.WriteLine(n);
                foreach (var pos in currentPermutation)
                {
                    Console.Write(pos+" ");
                }

                Console.WriteLine("");
            }

        }

        public List<int[]> GetValidBoards(int n)
        {
            var validator = new BoardValidator();
            var validBoards = new List<int[]>();
            var boardCount = 0;
            foreach (var boardList in BoardGenerator.GenerateBoards(validator, n))
            {
                var board = new int[n];
                int i = 0;
                foreach (var pos in boardList)
                {
                    board[i++] = pos;
                }
                if (boardCount % 1000000 == 0)
                {
                    Console.Write(".");
                }

                if (validator.IsValid(board, n))
                {
                    Console.Write(",");

                    validBoards.Add(board);
                    break;
                }

                boardCount++;
            }

            Console.WriteLine("");
            Console.WriteLine("Elapsed for validating diagonal={0}", validator.swDiagonal.Elapsed);
            Console.WriteLine("Elapsed for validating online={0}", validator.swOnLine.Elapsed);

            return validBoards;
        }
        
        bool Perms(BoardValidator validator, ref List<int> currentPermutation, List<int> elementsAvailable)
        {
            var elementsToNext = new List<int>(elementsAvailable);
            
            foreach (var elm in elementsAvailable)
            {
                // add to currentPermutation
                currentPermutation.Add(elm);

                if (validator.IsValid(currentPermutation.ToArray(), currentPermutation.Count()))
                {
                    elementsToNext.Remove(elm);
                    if ( ! elementsToNext.Any())
                    {
                        // all elements used and permutation is valid
                        return true;
                    }
                    
                    // go to next element
                    if (Perms(validator, ref currentPermutation, elementsToNext))
                    {
                        return true;
                    }
                    
                    // the element was not valid so it should be available for the next iteration
                    elementsToNext.Add(elm);
                }
                validation.Stop();
                
                currentPermutation.Remove(elm);
            }

            return false;
        }
    }
}
