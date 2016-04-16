using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen.GenericProblems
{
	public abstract class GenericProblemBase : ProblemBase
	{
		public override Type MethodReturnType
		{
			get
			{
				return typeof(void);
			}
		}

		private Type problemInterfaceType;
		private ParameterExpression problemParameter;

		public GenericProblemBase(object problem, Type problemInterfaceType) : base()
		{
			if (problem == null)
			{
				throw new ArgumentNullException(nameof(problem));
			}
			if (problemInterfaceType == null)
			{
				throw new ArgumentNullException(nameof(problemInterfaceType));
			}

			this.problemInterfaceType = problemInterfaceType;

			problemParameter = Expression.Parameter(problemInterfaceType, "problem");
		}

		protected override IEnumerable<Expression> CreateAvailableExpressions()
		{
			List<Expression> expressions = new List<Expression>();

			Use IProblemAccessExpressionFactory instead of Expression

			PropertyInfo[] properties = problemInterfaceType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
			expressions.AddRange(properties.Where(x => x.CanRead).Select(x => Expression.Property()
		}

		protected override IEnumerable<ParameterExpression> CreateParameters()
		{
			return new ParameterExpression[] { problemParameter };
		}
	}
}
