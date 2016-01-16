using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048
{
    public static class GameUtils
    {
        public static int GetBiggestFieldValue(this ITheGame game)
        {
            int biggestValue = 0;

            for (int x = 0; x < game.Width; x++)
            {
                for (int y = 0; y < game.Height; y++)
                {
                    if (biggestValue < game[x, y])
                    {
                        biggestValue = game[x, y];
                    }
                }
            }

            return biggestValue;
        }

        public static int GetBiggestFieldPow2(this ITheGame game)
        {
            return (int)Math.Pow(2, GetBiggestFieldValue(game));
        }

        public static int GetFieldsSum(this ITheGame game)
        {
            int fieldsSum = 0;

            for (int x = 0; x < game.Width; x++)
            {
                for (int y = 0; y < game.Height; y++)
                {
                    fieldsSum += game[x, y];
                }
            }

            return fieldsSum;
        }

        public static int GetFieldsSumPow2(this ITheGame game)
        {
            int fieldsSum = 0;

            for (int x = 0; x < game.Width; x++)
            {
                for (int y = 0; y < game.Height; y++)
                {
                    if (game[x, y] > 0)
                    {
                        fieldsSum += (int)Math.Pow(2, game[x, y]);
                    }
                }
            }

            return fieldsSum;
        }
    }
}
