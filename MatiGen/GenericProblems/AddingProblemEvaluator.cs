using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen.GenericProblems
{
	public class AddingProblemEvaluator : IGenericProblemEvaluator
	{
		private List<Tuple<int, int, int>> trials = new List<Tuple<int, int, int>>();

		public AddingProblemEvaluator()
		{
			trials.Add(CreateTest(5, 5));
			trials.Add(CreateTest(7, 6));
			trials.Add(CreateTest(23, 90));
		}

		public double Evaluate(Delegate solverMethod)
		{
			var finalMethod = (Action<IGenericAddingProblem>)solverMethod;

			int validCount = 0;

			for (int j = 0; j < trials.Count; j++)
			{
				Tuple<int, int, int> validRes = trials[j];

				GenericAddingProblem problem = new GenericAddingProblem(validRes.Item2, validRes.Item3);

				try
				{
					finalMethod(problem);
				}
				catch (Exception ex)
				{
					Debug.WriteLine("Exception while invoking solver method: " + ex.Message);
				}

				int? result = problem.Result;

				if (!result.HasValue)
				{
					continue;
				}

				if (result.Value == validRes.Item1)
				{
					validCount++;
				}
			}

			double fitness = (double)validCount / trials.Count;

			return fitness;
		}

		private static Tuple<int, int, int> CreateTest(int a, int b)
		{
			Func<int, int, int> validSolution = (xa, xb) =>
			{
				return xa + xb + xa * xb + xa * xa + xb * xb;
				//return xa + xb;
			};
			return new Tuple<int, int, int>(validSolution(a, b), a, b);
		}
	}
}
