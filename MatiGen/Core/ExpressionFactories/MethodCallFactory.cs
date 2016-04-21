using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen
{
	public class MethodCallFactory : IExpressionFactory
	{
		private MethodInfo method;
		private Expression invocationTarget;
		private Type[] arguments;

		public MethodCallFactory(MethodInfo method, Expression invocationTarget)
		{
			this.method = method;
			this.invocationTarget = invocationTarget;
			arguments = method.GetParameters().Select(x => x.ParameterType).ToArray();
		}

		public Expression Create(IEnumerable<Expression> expressions)
		{
			Expression[] finalParamters = new Expression[arguments.Length];

			for (int i = 0; i < finalParamters.Length; i++)
			{
				Type parameterType = arguments[i];

				Expression exp = expressions.Where(x => parameterType.IsAssignableFrom(x.Type)).RandomOrDefault();

				if (exp == null)
				{
					return null;
				}

				finalParamters[i] = exp;
			}

			return Expression.Call(invocationTarget, method, finalParamters);
		}
	}
}
