using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Implementation
{
    

    public class Interval
    {

        /// <summary>
        /// (3, 6), (8, 9), (1, 5) should return 6. 1-6 + 8-9
        /// 
        /// This should be fairly simple, first sort by the starting number such that we 
        /// have a good view of overlapping intervals. Then if the current one is within
        /// the range of the last one, just merge/extend until we run into a new interval
        /// When we do, calculate the length and move on
        /// </summary>
        /// <param name="intervals"></param>
        /// <returns></returns>
        public static int GetTotalCoveredLength(List<int[]> intervals)
        {
            intervals.Sort((i, j) => i[0].CompareTo(j[0]));

            int range = 0;
            int[] lastInterval = intervals[0];

            for (int count = 1; count < intervals.Count; count++)
            {
                if (intervals[count][0] < lastInterval[1])
                {
                    //this can be merged
                    lastInterval[1] = System.Math.Max(lastInterval[1], intervals[count][1]);
                }
                else
                {
                    //add the last interval
                    range += lastInterval[1] - lastInterval[0];

                    //this is a new one
                    lastInterval = intervals[count];
                }
            }

            //for very last one;
            range += lastInterval[1] - lastInterval[0];

            return range;
        }

    }

}
