using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Contracts;

namespace Game2048
{
    //[ContractClass(typeof(ITheGameContracts))]
    public interface ITheGame
    {
        int Width { get; }

        int Height { get; }

        /// <summary>
        /// Sets or gets field current value.
        /// </summary>
        /// <param name="x">X Coordinate of a field.</param>
        /// <param name="y">Y Coordinate of a field.</param>
        /// <returns>Current field value or 0 is field is empty.</returns>
        /// <remarks>Remember, that setting a field directly is against the rules of classical 2048 game!</remarks>
        int this[int x, int y] { get; set; }

        /// <summary>
        /// Initialize the board with given width and height.
        /// </summary>
        /// <param name="boardWidth">Initial board width to set.</param>
        /// <param name="boardHeight">Initial board height to set.</param>
        void Initialize(int boardWidth, int boardHeight);

        /// <summary>
        /// Tries to make a move.
        /// </summary>
        /// <param name="direction">Direction of a movement.</param>
        /// <returns>True if move was successfull. Otherwise false.</returns>
        bool MakeMove(MoveDirection direction);

        /// <summary>
        /// Returns the entire field as a 2D array of its values.
        /// </summary>
        /// <returns></returns>
        int[,] ToArray();
    }

    //[ContractClassFor(typeof(ITheGame))]
    //internal abstract class ITheGameContracts : ITheGame
    //{
    //    public int this[int x, int y]
    //    {
    //        get
    //        {
    //            Contract.Requires(x >= 0);
    //            Contract.Requires(y >= 0);

    //            return default(int);
    //        }
    //        set
    //        {
    //            Contract.Requires(x >= 0);
    //            Contract.Requires(y >= 0);

    //            Contract.Ensures(((ITheGame)this)[x, y] == value);
    //        }
    //    }

    //    public void Initialize(int boardWidth, int boardHeight)
    //    {
    //        Contract.Requires(boardWidth >= 2);
    //        Contract.Requires(boardHeight >= 2);
    //    }

    //    public bool MakeMove(MoveDirection direction)
    //    {
    //        return default(bool);
    //    }

    //    public int[,] ToArray()
    //    {
    //        return default(int[,]);
    //    }

    //    public int Width
    //    {
    //        get 
    //        {
    //            Contract.Ensures(Contract.Result<int>() >= 0);

    //            return default(int);
    //        }
    //    }

    //    public int Height
    //    {
    //        get
    //        {
    //            Contract.Ensures(Contract.Result<int>() >= 0);

    //            return default(int);
    //        }
    //    }


    //    public int[,] DirectBoardAccess()
    //    {
    //        return default(int[,]);
    //    }
    //}
}
