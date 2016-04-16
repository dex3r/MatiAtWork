using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen
{
	public abstract class ProblemBase
	{
		public IEnumerable<ParameterExpression> Parameters { get; protected set; }

		public IEnumerable<Expression> AvailableExressions { get; private set; }

		public abstract Type MethodReturnType
		{
			get;
		}

		public void Initialize()
		{
			AvailableExressions = CreateAvailableExpressions();
			Parameters = CreateParameters();
		}

		protected virtual IEnumerable<Expression> CreateAvailableExpressions()
		{
			return Enumerable.Empty<Expression>();
		}

		protected virtual IEnumerable<ParameterExpression> CreateParameters()
		{
			return Enumerable.Empty<ParameterExpression>();
		}

		public abstract double Evaluate(Delegate solverMethod);
	}
}
