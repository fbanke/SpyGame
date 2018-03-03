using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;

namespace SpyLib
{
    public class RandomSmartBoardGenerator : SmartBoardGenerator
    {
        public new IEnumerable<IEnumerable<int>> Generate(IBoardValidator validator, int n)
        {
            _validator = validator;
            
            var elementsAvailable = new List<int>(Enumerable.Range(1, n));
            elementsAvailable.Shuffle();
            
            var currentPermutation = new int[n];

            Perms(ref currentPermutation, elementsAvailable, 0);

            yield return currentPermutation;
        }
    }
    
    public static class ThreadSafeRandom
    {
        [ThreadStatic] private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }
    
    static class RandomExtension
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
