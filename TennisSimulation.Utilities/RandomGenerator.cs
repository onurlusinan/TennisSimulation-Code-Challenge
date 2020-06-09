using System;
using System.Collections.Generic;
using System.Linq;

namespace TennisSimulation.Utilities
{
    public static class RandomGenerator
    {
        // This is a distinct random generator between 0 and a chosen max value. 
        // We use this during the random league matches.
        // Based on the Fisher-Yates algorithm, since the number of players is 2^n.
        // Thus, number of the random values we must generate can be big values. Number of randoms: (2^n) * (2^n -1) / 2

        static Random random = new Random();

        public static List<int> GenerateRandom(int count, int min, int max)
        {
            if (max <= min || count < 0 ||
                    // max - min > 0 required to avoid overflow
                    (count > max - min && max - min > 0))
            {
                // need to use 64-bit to support big ranges (negative min, positive max)
                throw new ArgumentOutOfRangeException("Range " + min + " to " + max +
                        " (" + ((Int64)max - (Int64)min) + " values), or count " + count + " is illegal");
            }

            // generate count random values.
            HashSet<int> candidates = new HashSet<int>();

            // start count values before max, and end at max
            for (int top = max - count; top < max; top++)
            {
                // May strike a duplicate.
                // Need to add +1 to make inclusive generator
                // +1 is safe even for MaxVal max value because top < max
                if (!candidates.Add(random.Next(min, top + 1)))
                {
                    // collision, add inclusive max.
                    // which could not possibly have been added before.
                    candidates.Add(top);
                }
            }

            // load them in to a list, to sort
            List<int> result = candidates.ToList();

            // shuffle the results because HashSet has messed
            // with the order, and the algorithm does not produce
            // random-ordered results (e.g. max-1 will never be the first value)
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
