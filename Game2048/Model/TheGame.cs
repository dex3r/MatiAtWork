using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Game2048
{
    public class TheGame : ITheGame
    {
        private int[,] gameBoard = null;

        public int Width
        {
            get
            {
                IsInitialized();

                return gameBoard.GetLength(0);
            }
        }

        public int Height
        {
            get
            {
                IsInitialized();

                return gameBoard.GetLength(1);
            }
        }

        public int this[int x, int y]
        {
            get
            {
                IsInitialized();

                return gameBoard[x, y];
            }
            set
            {
                IsInitialized();

                gameBoard[x, y] = value;
            }
        }

        public void Initialize(int boardWidth, int boardHeight)
        {
            gameBoard = new int[boardWidth, boardHeight];

            int rX = DefaultRandom.Next(boardWidth);
            int rY = DefaultRandom.Next(boardHeight);

            this[rX, rY] = GetRandomNewFieldValue();

            int r2X, r2Y;

            do
            {
                r2X = DefaultRandom.Next(boardWidth);
                r2Y = DefaultRandom.Next(boardHeight);
            } while (rX == r2X && rY == r2Y);

            this[r2X, r2Y] = GetRandomNewFieldValue();
        }

        public bool MakeMove(MoveDirection direction)
        {
            IsInitialized();

            bool anythingMoved = false;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (MakeMove(x, y, direction))
                    {
                        anythingMoved = true;
                    }
                }
            }

            if (anythingMoved)
            {
                PlaceNewRandomField();
            }

            return anythingMoved;
        }

        private bool PlaceNewRandomField()
        {
            int emptyFieldsCount = GetEmptyFieldsCount();

            if (emptyFieldsCount == 0)
            {
                return false;
            }

            int rand = DefaultRandom.Next(emptyFieldsCount);

            int emptyFields = 0;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (gameBoard[x, y] == 0)
                    {
                        if (emptyFields == rand)
                        {
                            gameBoard[x, y] = GetRandomNewFieldValue();

                            return true;
                        }

                        emptyFields++;
                    }
                }
            }

            throw new InvalidOperationException("This should never happen! ASSERTION FALIED! Could not find next empty field.");
        }

        private int GetEmptyFieldsCount()
        {
            int count = 0;

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (gameBoard[x, y] == 0)
                    {
                        count++;
                    }
                    //
                    // Optimized to:

                    //count += gameBoard[x, y] & 1;
                }
            }

            return count;
        }

        private bool MakeMove(int x, int y, MoveDirection direction)
        {
            int value = gameBoard[x, y];

            if (value == 0)
            {
                return false;
            }

            int neighbourValue;
            int neighbourX;
            int neighbourY;

            if (TryToGetNeighbour(x, y, direction, out neighbourValue, out neighbourX, out neighbourY))
            {
                if (neighbourValue == 0)
                {
                    gameBoard[neighbourX, neighbourY] = gameBoard[x, y];
                    gameBoard[x, y] = 0;
                    MakeMove(neighbourX, neighbourY, direction);

                    return true;
                }
                else if (neighbourValue == value)
                {
                    gameBoard[x, y] = 0;
                    gameBoard[neighbourX, neighbourY]++;

                    return true;
                }
            }

            return false;
        }

        private bool TryToGetNeighbour(int x, int y, MoveDirection direction, out int neighbourValue, out int neighbourX, out int neighbourY)
        {
            switch (direction)
            {
                case MoveDirection.Up:
                    y--;
                    break;
                case MoveDirection.Down:
                    y++;
                    break;
                case MoveDirection.Left:
                    x--;
                    break;
                case MoveDirection.Right:
                    x++;
                    break;
            }

            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                neighbourValue = 0;
                neighbourX = 0;
                neighbourY = 0;

                return false;
            }

            neighbourX = x;
            neighbourY = y;
            neighbourValue = gameBoard[x, y];

            return true;
        }

        public int[,] ToArray()
        {
            //IsInitialized();

            int[,] copy = (int[,])gameBoard.Clone();
            return copy;
        }

        private int GetRandomNewFieldValue()
        {
            return DefaultRandom.NextDouble() < 0.9 ? 1 : 2;
        }

        //[ContractAbbreviator]
        private void IsInitialized()
        {
            //Contract.Requires(gameBoard != null, "The game was not initialized properly.");
        }
    }
}
