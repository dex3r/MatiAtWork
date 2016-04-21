using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen
{
	public class AssignExpressionFactory : IExpressionFactory
	{
		public Expression Create(IEnumerable<Expression> expressions)
		{
			var validExpressions = expressions.Where(x => ExpressionExtensions.CheckCanWrite(x));
			var leftExpr = validExpressions.RandomOrDefault();

			if (leftExpr == null)
			{
				return null;
			}

			return expressions.Where(x => leftExpr.Type.IsAssignableFrom(x.Type)).RandomOrDefault();
		}
	}
}
