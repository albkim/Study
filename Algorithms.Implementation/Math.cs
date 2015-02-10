﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Implementation
{
    public class Math
    {

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

    }
}
