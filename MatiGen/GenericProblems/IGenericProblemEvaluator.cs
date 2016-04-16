using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen.GenericProblems
{
	public interface IGenericProblemEvaluator
	{
		double Evaluate(Delegate solverMethod);
	}
}
