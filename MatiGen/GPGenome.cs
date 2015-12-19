using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen
{
    public class GPGenome
    {
        private double? _fitness;
        public double? Fitness
        {
            get { return _fitness; }
            set { _fitness = value; }
        }

        private List<Expression> _UsedExpression;

        public List<Expression> UsedExpression
        {
            get { return _UsedExpression; }
            set { _UsedExpression = value; }
        }

        private LambdaExpression _expression;

        public LambdaExpression Expression
        {
            get { return _expression; }
            set
            {
                if (!ReferenceEquals(_expression, value))
                {
                    CachedDelegate = null;
                }

                _expression = value;
            }
        }

        private Delegate _cachedDelegate;

        public Delegate CachedDelegate
        {
            get
            {
                if (_cachedDelegate == null)
                {
                    _cachedDelegate = TryCreateDelegate();
                }

                return _cachedDelegate;
            }
            private set
            {
                _cachedDelegate = value;
            }
        }

        public Delegate TryCreateDelegate()
        {
            if (Expression == null)
            {
                return null;
            }

            try
            {
                CachedDelegate = Expression.Compile();
            }
            catch { }

            return CachedDelegate;
        }

        public void InitializeRandomGenome(int expressions)
        {

        }

        public void Mutate(int expressionToAdd)
        {
            
        }
    }
}
