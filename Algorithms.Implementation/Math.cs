using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Implementation
{
    public class Math
    {


        #region Random Number Expansion

        public static int RandomNumberExpandFiveToSeven()
        {
            Func<int> rand = () =>
                           {
                               var a = new Random();
                               return a.Next(5);
                           };

            var vector = new int[5,5]
                             {
                                 {1, 2, 3, 4, 5},
                                 {6, 7, 1, 2, 3},
                                 {4, 5, 6, 7, 1},
                                 {2, 3, 4, 5, 6},
                                 {7, 0, 0, 0, 0}
                             };
            //same change for each number to come up as long as 0 causes reroll

            var number = 0;
            while(number == 0)
            {
                number = vector[rand(), rand()];
            }
            return number;
        }

        #endregion

        #region Square Rroot

        public static double SquareRoot(double number, int precision)
        {
            //rough estimate
            //even D = 2n + 2 : 6 * 10n, odd D = 2n + 1 : 2 * 10n
            var estimate = 0d;
            var digits = System.Math.Floor(System.Math.Log10(number)) + 1;
            if (digits % 2 == 0)
            {
                estimate = 6 * System.Math.Pow(10, (digits - 2)/2d);
            }
            else
            {
                estimate = 2 * System.Math.Pow(10, (digits - 1) / 2d);
            }

            for(var count = 0; count < precision; count++)
            {
                estimate = Baylonian(number, estimate);
            }

            var resultDigits = (int)System.Math.Floor(System.Math.Log10(estimate)) + 1;
            return System.Math.Round(estimate, precision - resultDigits);
        }

        private static double Baylonian(double number, double estimate)
        {
            return (estimate + number / estimate) / 2d;
        }
        
        public static double SqrtBinarySearch(double number, double precision)
        {
            if (number < 0)
            {
                throw new ArgumentException();
            }

            double min = 0;
            double max = number;

            while (System.Math.Abs(max - min) > precision)
            {
                double mid = min + ((max - min) / 2);
                if ((System.Math.Pow(mid, 2)) > number)
                {
                    max = mid;
                }
                else
                {
                    min = mid;
                }
            }

            return System.Math.Round(min + ((max - min) / 2), (int)System.Math.Log10(1/precision));
        }

        public static double SqrtImprovedBinarySearch(double number, double precision)
        {
            double min = 0;
            double max = number;

            while (System.Math.Abs(max - min) > precision)
            {
                double mid = min + ((max - min) / 2);
                if ((System.Math.Pow(mid, 2)) > number)
                {
                    max = mid;
                    min = number / mid;
                }
                else
                {
                    min = mid;
                    max = number / mid;
                }
            }

            return System.Math.Round(min + ((max - min) / 2), (int)System.Math.Log10(1 / precision));
        }

        /// <summary>
        /// Rewrite mid = (min + max) / 2 using min & max value which are same in both cases
        /// </summary>
        /// <param name="number"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static double SqrtNewtonBinarySearch(double number, double precision)
        {
            if (number < 0)
            {
                throw new ArgumentException();
            }

            double mid = number;
            double previous = 0;

            while (System.Math.Abs(mid - previous) > precision)
            {
                previous = mid;
                mid = (mid + (number / mid)) / 2;
            }

            return System.Math.Round(mid, (int)System.Math.Log10(1 / precision)); ;
        }

        #endregion

        #region Triangular Numbers

        /// <summary>
        /// sum of all number up to n is
        /// 1+2+3+4+5
        /// 
        /// n (n + 1) / 2
        /// Triangular number
        /// 
        /// 1 - 3 = 3 * (3 + 1) / 2 = 6
        ///         x      y y y
        ///        x x      y y
        ///       x x x      y
        /// </summary>
        public static int TriangularNumber(int number)
        {
            return (number + 1) * number / 2;
        }

        /// <summary>
        /// Sum of all number between 1 - n, divisible by 3 & 5
        /// 
        /// 9 => 3 + 5 + 6 = 14
        /// 10 => 3 + 5 + 6 + 9 = 23
        /// 
        /// Think of this as a triangular number
        /// Let say N is 20
        /// the sum of numbers less than 20 and divisible by 3 are:
        /// 3 + 6 + 9 + 12 + 15 + 18 = (1+2+3+4+5+6)*3
        /// 
        /// This is the same for the sum of numbers less than 20 and divisible by 5.
        /// 5 + 10 + 15 = (1+2+3)*5 => (3*4/2)*5
        /// 
        /// As seen, we add 15 in both of operations, so we have to eliminate duplicates.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static int SumOfAllFactorOf3And5(int number)
        {
            int numberOf3 = (number - 1) / 3;
            int sumOf3 = (numberOf3 + 1) * numberOf3 / 2 * 3;
            
            int numberOf5 = (number - 1) / 5;
            int sumOf5 = (numberOf5 + 1) * numberOf5 / 2 * 5;

            int numberOf15 = (number - 1) / 15;
            int sumOf15 = (numberOf15 + 1) * numberOf15 / 2 * 15;

            return sumOf3 + sumOf5 - sumOf15;
        }

        #endregion

        #region Primality Test

        /// <summary>
        /// Test prime numbers
        /// after n > 3
        /// it's 6k +- 1
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool isPrime(long n)
        {
            if (n <= 3)
            {
                return n > 1;
            }
            else if (n % 2 == 0 || n % 3 == 0)
            {
                return false;
            }
            else
            {
                for (int i = 5; i * i <= n; i += 6)
                {
                    if (n % i == 0 || n % (i + 2) == 0)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        #endregion

        #region Swap without temporary variable

        public static void Swap(int x, int y)
        {
            x = x + y; //put both values into x to hold and now we can set y = x
            y = x - y; //(x + y) - y = x
            x = x - y; //(x + y) - y (now x) = y (old y)
        }

        #endregion
        
    }
}
