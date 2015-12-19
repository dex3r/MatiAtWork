using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen.Problems
{
    public sealed class AddingProblem : Problem<Func<double, double, double>>
    {
        private List<Tuple<double, double, double>> trials = new List<Tuple<double, double, double>>();

        public AddingProblem()
        {
            trials.Add(CreateTest(5, 5));
            trials.Add(CreateTest(7, 6));
            trials.Add(CreateTest(23, 90));
        }

        public double Evaluate(Func<double, double, double> solverMethod)
        {
            int validCount = 0;

            for (int j = 0; j < trials.Count; j++)
            {
                Tuple<double, double, double> validRes = trials[j];

                try
                {
                    double result = solverMethod(validRes.Item2, validRes.Item3);

                    if (Math.Abs(result - validRes.Item1) < 0.001f)
                    {
                        validCount++;
                    }
                }
                catch
                {

                }
            }

            double fitness = validCount / trials.Count;

            return fitness;
        }

        private static Tuple<double, double, double> CreateTest(double a, double b)
        {
            Func<double, double, double> validSolution = (xa, xb) => xa + xb;
            return new Tuple<double, double, double>(validSolution(a, b), a, b);
        }
    }
}
