using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen.GenericProblems
{
	public class GenericAddingProblem : IGenericAddingProblem
	{
		public float A { get; internal set; }

		public float B { get; internal set; }

		internal float? Result { get; private set; }

		public void SendResult(float result)
		{
			if (this.Result.HasValue)
			{
				return;
			}

			this.Result = result;
		}
	}
}
