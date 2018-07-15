using System;
using System.Collections.Generic;

namespace MazeLibrary
{

    public class MazeSolver
    {
        private int[,] _labirint;
        private int _row;
        private int _column;
        private List<int> _coordsList = new List<int>();

        public MazeSolver(int[,] mazeModel, int startZ, int startColumn)
        {
            if (startZ < 0 || startColumn < 0)
            {
                throw new ArgumentException($"{nameof(startZ)}, {nameof(startColumn)}");
            }

            _labirint = mazeModel ?? throw new ArgumentNullException(nameof(mazeModel));
            _row = startZ;
            _column = startColumn;
        }

        public int[,] MazeWithPass()
        {
            int k = 0;
            int counter = 0;

            PassMaze();


            while (k != _coordsList.Count)
            {
                _labirint[_coordsList[k], _coordsList[k + 1]] = ++counter;
                k += 2;
            }


            return _labirint;
        }

        public void PassMaze()
        {
            while (_row != -1 && _column != -1)
            {
                _coordsList.Add(_row);
                _coordsList.Add(_column);
                PassHelper(_labirint, ref _row, ref _column, _coordsList);
            }

        }

        private bool IsSameWay(int row, int column, List<int> coord)
        {
            for (int i = 0; i < coord.Count - 2; i += 2)
            {
                if (row == coord[i] && column == coord[i + 1])
                {
                    return true;
                }
            }

            return false;
        }

        //private bool IsWrongWay(ref int row, ref int column, List<int> coord)
        //{
        //    if (row == -1 && column == -1)
        //    {
        //        if ()
        //        {
                    
        //        }
        //    }
        //}
        private void PassHelper(int[,] labirint, ref int row, ref int column, List<int> coords)
        {
            if ((column - 1) >= 0)
            {
                if (labirint[row, column - 1] == 0 && !IsSameWay(row, column - 1, coords))
                {
                    column = column - 1;
                    return;
                }

            }

            if ((column + 1) < labirint.GetLength(0))
            {
                if (labirint[row, column + 1] == 0 && !IsSameWay(row, column + 1, coords))
                {
                    column = column + 1;
                    return;
                }

            }

            if ((row - 1) >= 0)
            {
                if (labirint[row - 1, column] == 0 && !IsSameWay(row - 1, column, coords))
                {
                    row = row - 1;
                    return;
                }

            }

            if ((row + 1) < labirint.GetLength(1))
            {
                if (labirint[row + 1, column] == 0 && !IsSameWay(row + 1, column, coords))
                {
                    row = row + 1;
                    return;
                }

            }

            row = -1;
            column = -1;
        }
    }
}
