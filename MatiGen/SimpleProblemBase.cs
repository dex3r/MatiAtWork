using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen
{
	public abstract class SimpleProblemBase : ProblemBase
	{
		protected virtual IEnumerable<ParameterExpression> CreateParameters()
		{
			return Enumerable.Empty<ParameterExpression>();
		}
	}
}
