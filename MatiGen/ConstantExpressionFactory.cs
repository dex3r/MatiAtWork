using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MatiGen
{
    public sealed class ConstantExpressionFactory : IExpressionFactory
    {
        private Expression expression;

        public ConstantExpressionFactory(Expression expr)
        {
            this.expression = expr;
        }

        public Expression Create(IEnumerable<Expression> expressions)
        {
            return expression;
        }
    }
}
