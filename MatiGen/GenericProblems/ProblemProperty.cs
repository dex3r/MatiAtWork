using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen.GenericProblems
{
	public class ProblemProperty : IProblemProperty
	{
		private MemberInfo problemMember;

		public ProblemProperty(FieldInfo problemField)
		{
			this.problemMember = problemField;
		}

		public ProblemProperty(PropertyInfo problemProperty)
		{
			this.problemMember = problemProperty;
		}

		public Expression GetExpression(Expression problemInstance)
		{
			return Expression.MakeMemberAccess(problemInstance, problemMember);
		}
	}
}
