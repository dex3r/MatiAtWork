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

        public GPGenome Clone()
        {
            GPGenome clone = new GPGenome();

            clone.UsedExpressions = new List<Expression>(UsedExpressions);
            clone.FinalExpression = FinalExpression;
            clone.Fitness = Fitness;
            clone.CachedDelegate = CachedDelegate;

            return clone;
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

            return _cachedDelegate;
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

        public void Mutate(ProblemBase problem, MutationSettings settings)
        {
            double reduceRand = RAND.NextDouble();

            int expressionsToAddCount = RAND.Next(1, settings.MaxNodesToAdd + 1);

            if (settings.ReduceChance > reduceRand)
            {
                if (expressionsToAddCount < UsedExpressions.Count)
                {
                    int d = expressionsToAddCount;
                    if (d < UsedExpressions.Count)
                    {
                        expressionsToAddCount = d;
                    }

                    for (int i = 0; i < expressionsToAddCount; i++)
                    {
                        int res = RAND.Next(UsedExpressions.Count);

                        UsedExpressions.RemoveAt(res);
                    }
                }

                RebuildFinalExpression(problem);
            }
            else if (expressionsToAddCount > 0)
            {
                for (int i = 0; i < expressionsToAddCount; i++)
                {
                    int pos = RAND.Next(UsedExpressions.Count);
                    AddRandomExpression(problem, settings, pos);
                }

                RebuildFinalExpression(problem);
            }
        }

        private void AddRandomExpression(ProblemBase problem, MutationSettings settings, int index)
        {
            // Possibly use only expression that are already defined in this point of expressions tree (below 'index' in 'UsedExpressions')
            var expToAdd = RandomizeExpression(problem, settings);

            if (expToAdd != null)
            {
                UsedExpressions.Insert(index, expToAdd);
            }
            else
            {
                //throw new Exception("Expression coult not be created");

                //TODO: Could not create expression from factory for some reason.
                //TODO: Possible handle it (ie. report that expression was not added)
            }
        }

        private void AddRandomExpression(ProblemBase problem, MutationSettings settings)
        {
            AddRandomExpression(problem, settings, UsedExpressions.Count);
        }

        private Expression RandomizeExpression(ProblemBase problem, MutationSettings settings)
        {
            IExpressionFactory factory = settings.AvailableExpressions.Random(RAND);
            return factory.Create(problem.Parameters.Concat(UsedExpressions));
        }

        public void RebuildFinalExpression(ProblemBase problem)
        {
            BlockExpression body;

            if (UsedExpressions.Count > 0)
            {
                var finalExpressions = UsedExpressions.Where(x => !typeof(ParameterExpression).IsAssignableFrom(x.GetType())).Where(x => !UsedExpressions.Any(y =>
                    {
                        BlockExpression block = y as BlockExpression;

                        if (block != null)
                        {
                            if (block.Expressions.Any(z => Object.ReferenceEquals(z, x)))
                            {
                                return true;
                            }
                        }

                        return false;
                    }));

                var variables = UsedExpressions.Where(x => typeof(ParameterExpression).IsAssignableFrom(x.GetType())).Cast<ParameterExpression>();

                var validLastExpressions = finalExpressions.Concat(variables).Where(x => problem.MethodReturnType.IsAssignableFrom(x.Type));
                Expression lastExpression = null;

                if (validLastExpressions.Count() > 0)
                {
                    lastExpression = validLastExpressions.Random(RAND);

                    finalExpressions = finalExpressions.Concat(Enumerable.Repeat(lastExpression, 1));
                }

                // Parameter expression cannot be present more than once in Epxression Tree
                if(lastExpression.NodeType == ExpressionType.Parameter)
                {
                    //TODO
                }

                //System.Diagnostics.Debug.WriteLine("Last expression: " + lastExpression.GetType() + " nodeType: " + lastExpression.NodeType);

                if (finalExpressions.Count() == 0)
                {
                    finalExpressions = Enumerable.Repeat(Expression.Empty(), 1);
                }

                body = Expression.Block(variables, finalExpressions);
            }
            else
            {
                body = Expression.Block(Expression.Empty());
            }

            FinalExpression = Expression.Lambda(body, problem.Parameters);
        }
    }
}
