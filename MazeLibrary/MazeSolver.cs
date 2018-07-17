namespace MazeLibrary
{
    using System;
    using System.Collections.Generic;

    public class MazeSolver
    {
        #region Fields

        private readonly int[,] _labirint;
        private int _row;
        private int _column;
        private int[,] _trueLabirintCopy;
        private readonly List<int> _coordsList = new List<int>();

        #endregion

        #region Public API

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="mazeModel">Maze</param>
        /// <param name="startZ">Start row</param>
        /// <param name="startColumn">Start column</param>
        /// <exception cref="ArgumentException"><param name="startZ">,<param name="startColumn"> can't be less than zero</param></param></exception>
        /// <exception cref="ArgumentNullException"><param name="mazeModel">can't be null</param></exception>
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

        /// <summary>
        /// The method returns the passed labyrinth
        /// </summary>
        /// <returns>Labyrinth with pass</returns>
        public int[,] MazeWithPass()
        {
            int k = 0;
            int counter = 0;

            while (k != _coordsList.Count)
            {
                _labirint[_coordsList[k], _coordsList[k + 1]] = ++counter;
                k += 2;
            }

            return _labirint;
        }

        /// <summary>
        /// The method finds a way out of the labyrinth by searching for deadlocks and excluding them from the search
        /// </summary>
        public void PassMaze()
        {
            _trueLabirintCopy = new int[_labirint.GetLength(0), _labirint.GetLength(1)];

            for (int i = 0; i < _labirint.GetLength(1); i++)
            {
                for (int j = 0; j < _labirint.GetLength(0); j++)
                {
                    _trueLabirintCopy[i, j] = _labirint[i, j];
                }
            }

            SetDeadblock(_trueLabirintCopy);

            while (_row != -1 && _column != -1)
            {
                _coordsList.Add(_row);
                _coordsList.Add(_column);
                PassHelper(_trueLabirintCopy, ref _row, ref _column, _coordsList);
            }
        }

        #endregion

        #region Private API

        private void SetDeadblock(int[,] labirint)
        {
            for (int i = 1; i < labirint.GetLength(1) - 1; i++)
            {
                for (int j = 1; j < labirint.GetLength(0) - 1; j++)
                {
                    SetZeroAsDeadblockRec(i, j);
                }
            }
        }

        private void SetZeroAsDeadblockRec(int row, int col)
        {
            int k = 0;

            if (_trueLabirintCopy[row, col] == 0)
            {
                if (_trueLabirintCopy[row - 1, col] != 0)
                {
                    k = k + 1;
                }

                if (_trueLabirintCopy[row, col - 1] != 0)
                {
                    k = k + 1;
                }

                if (_trueLabirintCopy[row + 1, col] != 0)
                {
                    k = k + 1;
                }

                if (_trueLabirintCopy[row, col + 1] != 0)
                {
                    k = k + 1;
                }

                if (k == 4)
                {
                    _trueLabirintCopy[row, _column] = -1;
                }

                if (k == 3)
                {
                    _trueLabirintCopy[row, col] = -1;

                    if (_trueLabirintCopy[row - 1, col] == 0)
                    {
                        SetZeroAsDeadblockRec(row - 1, col);
                    }

                    if (_trueLabirintCopy[row, col - 1] == 0)
                    {
                        SetZeroAsDeadblockRec(row, col - 1);
                    }

                    if (_trueLabirintCopy[row + 1, col] == 0)
                    {
                        SetZeroAsDeadblockRec(row + 1, col);
                    }

                    if (_trueLabirintCopy[row, col + 1] == 0)
                    {
                        SetZeroAsDeadblockRec(row, col + 1);
                    }
                }
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

        private void PassHelper(int[,] labirint, ref int row, ref int column, List<int> coords)
        {
            if ((row - 1) >= 0)
            {
                if (labirint[row - 1, column] == 0 && !IsSameWay(row - 1, column, coords))
                {
                    row = row - 1;
                    return;
                }
            }

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

    #endregion
}