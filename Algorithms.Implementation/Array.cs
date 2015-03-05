﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Implementation
{
    public class Array
    {

        #region Find kth Smallest using Quick Select

        public static int FindKthSmallest(int[] array, int k)
        {
            if (array.Length == 1)
            {
                return array[0];
            }

            var start = 0;
            var end = array.Length;
            var kIndex = k - 1;

            while(true)
            {
                var pivotIndex = (start + end)/2;
                var newPivotIndex = Partition(array, start, end, pivotIndex);

                if (newPivotIndex == kIndex)
                {
                    //we found it
                    return array[kIndex];
                }
                else if (kIndex < newPivotIndex)
                {
                    end = newPivotIndex;
                }
                else if (kIndex > newPivotIndex)
                {
                    start = newPivotIndex + 1;
                }
            }
        }

        private static int Partition(int[] array, int start, int end, int pivotIndex)
        {
            var pivotValue = array[pivotIndex];
            
            //mode pivot to the end so we don't have to process it
            swap(array, pivotIndex, end - 1);

            var processedIndex = 0;
            for(var index = start; index < end; index++)
            {
                if (array[index] < pivotValue)
                {
                    swap(array, processedIndex, index);
                    processedIndex++;
                }
            }

            //move pivot back
            swap(array, processedIndex, end - 1);
            return processedIndex;
        }

        private static void swap(int[] array, int index1, int index2)
        {
            int tmp = array[index2];
            array[index2] = array[index1];
            array[index1] = tmp;
        }

        #endregion

        #region Find Missing Element

        public static int FindMissingElementSum(IEnumerable<int> original, IEnumerable<int> shuffled)
        {
            return original.Sum() - shuffled.Sum();
        }

        public static int FindMissingElementXor(IEnumerable<int> original, IEnumerable<int> shuffled)
        {
            var result = 0;

            foreach(var number in original)
            {
                result = result ^ number;
            }
            foreach (var number in shuffled)
            {
                result = result ^ number;
            }

            return result;
        }

        #endregion

        #region Get Number Adding Up To A Sum

        public static IList<int[]> GetNumberAddingUpToSum(IEnumerable<int> numbers, int sum)
        {
            var result = new List<int[]>();
            var lookup = new Dictionary<int, int>();

            if (numbers != null)
            {
                foreach (var number in numbers)
                {
                    int? pair = null;

                    if (lookup.ContainsKey(sum - number))
                    {
                        pair = sum - number;
                    }

                    if (pair.HasValue)
                    {
                        result.Add(new[] { pair.Value, number });
                    }

                    lookup[number] = number;
                }
            }

            return result;
        }

        /// <summary>
        /// This involves sorting the array and carefully moving left and right pointer
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public static List<int[]> TwoSumNoSpace(int[] numbers, int sum)
        {
            List<int[]> result = new List<int[]>();

            // n log n already
            System.Array.Sort(numbers);

            int i = 0;
            int j = numbers.Length - 1;

            while (i < j)
            {
                if (sum == (numbers[i] + numbers[j]))
                {
                    //we found one
                    result.Add(new int[] { numbers[i], numbers[j] });

                    i++;
                    j--;
                }
                else if (sum > (numbers[i] + numbers[j]))
                {
                    //if sum is larger, only way is to increase meaning we want to move left pointer
                    i++;
                }
                else
                {
                    j--;
                }
            }

            return result;
        }

        #endregion

        #region 3 Sum

        /// <summary>
        /// Two ways of solving this
        /// 
        ///     n^2 with n space, store all numbers in a dictionary and then search for 2 pairs + remainder
        ///     
        ///     n^2 with no space, for every number, do two sum with remainder
        ///         "-25" '-10' -7 -3 2 4 8 $10$  (a+b+c==-25)
        ///         "-25" -10 '-7' -3 2 4 8 $10$  (a+b+c==-22)
        ///         "-25" -10 -7 -3 2 4 '8' $10$  (a+b+c==-7)
        ///         
        ///         -25 "-10" -7 -3 '2' 4 8 $10$  (a+b+c==2)
        ///         -25 "-10" -7 -3 '2' 4 $8$ 10  (a+b+c==0)
        /// </summary>
        /// <returns></returns>
        public static List<int[]> ThreeSum(int[] numbers, int sum)
        {
            List<int[]> result = new List<int[]>();

            System.Array.Sort(numbers);

            for (int count = 0; count < numbers.Length; count++)
            {
                int i = count + 1;
                int j = numbers.Length - 1;
                int remainder = sum - numbers[count];

                while (i < j)
                {
                    if (remainder == (numbers[i] + numbers[j]))
                    {
                        //we found one
                        result.Add(new int[] { numbers[count], numbers[i], numbers[j] });

                        i++;
                        j--;
                    }
                    else if (remainder > (numbers[i] + numbers[j]))
                    {
                        //if sum is larger, only way is to increase meaning we want to move left pointer
                        i++;
                    }
                    else
                    {
                        j--;
                    }
                }
            }

            return result;
        }

        #endregion

        #region Get Largest Continuous Sum

        public static int GetLargestContinuousSum(IEnumerable<int> numbers)
        {
            var result = 0;

            if (numbers != null)
            {
                var runningCount = 0;
                foreach(var number in numbers)
                {
                    runningCount += number;
                    if (runningCount < 0)
                    {
                        runningCount = 0;
                    }

                    if (runningCount > result)
                    {
                        result = runningCount;
                    }
                }
            }

            return result;
        }

        #endregion

        #region Largest Continuous Product

        /// <summary>
        /// This is similar to continuous some but slightly more complex due to handling of 0 and negative numbers
        /// {6, -3, -10, 0, 2}
        /// 180  // The subarray is {6, -3, -10}
        /// 
        /// {-1, -3, -10, 0, 60}
        /// 60  // The subarray is {60}
        /// 
        /// {-2, -3, 0, -2, -40}
        /// 80  // The subarray is {-2, -40}
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int LargestContinuousProduct(int[] numbers)
        {
            int min = 1;
            int max = 1;

            int finalMax = int.MinValue;

            foreach (int number in numbers)
            {
                if (number > 0)
                {
                    //positive number
                    //max = previous max * number...assume we will always keep max > 0
                    max = max * number;
                    //min = previous min * number...assume we will always keep min < 0 or 1
                    min = System.Math.Min(min * number, 1);
                }
                else if (number < 0)
                {
                    //negative number
                    //backup max before we overwrite
                    int tempMax = max;
                    //max = previous negative number (min) * current number will give positive max
                    max = System.Math.Max(min * number, 1);
                    //min = previous max number * current number
                    min = tempMax * number;
                }
                else
                {
                    //0 case, reset to 1 so we can still calculate product next iteration
                    min = 1;
                    max = 1;
                }

                if (max > finalMax)
                {
                    finalMax = max;
                }
            }

            return finalMax;
        }

        #endregion

        #region Equilibrium

        /// <summary>
        /// Find the index where sum of left side is equal to sum of right side
        /// 
        /// -7, 1, 5, 2, -4, 3, 0
        /// index 3
        /// 
        /// Get the sum of all numbers first, then as we iterate, calculate left sum and
        /// right sum (total sum - left sum)
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int FindEquilibrium(int[] array)
        {
            int sum = array.Sum();

            int leftSum = 0;

            for (int index = 0; index < array.Length; index++)
            {
                //right sum need to subtract current cell
                sum -= array[index];

                //we want to compare excluding current cell (so right sum should subtract but left
                //sum shouldn't have added)
                if (sum == leftSum)
                {
                    return index;
                }

                leftSum += array[index];
            }

            //not found
            return -1;
        }

        #endregion

        #region Minimum Difference Two Array

        /// <summary>
        /// Given two sorted arrays find min(abs(x-y)) where x and y are one elements from each array
        /// 
        /// Seems like I need to compare x < y < x + 1
        /// 
        /// Works for all numbers
        /// 3 & 4 = 1, 4 & 3 = 1, -4 & -3 = 1
        /// 
        /// Doesn't break in negative case
        /// -4 & 5 = 9
        /// </summary>
        /// <param name="array1"></param>
        /// <param name="array2"></param>
        /// <returns></returns>
        public static int MinimumDifference(int[] array1, int[] array2)
        {
            int minimum = int.MaxValue;

            int secondIndex = 0;
            for (int firstIndex = 0; firstIndex < array1.Length; firstIndex++)
            {
                while ((secondIndex < array2.Length) && (array1[firstIndex] > array2[secondIndex]))
                {
                    secondIndex++;
                }

                //since second array is not equal or greater
                //minimum of existing minimum, x - 1 and x
                minimum = System.Math.Min(
                    minimum, System.Math.Min(
                    System.Math.Abs((secondIndex < array2.Length) ? array1[firstIndex] - array2[secondIndex] : int.MaxValue),
                    System.Math.Abs((secondIndex > 0) ? array1[firstIndex] - array2[secondIndex - 1] : int.MaxValue)));
            }

            return minimum;
        }

        #endregion

        #region Sum Of Nested Integers

        /// <summary>
        /// Given a nested list of integers, returns the sum of all integers in the list weighted by their depth 
        /// For example, given the list {{1,1},2,{1,1}} the function should return 10 (four 1's at depth 2, one 2 at depth 1) 
        /// Given the list {1,{4,{6}}} the function should return 27 (one 1 at depth 1, one 4 at depth 2, and one 6 at depth 3) 
        /// </summary>
        /// <param name="integers"></param>
        /// <returns></returns>
        public static int SumNestedIntegers(IEnumerable<object> integers)
        {
            return SumNestedIntegers(integers, 1);
        }

        private static int SumNestedIntegers(IEnumerable<object> integers, int level)
        {
            int sum = 0;
            foreach (object o in integers)
            {
                if (o is IEnumerable<object>)
                {
                    sum += SumNestedIntegers((IEnumerable<object>)o, level + 1);
                }
                else
                {
                    sum += ((int)o * level);
                }
            }
            return sum;
        }

        #endregion

        #region Triangle

        /// <summary>
        /// Three segments of lengths A, B, C form a triangle if
        ///     A + B > C
        ///     B + C > A
        ///     A + C > B
        /// 
        /// e.g.
        ///     6, 4, 5 can form a triangle
        ///     10, 2, 7 can't
        ///     
        /// Given a list of segments lengths algorithm should find at least one triplet of segments that form a triangle (if any).
        /// 
        /// Naive way would be n^3 solution where we try every combination of sides
        /// This can be optimized to n^2 if we sort the array
        ///     Sort the array
        ///     Scan through 1st side
        ///     Scan through 2nd side
        ///     Knowing two find the last element where it's below the sum of first two sides
        ///     
        ///     1, 2, 3, 4, 5, 6, 7
        ///     i = 1, j = 2, k = 3
        ///     i = 1, j = 3, k = 3, and still no match,
        ///     i = 1, j = 3, k = 4, no good what we have is not a valid triangle (1, 3, 4), moving on
        ///     i = 1, j = 4, k = 5, (1, 4, 5) still not valid
        ///     i = 1, j = 5, k = 6, (1, 5, 6) no good
        ///     i = 2, j = 3, k = 4, (2, 3, 4) 2+3 > 4, 3+4 > 2, 2+4 > 3
        ///     i = 2, j = 3, k = 5, (2, 3, 5) no good
        ///     i = 2, j = 4, k = 6, (2, 4, 6) no good
        ///     i = 3, j = 4, k = 5, (3, 4, 5) 3+4 > 5, 3+5 > 4, 4+5 > 3
        ///     i = 3, j = 4, k = 6, (3, 4, 6) 2+4 > 6, 3+6 > 4, 
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static List<List<int>> NumTriangle(int[] array)
        {
            //sort the array
            System.Array.Sort(array);

            List<List<int>> result = new List<List<int>>();

            //0..n-2
            for (int i = 0; i < (array.Length - 2); i++)
            {
                int k = i + 2;
                for (int j = i + 1; j < (array.Length - 1); j++)
                {
                    while ((k < array.Length) && (array[i] + array[j] > array[k]))
                    {
                        //we already check i + j > k, so check the other two
                        if (((array[i] + array[k] > array[j]) && (array[j] + array[k] > array[i])) &&
                            //also check that k is not being reused
                            (i != k && j != k))
                        {
                            result.Add(new List<int> { array[i], array[j], array[k] });
                        }
                        k++;
                    }

                }
            }

            return result;
        }

        #endregion

        #region Number of columns

        /// <summary>
        /// Given an array display the numbers in the number of columns. Each column should contain similar
        /// number of numbers
        /// 
        /// 1, 2, 3, 4, 5, 6, 7
        /// 
        /// 1,4,6
        /// 2,5,7
        /// 3
        /// 
        /// we want the size different of each column to be at most 1
        /// so number elements floor(length / num column)
        /// first (length % num column) columns should have extra
        /// </summary>
        /// <param name="array"></param>
        /// <param name="numColumn"></param>
        /// <returns></returns>
        public static List<List<int>> DisplayInNumberOfColumns(int[] array, int numColumn)
        {
            List<List<int>> result = new List<List<int>>();

            int numElements = (int)System.Math.Floor(array.Length / numColumn * 1d);
            int numColumnsWithExtra = array.Length % numColumn;

            for (int row = 0; row < (numElements + (numColumnsWithExtra > 0 ? 1 : 0)); row++)
            {
                List<int> line = new List<int>();

                int columnMax = (row < numElements) ? numColumn : numColumnsWithExtra;
                for (int column = 0; column < columnMax; column++)
                {
                    int columnIndex = (column * numElements) + System.Math.Min(numColumnsWithExtra, column);
                    int index = columnIndex + row;
                    line.Add(array[index]);
                }
                result.Add(line);
            }

            return result;
        }

        #endregion

        #region Window Sum

        /// <summary>
        /// Given a list of integers and a window size, return a new list of integers where each integer is the sum 
        /// of all integers in the kth window of the input list. The kth window of the input list is the integers from index
        /// k to index k + window size -1 (inclusive).
        /// 
        /// For example,[4,2,73,11,-5] and the window size 2 should return [6,75,84,6]. For another example.[4,2,73,11,-5] and
        /// window size 3 should return [79, 86, 79].
        /// 
        /// Seems like we can run a cumulative sum and subtract the sum of the current - windwsize (at 4, subtract 3, and get sum at 1)
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="windowSize"></param>
        /// <returns></returns>
        public static int[] WindowSum(int[] numbers, int windowSize)
        {
            List<int> result = new List<int>();

            int cumulativeSum = 0;

            for (int index = 0; index < numbers.Length; index++)
            {
                cumulativeSum += numbers[index];
                result.Add(cumulativeSum);
            }

            //need to subtract later since we need to previous cumulative sum to calculate diff correctly
            for (int index = numbers.Length - 1; index >= windowSize; index--)
            {
                result[index] -= result[index - windowSize];
            }

            //need to skip first window - 1 since those are partial sums
            return result.Skip(windowSize - 1).ToArray();
        }

        #endregion

        #region Minimum sub array to make sorted array

        /// <summary>
        /// Given an unsorted array arr[0..n-1] of size n, find the minimum length subarray arr[s..e]
        /// such that sorting this subarray makes the whole array sorted.
        /// 
        /// If the input array is [10, 12, 20, 30, 25, 40, 32, 31, 35, 50, 60], 
        /// your program should be able to find that the subarray lies between the indexes 3 and 8.
        /// 
        /// If the input array is [0, 1, 15, 25, 6, 7, 30, 40, 50],
        /// your program should be able to find that the subarray lies between the indexes 2 and 5.
        /// 
        /// Identify candidate
        /// sub array where left side is decreasing and right side is increasing
        /// [30, 25, 40, 32, 31]
        /// 
        /// Find the min and max from the sub array
        /// 
        /// See if there is anything larger than the min in left side
        /// See if there is anything smaller than the max in the right side
        /// 
        /// Sub array is the from the new min and new max
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int[] MinimumSubArray(int[] array)
        {
            //Find candidate sub array
            int index = 1;
            while ((index < array.Length) && (array[index - 1] < array[index]))
            {
                index++;
            }

            int leftIndex = index - 1;

            index = array.Length - 2;
            while ((index >= 0) && (array[index] < array[index + 1]))
            {
                index--;
            }

            int rightIndex = index + 1;

            if (leftIndex < rightIndex)
            {
                //we have found a candidate, find min and max
                int min = int.MaxValue;
                int max = int.MinValue;
                for (int count = leftIndex; count <= rightIndex; count++)
                {
                    if (array[count] < min)
                    {
                        min = array[count];
                    }
                    if (array[count] > max)
                    {
                        max = array[count];
                    }
                }

                //shift index to left if there is one larger than the min
                while ((leftIndex - 1 > 0) && (array[leftIndex - 1] > min))
                {
                    leftIndex--;
                }

                //shift index to right
                while ((rightIndex + 1 < array.Length) && (array[rightIndex + 1] < max))
                {
                    rightIndex++;
                }

                return new int[] { leftIndex, rightIndex };
            }

            return null;
        }

        #endregion

        #region Count Inversion

        /// <summary>
        /// Count elements out of order
        /// 
        /// 1, 3, 5, 2, 4, 6
        /// 
        /// Inversions are
        /// (3, 2), (5, 2), (5, 4)
        /// 
        /// What if we do merge sort
        /// 1, 3, 5
        /// 2, 4, 6
        /// 
        /// start with left side, for every number, any traverse on the right side is an inversion
        /// we have an inversion
        /// 
        /// 1 3 (2) 5 (2, 4)
        ///   2     4        6
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static int CountInversion(int[] numbers)
        {
            int[] sortedNumbers;

            return CountInversionR(numbers, out sortedNumbers);
        }

        private static int CountInversionR(int[] numbers, out int[] sortedNumbers)
        {
            sortedNumbers = new int[numbers.Length];

            if (numbers.Length == 1)
            {
                sortedNumbers[0] = numbers[0];
                return 0;
            }

            int[] sortedLeft;
            int[] sortedRight;

            int mid = (int)System.Math.Floor(numbers.Length / 2d);
            int leftCount = CountInversionR(numbers.Take(mid).ToArray(), out sortedLeft);
            int rightCount = CountInversionR(numbers.Skip(mid).ToArray(), out sortedRight);

            int index = 0;
            int rightIndex = 0;
            int mergeCount = 0;
            int inversionCount = 0;
            foreach (int number in sortedLeft)
            {
                while ((rightIndex < sortedRight.Length) && (sortedRight[rightIndex] < number))
                {
                    sortedNumbers[index] = sortedRight[rightIndex];

                    index++;
                    rightIndex++;
                    mergeCount++;
                }

                sortedNumbers[index] = number;
                index++;
                inversionCount += mergeCount;
            }

            for (int rest = rightIndex; rest < sortedRight.Length; rest++)
            {
                sortedNumbers[index] = sortedRight[rest];
                index++;
            }

            return leftCount + rightCount + inversionCount;
        }

        #endregion     

        #region Largest Number Formed from Array

        /// <summary>
        /// For example, given [3, 30, 34, 5, 9], the largest formed number is 9534330.
        /// 
        /// Seems like we just line up the number from the highest digit 9 -> 0. The tricky part is
        /// multiple digit numbers; 9 should come before 34
        /// 
        /// What if we cast everything to string and just do string sort
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        public static int LargestNumber(int[] numbers)
        {
            string[] stringNumbers = (from n in numbers select n.ToString()).ToArray();

            System.Array.Sort(stringNumbers);

            StringBuilder result = new StringBuilder();
            for (int index = stringNumbers.Length - 1; index >= 0; index--) 
            {
                result.Append(stringNumbers[index]);
            }

            return int.Parse(result.ToString());
        }

        #endregion

        #region Number of ways color rgb

        /// <summary>
        /// Given n boards, find the number of ways of coloring each board with rgb
        /// No three adjacent board can have the same color (rrr, ggg, bbb)
        /// 
        /// r   r   b   r
        ///             g
        ///             b
        ///         g   r
        ///             g
        ///             b
        ///     g   r   r
        ///             g
        ///             b
        ///         g   r
        ///             b
        ///         b   r
        ///             g
        ///             b
        ///     b   r   r
        ///             g
        ///             b
        ///         g   r
        ///             g
        ///             b
        ///         b   r
        ///             g
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int NumberOfWaysColor(int number)
        {
            return NumberOfWaysColor(number, 'a', new List<char> { 'r', 'g', 'b' });
        }

        private static int NumberOfWaysColor(int number, char previousOne, List<char> characters)
        {
            if (number == 1)
            {
                return characters.Count;
            }

            int count = 0;

            foreach (char color in characters)
            {
                List<char> availableCharacters = new List<char> { 'r', 'g', 'b' };
                if (color == previousOne)
                {
                    availableCharacters.Remove(color);
                }
                count += NumberOfWaysColor(number - 1, color, availableCharacters);
            }

            return count;
        }

        #endregion

    }

}
