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

            for(var row = 0; row < _matrix.GetLength(0); row++)
            {
                for (var column = 0; column < _matrix.GetLength(1); column++)
                {
                    //we found a new region
                    if (_matrix[row, column] == 1)
                    {
                        //we need to DFS all direction and set the other connected regions to 0
                        int[,] check = new int[_matrix.GetLength(0), _matrix.GetLength(1)];
                        MarkRegion(row, column, check, false);
                        count++;
                    }
                }
            }

            return count;
        }

        private void MarkRegion(int row, int column, int[,] check, bool markCurrentCell)
        {
            check[row, column] = 1;
            if (_matrix[row, column] == 1)
            {
                if (markCurrentCell)
                {
                    _matrix[row, column] = 0;
                }
                if ((row > 0) && (check[row - 1, column] == 0))
                {
                    MarkRegion(row - 1, column, check, true);
                }
                if ((row < (_matrix.GetLength(0) - 1)) && (check[row + 1, column] == 0))
                {
                    MarkRegion(row + 1, column, check, true);
                }
                if ((column > 0) && (check[row, column - 1] == 0))
                {
                    MarkRegion(row, column - 1, check, true);
                }
                if ((column < (_matrix.GetLength(1) - 1)) && (check[row, column + 1] == 0))
                {
                    MarkRegion(row, column + 1, check, true);
                }
            }
        }
    }
}
