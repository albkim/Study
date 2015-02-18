using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Implementation
{
    
    public class DynamicProgramming
    {

        /// <summary>
        /// 0 - 1 knapsack where the item can be included or not included
        /// <image url="$(SolutionDir)\Images\knapsack.png" />
        /// </summary>
        /// <param name="total"></param>
        /// <param name="weights"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static int Knapsack(int capacity, int[] weights, int[] values, List<int> choices)
        {
            int[,] Value = new int[weights.Length + 1, capacity + 1];

            for (int item = 0; item <= weights.Length; item++)
            {
                for (int space = 0; space <= capacity; space++)
                {
                    if ((item == 0) || (space == 0))
                    {
                        //initialize
                        Value[item, space] = 0;
                        continue;
                    }

                    int currentWeight = weights[item - 1];
                    int currentValue = values[item - 1];

                    if (currentWeight <= space)
                    {
                        //now this item may be valid since it's smaller than available space
                        //cost is max of the following
                        //if current item is included, then n-1 item + current item with W minus current weight
                        //if current item is not included then n-1 item and W
                        Value[item, space] = System.Math.Max(Value[item - 1, space - currentWeight] + currentValue, Value[item - 1, space]);
                    }
                    else
                    {
                        //this item is not valid...keep the last one
                        Value[item, space] = Value[item - 1, space];
                    }
                }
            }

            int weight = capacity;
            for (int item = weights.Length; item > 0; item--)
            {
                if (Value[item, weight] != Value[item - 1, weight])
                {
                    //if the value here is equal to the previous item's max, that means i did not take this item
                    //if they are not equal, that means i took this item
                    choices.Add(weights[item - 1]);
                }
            }

            return Value[weights.Length, capacity];
        }

        /// <summary>
        /// +1 for delete, insertion and substitution
        /// e.g. Saturday & Sunday has 3
        /// S a(i) t(i) u r(n -> r) day
        /// </summary>
        /// <param name="string1"></param>
        /// <param name="string2"></param>
        /// <returns></returns>
        public static int EditDistanceRecursive(string string1, string string2)
        {
            return EditDistanceRecursive(string1, string2, string1.Length, string2.Length);
        }

        private static int EditDistanceRecursive(string string1, string string2, int length1, int length2)
        {
            // if one is shorter, then we need to make as many changes as the other string
            if (length1 == 0)
            {
                return length2;
            } 
            if (length2 == 0)
            {
                return length1;
            }

            //now we have three cases
            //cost of current letter which could be 0 or 1 and added with cost of all previous letters (abcd <> abce)
            //cost of removing one letter (abcd <> abc)
            //cost of adding one letter (abc <> abcd)
            //then we just have to recursively do this for each length
            return System.Math.Min(System.Math.Min(
                EditDistanceRecursive(string1, string2, length1 - 1, length2 - 1) + ((string1[length1 - 1] == string2[length2 - 1]) ? 0 : 1),
                EditDistanceRecursive(string1, string2, length1 - 1, length2) + 1), //since we removed one, we have to take the cost
                EditDistanceRecursive(string1, string2, length1, length2 - 1) + 1 //this is same as adding one to string 1
            );
        }

        /// <summary>
        ///             S   A   T   U   R   D   A   Y
        ///     S       0   1   2   3   4   5   6   7
        ///     U       1   1   2   2   3   4   5   6
        ///     N       2   2   3   3   4   5   6   7
        ///     D       3   3   3   3   4   4   5   6
        ///     A       4
        ///     Y       5
        /// </summary>
        /// <param name="string1"></param>
        /// <param name="string2"></param>
        /// <returns></returns>
        public static int EditDistanceDynamicProgramming(string string1, string string2)
        {

        }

    }

}
