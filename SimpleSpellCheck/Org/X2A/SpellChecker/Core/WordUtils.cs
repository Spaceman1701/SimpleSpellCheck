using System;
using System.Collections.Generic;
using System.Text;

namespace Org.X2A.SpellChecker.Core
{
    public sealed class WordUtils
    {
        private WordUtils() { }


        public static int EditDistance(string a, string b)
        {

            if (a.Length == 0)
                return b.Length;
            if (b.Length == 0)
                return a.Length;

            int[,] dist = new int[a.Length + 1, b.Length + 1];

            for (int i = 1; i <= a.Length; i++)
            {
                dist[i, 0] = i;
                for (int j = 1; j <= b.Length; j++)
                {
                    int cost = a[i - 1] == b[j - 1] ? 0 : 1;
                    if (i == 1)
                    {
                        dist[0, j] = j;
                    }

                    dist[i, j] = Min(
                        dist[i - 1, j] + 1,
                        dist[i, j - 1] + 1,
                        dist[i - 1, j - 1] + cost
                    );
                    if (i > 1 && j > 1 && a[i - 1] == b[j - 2] && a[i - 2] == b[j - 1]) //is transposition legal
                    {
                        dist[i, j] = Min(dist[i, j], dist[i - 2, j - 2] + cost); //is transposition the best choice
                    }
                }
            }

            return dist[a.Length, b.Length];
        }


        private static int Min(int a, params int[] vals)
        {
            if (vals.Length == 0)
            {
                return a;
            }
            int currentMin = a;
            for (int i = 0; i < vals.Length; i++)
            {
                currentMin = Math.Min(currentMin, vals[i]);
            }
            return currentMin;
        }
    }
}
