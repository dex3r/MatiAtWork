using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen
{
	public class ProblemHandler
	{
		public IEnumerable<Expression> Parameters { get; private set; }

		/// <summary>
		/// Collection of additional factories created for this problem. This contains public problem interface methods calls.
		/// </summary>
		public IEnumerable<IExpressionFactory> AdditionalFactories { get; private set; }

		public ParameterExpression ProblemInstance { get; private set; }
		public IGenericProblemEvaluator Evaluator { get; private set; }
		public Type DelegateType { get; private set; }

		private Type problemInterfaceType;

		public ProblemHandler(Type problemInterfaceType, IGenericProblemEvaluator evaluator)
		{
			if (problemInterfaceType == null)
			{
				throw new ArgumentNullException(nameof(problemInterfaceType));
			}
			if (evaluator == null)
			{
				throw new ArgumentNullException(nameof(evaluator));
			}

			this.Evaluator = evaluator;
			this.problemInterfaceType = problemInterfaceType;

			ProblemInstance = Expression.Parameter(problemInterfaceType, "problem");
			DelegateType = typeof(Action<>).MakeGenericType(problemInterfaceType);

			PropertyInfo[] properties = problemInterfaceType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			FieldInfo[] fields = problemInterfaceType.GetFields(BindingFlags.Instance | BindingFlags.Public);

			Parameters = properties.Cast<MemberInfo>().Concat(fields.Cast<MemberInfo>()).Select(x => Expression.MakeMemberAccess(ProblemInstance, x));

			MethodInfo[] methods = problemInterfaceType.GetMethods(BindingFlags.Instance | BindingFlags.Public);
			AdditionalFactories = methods.Select(x => new MethodCallFactory(x, ProblemInstance));
		}
	}
}
