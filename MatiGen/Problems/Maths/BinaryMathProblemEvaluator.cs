using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen.Problems.Maths
{
	/// <summary>
	/// Evaluator for <see cref="IBinaryMathProblem"/>.
	/// </summary>
	public sealed class BinaryMathProblemEvaluator : IGenericProblemEvaluator
	{
		private Func<double, double, double> equation;
		private Tuple<double, double, double>[] testCases;

		/// <summary>
		/// Creates new evaluator for given equation and default test cases.
		/// </summary>
		/// <param name="equation"></param>
		public BinaryMathProblemEvaluator(Func<double, double, double> equation)
		{
			this.equation = equation;
			testCases = GenerateTests();
		}

		/// <summary>
		/// Creates new evaluator for given equation with given test cases.
		/// </summary>
		/// <param name="equation"></param>
		/// <param name="testCases"></param>
		public BinaryMathProblemEvaluator(Func<double, double, double> equation, Tuple<double, double, double>[] testCases)
		{
			this.equation = equation;
			this.testCases = testCases;
		}

		private Tuple<double, double, double>[] GenerateTests()
		{
			var list = new List<Tuple<double, double, double>>();

			list.Add(CreateTest(5.0, 9.0));
			list.Add(CreateTest(7.0, 6.0));
			list.Add(CreateTest(23.0, 90.0));

			return list.ToArray();
		}

		private Tuple<double, double, double> CreateTest(double a, double b)
		{
			return new Tuple<double, double, double>(equation(a, b), a, b);
		}

		public double Evaluate(Delegate solverMethod)
		{
			var finalMethod = (Action<IBinaryMathProblem>)solverMethod;

			// How many times the problem has been solved.
			int validCount = 0;

			for (int i = 0; i < testCases.Length; i++)
			{
				Tuple<double, double, double> validRes = testCases[i];

				BinaryMathProblem problem = new BinaryMathProblem(validRes.Item2, validRes.Item3);

				try
				{
					finalMethod(problem);
				}
				catch (Exception ex)
				{
					Debug.WriteLine("Exception while invoking solver method: " + ex.Message);
				}

				double? result = problem.Result;

				if (!result.HasValue)
				{
					// Solver method did not submited any value.
					continue;
				}

				// Compare obtained result with expexted with floating point error taken into account.
				if (Math.Abs(result.Value - validRes.Item1) < 0.0001)
				{
					validCount++;
				}
			}

			// Fitness result is an average of all results
			double fitness = (double)validCount / (double)testCases.Length;

			return fitness;
		}

		/// <summary>
		/// Simple adding problem (a + b).
		/// </summary>
		public readonly static BinaryMathProblemEvaluator AddingProblem = new BinaryMathProblemEvaluator((a, b) => a + b);

		/// <summary>
		/// Simple substraction problem (a - b).
		/// </summary>
		public readonly static BinaryMathProblemEvaluator SubstractionProblem = new BinaryMathProblemEvaluator((a, b) => a - b);

		/// <summary>
		/// Simple multiplication problem (a * b).
		/// </summary>
		public readonly static BinaryMathProblemEvaluator MultiplicationProblem = new BinaryMathProblemEvaluator((a, b) => a * b);

		/// <summary>
		/// Simple division problem (a / b).
		/// </summary>
		public readonly static BinaryMathProblemEvaluator DivisionProblem = new BinaryMathProblemEvaluator((a, b) => a / b);

		/// <summary>
		/// Simple comparison problem (a > b ? a : b).
		/// </summary>
		public readonly static BinaryMathProblemEvaluator ComparisonProblem = new BinaryMathProblemEvaluator((a, b) => a > b ? a : b);

		/// <summary>
		/// Comples adding and multiplying problem (a + b + a * b). Can take more time to solve, than simple problems.
		/// </summary>
		public readonly static BinaryMathProblemEvaluator ComplexProblem = new BinaryMathProblemEvaluator((a, b) => a + b + a * b);

		/// <summary>
		/// More complex adding and multiplication problme (a + b + a * b + a * a + b * b). Can take significantly more time, then simple problems.
		/// </summary>
		public readonly static BinaryMathProblemEvaluator MoreComplexProblem = new BinaryMathProblemEvaluator((a, b) => a + b + a * b + a * a + b * b);

		/// <summary>
		/// Extreme binary math problem. Solving this can take a very long time.
		/// Probably days. Or if you are reading this in the future on better computers, few miliseconds ;). BTW Hello from 2016.
		/// </summary>
		public readonly static BinaryMathProblemEvaluator ExtremeProblem = new BinaryMathProblemEvaluator((a, b) =>
		{
			double big = a > b ? a : b;
			double small = a > b ? b : a;

			return ((big / small) * (big / small)) * big + big + small - (big * big + small * small);
		});
	}
}
