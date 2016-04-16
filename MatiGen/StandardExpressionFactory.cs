using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;

namespace MatiGen
{
	public class StandardExpressionFactory : IExpressionFactory
	{
		protected readonly static Func<Expression, bool> DEFAULT_SELECTOR = (exp) => true;

		private Delegate targetDelegate;
		private int parametersCount;

		Func<Expression, bool>[] paramsSelectors;

		public StandardExpressionFactory(Delegate expressionFactoryMethod, params Func<Expression, bool>[] paramsSelectors)
		{
			this.targetDelegate = expressionFactoryMethod;
			this.parametersCount = expressionFactoryMethod.Method.GetParameters().Length;
			this.paramsSelectors = paramsSelectors;

			if (paramsSelectors.Length == 0)
			{
				this.paramsSelectors = Enumerable.Repeat(DEFAULT_SELECTOR, parametersCount).ToArray();
			}
		}

		public virtual Expression Create(IEnumerable<Expression> expressions)
		{
			Expression[] exprs = new Expression[parametersCount];

			if (!SelectExpressions(expressions, exprs))
			{
				return null;
			}

			//try
			{
				return (Expression)targetDelegate.DynamicInvoke(exprs);
			}
			//catch(TargetInvocationException)
			{
				//return null;
			}
		}

		public virtual bool SelectExpressions(IEnumerable<Expression> availableExpressions, Expression[] outputExpressions)
		{
			for (int i = 0; i < outputExpressions.Length; i++)
			{
				var selector = paramsSelectors[i];
				var validExprs = availableExpressions;

				if (selector != DEFAULT_SELECTOR)
				{
					validExprs = availableExpressions.Where(selector);
				}

				if (validExprs.IsEmpty())
				{
					return false;
				}

				outputExpressions[i] = validExprs.Random();
			}

			return true;
		}
	}
}
