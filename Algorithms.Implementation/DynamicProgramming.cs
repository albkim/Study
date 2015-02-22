using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Implementation
{
    
    public class DynamicProgramming
    {

        #region Knapsack

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

        #endregion

        #region Edit Distance
        
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
        ///         0   1   2   3   4   5   6   7   8   
        ///     S   1   0   1   2   3   4   5   6   7
        ///     U   2   1   1   2   2   3   4   5   6
        ///     N   3   2   2   2   3   3   4   5   6
        ///     D   4   3   3   3   3   4   3   4   5
        ///     A   5   4   3   4   4   4   4   3   4
        ///     Y   6   5   4   4   5   5   5   4   3
        /// </summary>
        /// <param name="string1"></param>
        /// <param name="string2"></param>
        /// <returns></returns>
        public static int EditDistanceDynamicProgramming(string string1, string string2)
        {
            int[,] distance = new int[string1.Length + 1, string2.Length + 1];

            //base case
            //for all length of 0, edit distance is equal to length of the other string
            for (int count = 1; count <= string1.Length; count++)
            {
                distance[count, 0] = count;
            }
            for (int count = 1; count <= string2.Length; count++)
            {
                distance[0, count] = count;
            }

            //build bottom up use the same logic as recursive method
            for (int i = 1; i <= string1.Length; i++)
            {
                for (int j = 1; j <= string2.Length; j++)
                {
                    distance[i, j] = System.Math.Min(System.Math.Min(
                        distance[i - 1, j - 1] + (string1[i - 1] == string2[j - 1] ? 0 : 1),
                        distance[i - 1, j] + 1),
                        distance[i, j - 1] + 1);
                }
            }

            return distance[string1.Length, string2.Length];
        }

        #endregion

        #region Longest Subsequence

        /// <summary>
        /// Not common substring which has to be consecutive
        /// Given two strings like BANANA & ATANA answer is AANA and there are 3 cases
        ///     If last characters are the same, then we can remove and continue comparing
        ///            BANAN, ATAN => BANA, ATA => BAN, AT and come up with ANA
        ///     If last characters are different then try variation by removing last character
        ///         BAN, AT => BA, AT => BA, A and come up with A
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static string LongestSubstringRecursive(string s1, string s2)
        {
            return LongestSubstringRecursive(s1, s2, s1.Length, s2.Length);
        }

        private static string LongestSubstringRecursive(string s1, string s2, int length1, int length2)
        {
            StringBuilder subsequence = new StringBuilder();

            if ((length1 == 0) || (length2 == 0))
            {
                return string.Empty;
            }

            if (s1[length1 - 1] == s2[length2 - 1])
            {
                subsequence.Append(LongestSubstringRecursive(s1, s2, length1 - 1, length2 - 1));
                subsequence.Append(s1[length1 - 1]);
            }
            else
            {
                string subsequence1 = LongestSubstringRecursive(s1, s2, length1 - 1, length2);
                string subsequence2 = LongestSubstringRecursive(s1, s2, length1, length2 - 1);

                subsequence.Insert(0, (subsequence1.Length > subsequence2.Length) ? subsequence1 : subsequence2);
            }

            return subsequence.ToString();
        }

        /// <summary>
        ///             B       A       N       A       N       A
        ///             ""      ""      ""      ""      ""      ""
        /// A      ""   ""      A       A       A       A       A
        /// T      ""   ""      A       A       A       A       A
        /// A      ""   ""      A       A       AA      AA      AA
        /// N      ""   ""      A      AN      AN/AA    AAN     AAN
        /// A      ""   ""      A      AN       ANA    ANA/AAN  AANA
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static string LongestSubstringDynamic(string s1, string s2)
        {
            string[,] matrix = new string[s1.Length + 1, s2.Length + 1];

            for (int i1 = 0; i1 <= s1.Length; i1++)
            {
                for (int i2 = 0; i2 <= s2.Length; i2++)
                {
                    if ((i1 == 0) || (i2 == 0))
                    {
                        matrix[i1, i2] = string.Empty;
                        continue;
                    }

                    if (s1[i1 - 1] == s2[i2 - 1]) {
                        matrix[i1, i2] = matrix[i1 - 1, i2 - 1] + s1[i1 - 1];
                    }
                    else {
                        string substring1 = matrix[i1 - 1, i2];
                        string substring2 = matrix[i1, i2 - 1];
                        
                        matrix[i1, i2] = ((substring1.Length > substring2.Length) ? substring1 : substring2);
                    }
                }
            }

            return matrix[s1.Length, s2.Length];
        }

        #endregion

    }

}
