using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace MatiGen
{
    public sealed class RandomNumberExpressionFactory : IExpressionFactory
    {
        private readonly Random RAND = new Random();

        private RandomNumberType type;

        public RandomNumberExpressionFactory(RandomNumberType type)
        {
            this.type = type;
        }

        public Expression Create(IEnumerable<Expression> expressions)
        {
            if (type == RandomNumberType.Integer)
            {
                return Expression.Constant(RAND.Next());
            }
            else if (type == RandomNumberType.Double)
            {
                return Expression.Constant(RAND.NextDouble());
            }
            else
            {
                throw new InvalidOperationException("Unknown RandomNumberType type");
            }
        }
    }

    public enum RandomNumberType
    {
        Integer,
        Double,
    }
}
