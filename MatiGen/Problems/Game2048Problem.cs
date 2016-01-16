using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Game2048;

namespace MatiGen.Problems
{
    public sealed class Game2048Problem : ProblemBase
    {
        public const int GameBoardWidth = 4;
        public const int GameBoardHeight = 4;

        public override Type MethodReturnType
        {
            get { return typeof(int); }
        }

        public override double Evaluate(Delegate solverMethod)
        {
            var gameMethod = solverMethod as Func<int[,], int>;

            if(gameMethod == null)
            {
                return 0f;
            }

            // How many games should every solution be evaluated against.
            int gamesToPlay = 10;

            double[] results = new double[gamesToPlay];

            for(int i = 0; i < gamesToPlay; i++)
            {
                results[i] = RunSingleTest(gameMethod);
            }

            double finalFitness = results.Average();

            return finalFitness;
        }

        private double RunSingleTest(Func<int[,], int> evaluator)
        {
            ITheGame game = new TheGame();
            game.Initialize(GameBoardWidth, GameBoardHeight);

            while(true)
            {
                int result;

                try
                {
                    result = evaluator(game.ToArray());
                }
                catch
                {
                    // If evaluator method throws exception probably means that it will throw it again next time, so just finish the game.
                    break;
                }

                bool moved = false;

                for (int i = 0; i < 4; i++)
                {
                    MoveDirection direction = (MoveDirection)((int)Math.Abs(result) % 4);

                    if( game.MakeMove(direction))
                    {
                        moved = true;
                        break;
                    }

                    // If movement in selected direction was not possible try another direction.
                    result++;
                }

                // No successfull movement in all 4 directions means game over.
                if(!moved)
                {
                    break;
                }
            }

            double fitness = CalculateFitness(game.GetFieldsSumPow2(), game.GetBiggestFieldPow2());
            return fitness;
        }

        private double CalculateFitness(int fieldsSum, int bestFieldValue)
        {
            // Fields sum is relevant, but the biggest field value is much more important.
            return (fieldsSum + ((double)bestFieldValue * 10.0));
        }

        protected override IEnumerable<ParameterExpression> CreateParameters()
        {
            return new ParameterExpression[] { Expression.Parameter(typeof(int[,]), "gameBoardValues") };
        }

    }
}
