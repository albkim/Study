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

        #region Matrix Traverse

        /// <summary>
        /// A robot has to move in a grid which is in the form of a matrix. It can go to
        /// 1.) A(i,j)--> A(i+j,j) (Right)
        /// 2.) A(i,j)--> A(i,i+j) (Down)
        /// 
        /// Given it starts at (1,1) and it has to go to A(m,n), find the minimum number of STEPS it has to take to get to (m,n) and write
        /// public static int minSteps(int m,int n)
        /// 
        /// For instance to go from (1,1) to m=3 and n=2 it has to take (1, 1) -> (1, 2) -> (3, 2) i.e. 2 steps
        /// </summary>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int TraverseMatrix(int m, int n)
        {
            if ((m == 1) && (n == 1)) {
                return 0;
            }

            if (m < n)
            {
                //if m is smaller we can only go n - m
                return 1 + TraverseMatrix(m, n - m);
            }
            else
            {
                return 1 + TraverseMatrix(m - n, n);
            }
        }

        #endregion

        #region Discontinuous String

        /// <summary>
        /// Given two strings, find number of discontinuous matches.
        /// “cat”, “catapult”
        /// 
        /// 3 => “CATapult”, “CatApulT”, “CAtapulT”
        /// 
        /// Seems like i can do this, for each substring of the longer word, pass shorter string to see if i find a match
        /// If the character matches, pass substrings of both to see if there is a further match
        /// If shorter word has just one character left, then do contains and increment a count
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static int DiscontinuousStringRecursive(string s1, string s2)
        {
            if (s1.Length == 1)
            {
                return (s2.Contains(s1[0])) ? 1 : 0;
            }

            if (s2.Length == 1)
            {
                return 0;
            }

            int count = 0;

            if (s1[s1.Length - 1] == s2[s2.Length - 1])
            {
                count += DiscontinuousStringRecursive(s1.Substring(0, s1.Length - 1), s2.Substring(0, s2.Length - 1));
            }

            if (s2.Length > s1.Length)
            {
                count += DiscontinuousStringRecursive(s1, s2.Substring(0, s2.Length - 1));
            }

            return count;
        }

        /// <summary>
        ///             C   A   T   A   P   U   L   T
        ///         0   0   0   0   0   0   0   0   0 
        ///      C  0   1   1   1   1   1   1   1   1   
        ///      A  0   0   1   1   2   2   2   2   2
        ///      T  0   0   0   1   1   1   1   1   3
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns></returns>
        public static int DiscontinuousStringDynamic(string s1, string s2)
        {
            int[,] matrix = new int[s1.Length + 1, s2.Length + 1];

            for (int i = 0; i <= s1.Length; i++)
            {
                for (int j = 0; j <= s2.Length; j++)
                {
                    if ((i == 0) || (j == 0))
                    {
                        matrix[i, j] = 0;
                        continue;
                    }

                    if (i == 1)
                    {
                        matrix[i, j] = s2.Substring(0, j).Contains(s1[i - 1]) ? 1 : 0;
                        continue;
                    }

                    int count = 0;
                    if (s1[i - 1] == s2[j - 1])
                    {
                        count += matrix[i - 1, j - 1];
                    }

                    if (j > i)
                    {
                        count += matrix[i, j - 1];
                    }

                    matrix[i, j] = count;
                }
            }

            return matrix[s1.Length, s2.Length];
        }

        #endregion

        #region Raggedness of line

        /// <summary>
        /// Given the jaggedness as sum of squares of trailing spaces, find the optimal line breaks to reduce
        /// jaggedness
        /// 
        /// column size of 6
        /// aaa bb
        /// cc
        /// ddddd
        /// 
        /// or 
        /// 
        /// aaa
        /// bb cc
        /// ddddd
        /// 
        /// Seems like a simple DP problem
        /// 
        /// Take min between (adding it to previous line, removing last word and putting it together with current, or adding as a new line)
        /// 
        ///             1                2                            3       4
        ///             0                0                            0       0
        /// aaa     0   9                9                            9       9
        /// bb      0   0          min(9 - 9, 36 + 0, 9 + 16)         0       0
        /// cc      0   0          min(in, 0 + 9 + 1, 0 + 16)         10      10
        /// ddddd   0   0               0                             11      11
        /// </summary>
        /// <param name="words"></param>
        /// <param name="columnSize"></param>
        /// <returns></returns>
        public static List<string> ReduceRaggedness(string[] words, int columnSize)
        {
            int[,] matrix = new int[words.Length + 1, words.Length + 1];

            for (int i = 0; i <= words.Length; i++)
            {
                for (int j = 0; j <= words.Length; j++)
                {
                    if ((i == 0) || (j == 0))
                    {
                        matrix[i, j] = 0;
                        continue;
                    }

                    string word = words[i - 1];
                    
                    //if there is a room in previous (score ^ 1/2 >= word.length + space)
                    int scorePreviousLine = (System.Math.Sqrt(matrix[i - 1, j - 1]) >= (word.Length + 1)) ?
                        (int)System.Math.Pow(System.Math.Sqrt(matrix[i - 1, j - 1]) - word.Length - 1, 2) : int.MaxValue;
                    
                    //try removing the last word from last line and then combining with current
                    int scoreCombine = int.MaxValue;
                    if ((i > 1) && ((words[i - 2].Length + 1 + word.Length <= columnSize)))
                    {
                        scoreCombine = matrix[i - 2, j - 1] + (int)System.Math.Pow(columnSize - words[i - 2].Length - 1 - word.Length, 2);
                    }

                    //now just put it on the new line
                    int scoreNewLine = matrix[i - 1, j - 1] + (int)System.Math.Pow(columnSize - word.Length, 2);

                    matrix[i, j] = System.Math.Min(scorePreviousLine, System.Math.Min(scoreCombine, scoreNewLine));
                }
            }

            List<string> result = new List<string>();

            int lastWordIndex = 1;
            for (int line = 1; line <= words.Length; line++)
            {
                StringBuilder sb = new StringBuilder();
                
                //scan from the bottom and see what is the last work index;
                int wordIndex = words.Length;
                while ((wordIndex > 0) && (matrix[wordIndex, line] == 0))
                {
                    wordIndex--;
                }

                //append up to the word index
                for (int index = lastWordIndex; index <= wordIndex; index++)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(' ');
                    }
                    sb.Append(words[index - 1]);
                }
                result.Add(sb.ToString());
                lastWordIndex = wordIndex + 1;

                if (lastWordIndex >= words.Length)
                {
                    break;
                }
            }

            return result;
        }

        #endregion
    }

}
