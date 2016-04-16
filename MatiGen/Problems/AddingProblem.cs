﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MatiGen.Problems
{
    public sealed class AddingProblem : SimpleProblemBase
    {
        public override Type MethodReturnType
        {
            get { return typeof(double); }
        }

        private List<Tuple<double, double, double>> trials = new List<Tuple<double, double, double>>();

        public AddingProblem()
            : base()
        {
            trials.Add(CreateTest(5, 5));
            trials.Add(CreateTest(7, 6));
            trials.Add(CreateTest(23, 90));
        }

        public override double Evaluate(Delegate solverMethod)
        {
            var addingMethod = solverMethod as Func<double, double, double>;

            if (addingMethod == null)
            {
                return 0f;
            }

            int validCount = 0;

            for (int j = 0; j < trials.Count; j++)
            {
                Tuple<double, double, double> validRes = trials[j];

                try
                {
                    double result = addingMethod(validRes.Item2, validRes.Item3);

                    if (Math.Abs(result - validRes.Item1) < 0.001f)
                    {
                        validCount++;
                    }
                }
                catch
                {

                }
            }

            double fitness = (double)validCount / trials.Count;

            return fitness;
        }

        private static Tuple<double, double, double> CreateTest(double a, double b)
        {
            Func<double, double, double> validSolution = (xa, xb) =>
                {
                    if(xa > xb)
                    {
                        return xa;
                    }
                    else
                    {
                        return xb;
                    }
                };
            return new Tuple<double, double, double>(validSolution(a, b), a, b);
        }

        protected override IEnumerable<System.Linq.Expressions.ParameterExpression> CreateParameters()
        {
            return new ParameterExpression[] { Expression.Parameter(typeof(double), "a"), Expression.Parameter(typeof(double), "b") };
        }
    }
}
