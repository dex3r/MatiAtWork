using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen.GenericProblems
{
	public class GenericAddingProblem : IGenericAddingProblem
	{
		public int A { get; private set; }

		public int B { get; private set; }

		internal int? Result { get; private set; }

		public GenericAddingProblem(int a, int b)
		{
			this.A = a;
			this.B = b;
		}

		public void SendResult(int result)
		{
			this.Result = result;
		}
	}
}
