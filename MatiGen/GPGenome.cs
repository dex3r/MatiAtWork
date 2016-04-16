using MatiGen.GenericProblems;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
			catch (Exception ex)
			{
				Debug.WriteLine("Exception while compiling genome delegate: " + ex.Message);
			}

			return _cachedDelegate;
		}

		public void InitializeRandomGenome(int expressions, ProblemHandler problem, MutationSettings settings)
		{
			UsedExpressions = new List<Expression>();

			for (int i = 0; i < expressions; i++)
			{
				AddRandomExpression(problem, settings);
			}

			RebuildFinalExpression(problem);
		}

		public void Mutate(ProblemHandler problem, MutationSettings settings)
		{
			double reduceRand = StaticRandom.Rand.NextDouble();

			int expressionsToAddCount = StaticRandom.Rand.Next(1, settings.MaxNodesToAdd + 1);

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
						int res = StaticRandom.Rand.Next(UsedExpressions.Count);

						UsedExpressions.RemoveAt(res);
					}
				}

				RebuildFinalExpression(problem);
			}
			else if (expressionsToAddCount > 0)
			{
				for (int i = 0; i < expressionsToAddCount; i++)
				{
					int pos = StaticRandom.Rand.Next(UsedExpressions.Count);
					AddRandomExpression(problem, settings, pos);
				}

				RebuildFinalExpression(problem);
			}
		}

		private void AddRandomExpression(ProblemHandler problem, MutationSettings settings, int index)
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

		private void AddRandomExpression(ProblemHandler problem, MutationSettings settings)
		{
			AddRandomExpression(problem, settings, UsedExpressions.Count);
		}

		private Expression RandomizeExpression(ProblemHandler problem, MutationSettings settings)
		{
			IExpressionFactory factory = settings.AvailableExpressions.Concat(problem.AdditionalFactories).Random();
			return factory.Create(problem.Parameters.Concat(UsedExpressions));
		}

		public void RebuildFinalExpression(ProblemHandler problem)
		{
			BlockExpression body;

			if (UsedExpressions.Count > 0)
			{
				// Dont include parameter expressions into final expressions collection (they have special place inside BlockExpression)
				//var finalExpressions = UsedExpressions.Where(x => !typeof(ParameterExpression).IsAssignableFrom(x.GetType()));
				IEnumerable<Expression> finalExpressions = UsedExpressions;

				// Duuno why this is here?
				// Remove all block expressions that contains an expression that is already in expression tree
				//finalExpressions = finalExpressions.Where(x => !UsedExpressions.Any(y =>
				//	{
				//		BlockExpression block = y as BlockExpression;

				//		if (block != null)
				//		{
				//			if (block.Expressions.Any(z => Object.ReferenceEquals(z, x)))
				//			{
				//				return true;
				//			}
				//		}
				//		return false;
				//	}));

				var variables = UsedExpressions.Where(x => typeof(ParameterExpression).IsAssignableFrom(x.GetType())).Cast<ParameterExpression>().Distinct();

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

			FinalExpression = Expression.Lambda(problem.DelegateType, body, problem.ProblemInstance);
		}
	}
}
