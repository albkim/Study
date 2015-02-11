using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Implementation
{
    public class IntegerPartition
    {

        public static List<List<int>> Partition(int number)
        {
            List<List<int>> partitions = new List<List<int>>();

            Partition(number, number, partitions, new List<int>());

            return partitions;
        }

        /// <summary>
        /// we want to get partitions like this for sum = 4
        /// 4
        /// 3 + 1
        /// 2 + 2
        /// 2 + 1 + 1
        /// 1 + 1 + 1 + 1
        /// </summary>
        /// <param name="sum"></param>
        /// <param name="largestNumber"></param>
        /// <param name="partitions"></param>
        private static void Partition(int sum, int largestNumber, List<List<int>> partitions, List<int> currentParitition)
        {
            if (sum <= 0)
            {
                //we crossed the end, we should finish this partition
                //3. add to partition 1 + 1 + 1 + 1
                //8. add to partition 2 + 1 + 1
                //11. add to partition 2 + 2
                //16. add to partition 3 + 1
                //19. add to partition 4
                partitions.Add(currentParitition);
                return;
            }

            if (largestNumber > 1)
            {
                //this will go
                //1. have 4, 4 -> 4, 3 -> 4, 2 -> 4, 1
                //4. process 4, 2
                //6. have 2, 2 -> 2, 1
                //9. process 2, 2
                //12. process 4, 3
                //14. have 1, 3 -> 1, 2 -> 1, 1
                //17. process 1, 2 -> process 1, 3
                //18. process 4, 4
                Partition(sum, largestNumber - 1, partitions, new List<int>(currentParitition));
            }
            
            if (sum >= largestNumber)
            {
                //first we will have 4, 1, so we will have
                //2. add 1 -> 3, 1 -> add 1 -> 2, 1 -> add 1 -> 1, 1 -> add 1 -> 0, 1
                //5. add 2 -> 2, 2
                //7. add 1 -> 1, 1 -> add 1 -> 0, 1
                //10. add 2 -> 0, 2
                //13. add 3 -> 1, 3 
                //15. add 1 -> 0, 1
                //19. add 4 -> 0, 4
                currentParitition.Add(largestNumber);
                Partition(sum - largestNumber, largestNumber, partitions, currentParitition);
            }
        }

        public static List<List<int>> CombinationSubSet(int[] array, int number)
        {
            List<List<int>> partitions = new List<List<int>>();

            CombinationSubSet(array, number, array.Length - 1, partitions, new List<int>());

            return partitions;
        }

        private static void CombinationSubSet(int[] array, int sum, int index, List<List<int>> partitions, List<int> currentParitition)
        {
            if (sum <= 0)
            {
                //we found the exact match
                partitions.Add(currentParitition);
                return;
            }

            int number = array[index];

            if (index > 0)
            {
                CombinationSubSet(array, sum, index - 1, partitions, new List<int>(currentParitition));
            }

            if (sum >= number)
            {
                currentParitition.Add(number);
                CombinationSubSet(array, sum - number, index, partitions, currentParitition);
            }
        }

    }
}
