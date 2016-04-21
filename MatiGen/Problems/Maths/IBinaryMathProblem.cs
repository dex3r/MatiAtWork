using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen.Problems.Maths
{
	/// <summary>
	/// Binary math problem interface.
	/// </summary>
	public interface IBinaryMathProblem
	{
		/// <summary>
		/// First argument (problem input).
		/// </summary>
		double A { get; }

		/// <summary>
		/// Second argument (problem input).
		/// </summary>
		double B { get; }

		/// <summary>
		/// Method used to send result (problem output).
		/// </summary>
		void SendResult(double result);
	}
}
