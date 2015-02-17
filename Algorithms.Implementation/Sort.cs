using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Implementation
{
    
    public class Sort
    {

        public static void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }

        private static void QuickSort(int[] array, int start, int end) {
            //now element at pivotIndex is in correct position
            int pivotIndex = Partition(array, start, end);

            //if there are more elements to the left, recurse
            if ((pivotIndex - 1) > start)
            {
                QuickSort(array, start, pivotIndex - 1);
            }

            //if there are more elements to the right, recurse
            if ((pivotIndex + 1) < end)
            {
                QuickSort(array, pivotIndex + 1, end);
            }
        }

        /// <summary>
        /// arranges the numbers such that all elements left of the pivot is smaller or equal than the pivot
        /// and all elements right of the pivot is larger than the pivot
        /// returns the pivot index
        /// </summary>
        /// <param name="array"></param>
        /// <param name="start">index of start</param>
        /// <param name="end">index of end</param>
        /// <returns></returns>
        private static int Partition(int[] array, int start, int end)
        {
            //we could randomly choose here to get better run time
            int pivot = array[start];

            int left = start + 1;
            int right = end;

            //do until we meet
            while (left < right)
            {
                //now if the condition is violated, swap left and right
                while (array[left] <= pivot)
                {
                    left++;
                }

                while (array[right] > pivot)
                {
                    right--;
                }

                if (left < right)
                {
                    swap(array, left, right);
                }
            }

            //put the pivot in the right place
            swap(array, start, right);

            return right;
        }

        private static void swap(int[] array, int left, int right)
        {
            int temp = array[left];
            array[left] = array[right];
            array[right] = temp;
        }

    }

}
