using System;
using System.Collections.Generic;
using System.Linq;

namespace Algorithms.Implementation
{
    public class Array
    {

        public static List<List<int>> FindAllPossibleCombinations(int[] array, int total) {
            List<List<int>> validCombinations = new List<List<int>>();

            FindAllPossibleCombinations(array, 0, total, new List<int>(), validCombinations);

            return validCombinations;
        }

        private static void FindAllPossibleCombinations(int[] array, int currentTotal, int total, List<int> path, List<List<int>> validCombinations) {
            foreach (int number in array)
            {
                List<int> newPath = new List<int>(path);
                newPath.Add(number);
                if (currentTotal + number == total)
                {
                    validCombinations.Add(newPath);
                }
                else if (currentTotal + number < total)
                {
                    //if there is still a chance (less than total), then iterate one more
                    FindAllPossibleCombinations(array, currentTotal + number, total, newPath, validCombinations);
                }
            }
        }

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
    }

}
