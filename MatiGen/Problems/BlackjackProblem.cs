using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MatiGen.Problems
{
    public sealed class BlackjackProblem : SimpleProblemBase
	{
        public override Type MethodReturnType
        {
            get { return typeof(int); }
        }

        public override double Evaluate(Delegate solverMethod)
        {
            var blackjackMethod = solverMethod as Func<int, int>;

            if (blackjackMethod == null)
            {
                return 0f;
            }

            double[] results = new double[prerandomizedSets.Length];

            for (int i = 0; i < prerandomizedSets.Length; i++)
            {
                int[] set = prerandomizedSets[i];
                int currentSum = 0;
                bool runtimeException = false;

                for (int j = 0; j < set.Length; j++)
                {
                    if (j < 2)
                    {
                        currentSum += set[j];
                    }
                    else
                    {
                        int blackjackResult;

                        try
                        {
                            blackjackResult = blackjackMethod(currentSum);
                        }
                        catch
                        {
                            runtimeException = true;
                            break;
                        }

                        if (blackjackResult <= 0)
                        {
                            break;
                        }
                        else
                        {
                            currentSum += set[j];
                        }
                    }
                }

                if (runtimeException || currentSum > 21)
                {
                    results[i] = 0;
                }
                else
                {
                    results[i] = CalculateFitness(currentSum);
                }
            }

            double fitness = results.Average();

            return fitness;
        }

        private double CalculateFitness(int sum)
        {
            double normalizedResult = sum / 21.0;

            return Math.Pow(normalizedResult, 14.205);
        }

        protected override IEnumerable<ParameterExpression> CreateParameters()
        {
            return new ParameterExpression[] { Expression.Parameter(typeof(int), "currentSum") };
        }

        #region samples
        private static readonly int[][] prerandomizedSets = new int[][]
        {
            new int[] { 2, 4, 3, 4, 4, 10, },
            new int[] { 4, 8, 2, 9, },
            new int[] { 6, 4, 2, 10, },
            new int[] { 6, 5, 9, 10, },
            new int[] { 10, 10, 4, },
            new int[] { 10, 10, 4, },
            new int[] { 10, 5, 7, },
            new int[] { 3, 10, 6, 8, },
            new int[] { 10, 10, 6, },
            new int[] { 6, 7, 4, 10, },
            new int[] { 9, 6, 5, 11, },
            new int[] { 8, 10, 10, },
            new int[] { 10, 2, 5, 9, },
            new int[] { 5, 3, 10, 11, },
            new int[] { 10, 5, 5, 8, },
            new int[] { 10, 8, 10, },
            new int[] { 4, 3, 10, 10, },
            new int[] { 3, 10, 5, 10, },
            new int[] { 6, 11, 2, 10, },
            new int[] { 6, 8, 8, },
            new int[] { 4, 5, 11, 6, },
            new int[] { 10, 4, 3, 8, },
            new int[] { 7, 5, 10, },
            new int[] { 10, 5, 3, 3, 11, },
            new int[] { 5, 4, 10, 3, },
            new int[] { 4, 5, 10, 9, },
            new int[] { 3, 9, 7, 11, },
            new int[] { 9, 5, 4, 7, },
            new int[] { 8, 4, 2, 2, 8, },
            new int[] { 9, 10, 10, },
            new int[] { 2, 9, 5, 8, },
            new int[] { 6, 3, 2, 9, 5, },
            new int[] { 2, 10, 10, },
            new int[] { 10, 6, 5, 10, },
            new int[] { 7, 6, 4, 5, },
            new int[] { 10, 4, 11, },
            new int[] { 4, 2, 7, 4, 3, 2, },
            new int[] { 3, 7, 10, 9, },
            new int[] { 11, 3, 10, },
            new int[] { 2, 6, 5, 2, 9, },
            new int[] { 10, 10, 8, },
            new int[] { 8, 8, 10, },
            new int[] { 3, 10, 4, 9, },
            new int[] { 3, 6, 6, 10, },
            new int[] { 10, 9, 2, 7, },
            new int[] { 11, 10, 10, },
            new int[] { 8, 3, 10, 2, },
            new int[] { 4, 10, 5, 10, },
            new int[] { 10, 3, 3, 4, 10, },
            new int[] { 11, 7, 8, },
            new int[] { 10, 9, 10, },
            new int[] { 2, 4, 10, 8, },
            new int[] { 6, 7, 4, 3, 4, },
            new int[] { 2, 11, 10, },
            new int[] { 5, 3, 10, 5, },
            new int[] { 9, 10, 7, },
            new int[] { 6, 3, 10, 4, },
            new int[] { 5, 8, 11, },
            new int[] { 3, 9, 10, },
            new int[] { 7, 3, 5, 10, },
            new int[] { 5, 10, 10, },
            new int[] { 10, 10, 5, },
            new int[] { 9, 11, 8, },
            new int[] { 2, 10, 10, },
            new int[] { 4, 10, 2, 3, 10, },
            new int[] { 10, 9, 5, },
            new int[] { 4, 9, 4, 11, },
            new int[] { 10, 2, 6, 5, },
            new int[] { 10, 4, 10, },
            new int[] { 10, 10, 2, },
            new int[] { 11, 4, 10, },
            new int[] { 10, 11, 9, },
            new int[] { 10, 7, 8, },
            new int[] { 10, 9, 6, },
            new int[] { 5, 3, 2, 8, 4, },
            new int[] { 9, 8, 2, 10, },
            new int[] { 10, 10, 10, },
            new int[] { 2, 10, 11, },
            new int[] { 5, 5, 6, 6, },
            new int[] { 10, 4, 4, 10, },
            new int[] { 10, 4, 3, 10, },
            new int[] { 2, 7, 10, 2, 7, },
            new int[] { 7, 5, 10, },
            new int[] { 5, 4, 4, 2, 5, 7, },
            new int[] { 10, 6, 5, 8, },
            new int[] { 4, 9, 6, 10, },
            new int[] { 9, 3, 4, 8, },
            new int[] { 10, 3, 2, 5, 4, },
            new int[] { 5, 3, 10, 8, },
            new int[] { 10, 10, 10, },
            new int[] { 6, 6, 10, },
            new int[] { 10, 10, 3, },
            new int[] { 2, 5, 3, 7, 9, },
            new int[] { 10, 9, 6, },
            new int[] { 2, 8, 9, 7, },
            new int[] { 10, 10, 10, },
            new int[] { 10, 9, 3, },
            new int[] { 10, 2, 2, 11, },
            new int[] { 11, 8, 10, },
            new int[] { 2, 9, 9, 4, },
        };
        #endregion samples

    }
}
