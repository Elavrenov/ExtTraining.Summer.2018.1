using System;
using System.Collections.Generic;

namespace MazeLibrary
{

    public class MazeSolver
    {
        private int[,] _labirint;
        private int _x;
        private int _y;
        private int[,] _passLabirint;
        private List<int> _coordsList = new List<int>();

        public MazeSolver(int[,] mazeModel, int startZ, int startY)
        {
            if (startZ < 0 || startY < 0)
            {
                throw new ArgumentException($"{nameof(startZ)}, {nameof(startY)}");
            }

            _labirint = mazeModel ?? throw new ArgumentNullException(nameof(mazeModel));
            _x = startZ;
            _y = startY;
        }

        public int[,] MazeWithPass()
        {
            _passLabirint = new int[_labirint.GetLength(1), _labirint.GetLength(1)];

            int k = 0;
            int counter = 0;
            for (int i = 0; i < _labirint.GetLength(1); i++)
            {
                for (int j = 0; j < _labirint.GetLength(0); j++)
                {
                    _passLabirint[i, j] = _labirint[i, j];
                }
            }

            PassMaze();


            while (k != _coordsList.Capacity - 2)
            {
                _passLabirint[_coordsList[k], _coordsList[++k]] = ++counter;
                k++;
            }


            return _passLabirint;
        }

        public void PassMaze()
        {
            while (_x != -1 && _y != -1)
            {
                _coordsList.Add(_x);
                _coordsList.Add(_y);
                PassHelper(_labirint, ref _x, ref _y, _coordsList);
            }

        }

        private bool IsSameWay(int x, int y, List<int> coord)
        {
            for (int i = 0; i < coord.Capacity - 2; i++)
            {
                if (x == coord[i] && y == coord[++i])
                {
                    return true;
                }
            }

            return false;
        }
        private void PassHelper(int[,] labirint, ref int x, ref int y, List<int> coords)
        {
            int newX = -1;
            int newY = -1;

            if ((y + 1) < labirint.GetLength(0))
            {
                if (!IsSameWay(x, y + 1, coords) && labirint[x, y + 1] == 0)
                {
                    newX = x;
                    newY = y + 1;
                }
            }
            if ((y - 1) >= 0)
            {
                if (!IsSameWay(x, y - 1, coords) && labirint[x, y - 1] == 0)
                {
                    newX = x;
                    newY = y - 1;
                }
            }
            if ((x + 1) < labirint.GetLength(1))
            {
                if (!IsSameWay(x + 1, y, coords) && labirint[x + 1, y] == 0)
                {
                    newX = x + 1;
                    newY = y;
                }
            }
            if ((x - 1) >= 0)
            {
                if (!IsSameWay(x - 1, y, coords) && labirint[x - 1, y] == 0)
                {
                    newX = x - 1;
                    newY = y;
                }
            }

            x = newX;
            y = newY;
        }
    }
}
