using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen.GenericProblems
{
	public interface IGenericAddingProblem
	{
		float A { get; }
		float B { get; }

		void SendResult(float result);
	}
}
