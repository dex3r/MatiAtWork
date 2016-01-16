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
        protected readonly static Random RAND = new Random();

        public Expression Create(IEnumerable<Expression> expressions)
        {
            var leftExpr = expressions.Where(ExpressionExtensions.CheckCanWrite).RandomOrDefault(RAND);

            if (leftExpr == null)
            {
                return null;
            }

            return expressions.Where(x => leftExpr.Type.IsAssignableFrom(x.Type)).RandomOrDefault(RAND);
        }
    }
}
