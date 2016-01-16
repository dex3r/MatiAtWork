using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MatiGen
{
    public class SameTypeExpressionFactory : StandardExpressionFactory
    {
        private Func<Expression, bool> selector;

        public SameTypeExpressionFactory(Delegate expressionFactoryMethod, Func<Expression, bool> paramsSelector)
            : base(expressionFactoryMethod)
        {
            this.selector = paramsSelector;
        }

        public override bool SelectExpressions(IEnumerable<Expression> availableExpressions, Expression[] outputExpressions)
        {
            Type firstType = null;
            var validExpressions = availableExpressions.Where(selector);

            for(int i = 0; i < outputExpressions.Length; i++)
            {
                var trueTypeExprs = validExpressions;

                if(firstType != null)
                {
                    trueTypeExprs = validExpressions.Where(x => x.Type == firstType);
                }

                var expr = trueTypeExprs.RandomOrDefault(RAND);

                if(expr == null)
                {
                    return false;
                }

                if(i == 0)
                {
                    firstType = expr.Type;
                }

                outputExpressions[i] = expr;
            }

            return true;
        }
    }
}
