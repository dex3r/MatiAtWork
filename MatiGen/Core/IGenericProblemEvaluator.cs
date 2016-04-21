using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen
{
	/// <summary>
	/// Interface for problem evaluators.
	/// </summary>
	public interface IGenericProblemEvaluator
	{
		/// <summary>
		/// Returns fitness value for given solver method. The delegate should be casted to Action(T), where T is the problem interface.
		/// Note, that solver method could throw an exception or do nothing.
		/// </summary>
		/// <param name="solverMethod">Solver method to call with problem as a parameter.</param>
		/// <returns>Fitness value</returns>
		double Evaluate(Delegate solverMethod);
	}
}
