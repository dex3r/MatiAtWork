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
        protected readonly static Random RAND = new Random();

        private Delegate targetDelegate;
        private int parametersCount;

        public StandardExpressionFactory(Delegate expressionFactoryMethod)
        {
            this.targetDelegate = expressionFactoryMethod;
            this.parametersCount = expressionFactoryMethod.Method.GetParameters().Length;
        }

        public Expression Create(params IEnumerable<Expression>[] expressions)
        {
            Expression[] exprs = new Expression[parametersCount];

            for (int i = 0; i < exprs.Length; i++)
            {
                exprs[i] = Enumerable.Empty<Expression>().Random(RAND, expressions);
            }

            try
            {
                return (Expression)targetDelegate.DynamicInvoke(exprs);
            }
            catch(TargetInvocationException)
            {
                return null;
            }
        }
    }
}
