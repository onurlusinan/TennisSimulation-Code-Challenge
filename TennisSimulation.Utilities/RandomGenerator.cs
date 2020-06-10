using System;
using System.Collections.Generic;
using System.Linq;

namespace TennisSimulation.Utilities
{
    public static class RandomGenerator
    {
        // This is a distinct random generator between 0 and a chosen max value, with a count 
        // We use this during the random league matches
        // We put the integers in a HashSet, then shuffle them using the Fisher-Yates shuffle
        // Number of unique randoms: (2^n) * (2^n -1) / 2
        // Thus in some scenarios where the player count is high we might need to create many many randoms, which would be inefficient

        static Random random = new Random();

        public static List<int> GenerateRandom(int count, int min, int max)
        {
            if (max <= min || count < 0 || (count > max - min && max - min > 0)) // max - min > 0 to avoid overflow
            {
                throw new ArgumentOutOfRangeException("Range " + min + " to " + max +
                        " (" + ((Int64)max - (Int64)min) + " values), or count " + count + " is illegal");
            }

            HashSet<int> candidates = new HashSet<int>();

            for (int top = max - count; top < max; top++)
            {
                if (!candidates.Add(random.Next(min, top + 1)))
                {
                    candidates.Add(top);
                }
            }

            List<int> result = candidates.ToList();

            // shuffle the results
            for (int i = result.Count - 1; i > 0; i--)
            {
                int k = random.Next(i + 1);
                int tmp = result[k];
                result[k] = result[i];
                result[i] = tmp;
            }
            return result;
        }

        public static List<int> GenerateRandomList(int count, int maxValue)
        {
            return GenerateRandom(count, 0, maxValue);
        }
    }
}
