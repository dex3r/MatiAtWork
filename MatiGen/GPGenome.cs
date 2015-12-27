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
        protected readonly static Random RAND = new Random();

        private double? _fitness;
        public double? Fitness
        {
            get { return _fitness; }
            set { _fitness = value; }
        }

        private List<Expression> _UsedExpression;

        public List<Expression> UsedExpressions
        {
            get { return _UsedExpression; }
            set { _UsedExpression = value; }
        }

        private LambdaExpression _expression;

        public LambdaExpression FinalExpression
        {
            get 
            { 
                return _expression;
            }
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
            if (FinalExpression == null)
            {
                return null;
            }

            try
            {
                CachedDelegate = FinalExpression.Compile();
            }
            catch 
            {
                // Used expression have compile errors. Handle it.
            }

            return CachedDelegate;
        }

        public void InitializeRandomGenome(int expressions, ProblemBase problem, MutationSettings settings)
        {
            UsedExpressions = new List<Expression>();
            Random r = new Random();

            for (int i = 0; i < expressions; i++)
            {
                AddRandomExpression(problem, settings);
            }

            RebuildFinalExpression(problem);
        }

        public void Mutate(int expressionsToAddCount, ProblemBase problem, MutationSettings settings)
        {
            if(expressionsToAddCount < 0)
            {
                int expressionToRemoveCount = -expressionsToAddCount;

                for(int i = 0; i < expressionToRemoveCount; i++)
                {
                    int res = RAND.Next(UsedExpressions.Count);

                    UsedExpressions.RemoveAt(res);
                }

                RebuildFinalExpression(problem);
            }
            else if(expressionsToAddCount > 0)
            {
                // Bad. Select random point in expression tree and add new Expression there (with all used expressions as params or just previous to the new one??)

                for(int i = 0; i < expressionsToAddCount; i++)
                {
                    AddRandomExpression(problem, settings);
                }

                RebuildFinalExpression(problem);
            }
        }

        private void AddRandomExpression(ProblemBase problem, MutationSettings settings)
        {
            IExpressionFactory factory = settings.AvailableExpressions.Random(RAND);

            Expression expToAdd = factory.Create(problem.Parameters, UsedExpressions);

            UsedExpressions.Add(expToAdd);
        }

        public void RebuildFinalExpression(ProblemBase problem)
        {
            BlockExpression body = Expression.Block(UsedExpressions);
            FinalExpression = Expression.Lambda(body, problem.Parameters);
        }
    }
}
