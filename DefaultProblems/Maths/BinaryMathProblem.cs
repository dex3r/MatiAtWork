using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen.Problems.Maths
{
	/// <summary>
	/// Binary math problem implementation.
	/// </summary>
	public class BinaryMathProblem : IBinaryMathProblem
	{
		public double A { get; private set; }

		public double B { get; private set; }

		/// <summary>
		/// Last sent result via <see cref="SendResult(double)"/> or null, if no result was sent.
		/// </summary>
		public double? Result { get; private set; }

		public BinaryMathProblem(double a, double b)
		{
			this.A = a;
			this.B = b;
		}

		public void SendResult(double result)
		{
			this.Result = result;
		}
	}
}
