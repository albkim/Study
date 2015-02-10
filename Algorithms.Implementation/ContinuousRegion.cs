using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Implementation
{
    public class ContinuousRegion
    {
        private readonly int[,] _matrix;

        public ContinuousRegion(int[,] matrix)
        {
            _matrix = matrix;
        }

        public int Count()
        {
            var count = 0;
            var hContinuous = false;

            for(var row = 0; row < _matrix.GetLength(0); row++)
            {
                for (var column = 0; column < _matrix.GetLength(1); column++)
                {
                    //we found a new region
                    if (_matrix[row, column] == 1)
                    {
                        //horizontally not continuous
                        if (!hContinuous)
                        {
                            hContinuous = true;
                            if ((row == 0) || (_matrix[row - 1, column] != 1))
                            {
                                //also vertically not continuous
                                count++;
                            }
                        }
                    }
                    else
                    {
                        hContinuous = false;
                    }
                }
            }

            return count;
        }
    }
}
