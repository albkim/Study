using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Implementation
{

    public class BinarySearch
    {

        #region Normal Search

        public static bool Search(int[] array, int number)
        {
            return Search(array, number, 0, array.Length - 1);
        }

        private static bool Search(int[] array, int number, int start, int end)
        {
            int pivotIndex = start + (int)System.Math.Floor((end - start) / 2d);
            int pivot = array[pivotIndex];

            if (pivot == number)
            {
                //we found the number
                return true;
            }
            else if ((number < pivot) && (start < pivotIndex))
            {
                //if number is on the left side and there is a room to go
                return Search(array, number, start, pivotIndex - 1);
            }
            else if ((number > pivot) && (end > pivotIndex))
            {
                //if the number is larger
                return Search(array, number, pivotIndex + 1, end);
            }

            //if we are here, we did not find it and ran out of numbers
            return false;
        }

        #endregion

        #region Rotated Array

        public static bool SearchRotatedArray(int[] array, int number)
        {
            return SearchRotatedArray(array, number, 0, array.Length - 1);
        }

        /// <summary>
        /// looks like 4, 5, 6, 7, 1, 2, 3
        /// if we are looking at 7, look at the beginning, if it's smaller than 7, we know the rotation happened to the right
        /// if we are looking at 1, look at the beginning, if it's bigger than 1 then we know rotation happened to the left
        /// </summary>
        /// <param name="array"></param>
        /// <param name="number"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        private static bool SearchRotatedArray(int[] array, int number, int start, int end)
        {
            int pivotIndex = start + (int)System.Math.Floor((end - start) / 2d);
            int pivot = array[pivotIndex];

            if (pivot == number)
            {
                //same as normal binary search, if we hit the number as the pivot, we have found it
                return true;
            }
            else if (number < pivot)
            {
                //we know we should go left in normal case, but we need some additional logic
                if ((array[start] <= number) && (start < pivotIndex))
                {
                    //we got lucky, left side is sequential and the number lies somewhere on the left
                    return SearchRotatedArray(array, number, start, pivotIndex - 1);
                }
                else if (end > pivotIndex)
                {
                    //else go right
                    return SearchRotatedArray(array, number, pivotIndex + 1, end);
                }
            }
            else if (number > pivot)
            {
                if ((array[end] >= number) && (end > pivotIndex))
                {
                    //we got lucky, right side is sequential and the number lies somewhere on the right
                    return SearchRotatedArray(array, number, pivotIndex + 1, end);
                }
                else if (start < pivotIndex)
                {
                    return SearchRotatedArray(array, number, start, pivotIndex - 1);
                }
            }

            return false;
        }

        #endregion

        #region Next Smallest

        public static int SearchNext(int[] array, int number)
        {
            int start = 0;
            int end = array.Length - 1;

            int next = int.MaxValue;

            while (start <= end)
            {
                int mid = start + (int)System.Math.Floor((end - start) / 2d);

                if (array[mid] <= number)
                {
                    //since we are looking for a number larger than the number, we can discard left
                    start = mid + 1;
                }
                else
                {
                    if ((array[mid] > number) && (array[mid] < next))
                    {
                        next = array[mid];
                    }

                    //since we captured the smallest number larger than the number, we can now discard
                    end = mid - 1;
                }
            }

            if (next == int.MaxValue)
            {
                //cannot find...all numbers smaller than number
                throw new ArgumentException();
            }

            return next;
        }

        #endregion

    }

}
