using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen
{
    public class Population
    {
        private ProblemBase _problem;

        public ProblemBase Problem
        {
            get { return _problem; }
            private set { _problem = value; }
        }

        private List<GPGenome> _genomes;
        public List<GPGenome> Genomes
        {
            get { return _genomes; }
            private set { _genomes = value; }
        }

        private int _size;

        public int Size
        {
            get { return _size; }
            set { _size = value; }
        }

        private MutationSettings _mutationSettings;

        public MutationSettings MutationSettings
        {
            get { return _mutationSettings; }
            set { _mutationSettings = value; }
        }

        private int _generation = 0;

        public int Generation
        {
            get { return _generation; }
            set { _generation = value; }
        }

        public Population(ProblemBase problem)
        {
            this.Problem = problem;

            MutationSettings = new MutationSettings(GenerateExpressionsFactories());
        }

        private IEnumerable<IExpressionFactory> GenerateExpressionsFactories()
        {
            return new IExpressionFactory[] {
                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.Add),
                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.Subtract),
                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.Divide),
                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.Multiply),
                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.Modulo),

                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.LessThan),
                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.LessThanOrEqual),
                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.GreaterThan),
                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.GreaterThanOrEqual),
                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.Equal),
                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.NotEqual),

                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.IfThen),
                new StandardExpressionFactory((Func<Expression, Expression, Expression, Expression>)Expression.IfThenElse),

                new StandardExpressionFactory((Func<Expression, Expression>)Expression.PostDecrementAssign),
                new StandardExpressionFactory((Func<Expression, Expression>)Expression.PostIncrementAssign),

                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.And),
                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.Or),
                new StandardExpressionFactory((Func<Expression, Expression>)Expression.Not),

                new StandardExpressionFactory((Func<Expression, Expression>)(x => Expression.Call((((Func<double, double>)Math.Sin)).Method, x))),
                new StandardExpressionFactory((Func<Expression, Expression>)(x => Expression.Call((((Func<double, double>)Math.Cos)).Method, x))),
                new StandardExpressionFactory((Func<Expression, Expression>)(x => Expression.Call((((Func<double, double>)Math.Abs)).Method, x))),

                new ConstantExpressionFactory(Expression.Constant(Math.E)),
                new ConstantExpressionFactory(Expression.Constant(Math.PI)),
                new ConstantExpressionFactory(Expression.Constant(1)),
                new ConstantExpressionFactory(Expression.Constant(-1)),

                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.Block),
                new StandardExpressionFactory((Func<Expression, Expression, Expression, Expression>)Expression.Block),
                new StandardExpressionFactory((Func<Expression, Expression, Expression, Expression, Expression>)Expression.Block),
                new StandardExpressionFactory((Func<Expression, Expression, Expression, Expression, Expression, Expression>)Expression.Block),
            };
        }

        public void InitializeRandomPopulation(int size, int minComplexity, int maxComplexity)
        {
            this.Size = size;

            Genomes = new List<GPGenome>();
            Random r = new Random();
            for (int i = 0; i < size; i++)
            {
                int complexity = r.Next(minComplexity, maxComplexity + 1);
                GPGenome newGenome = new GPGenome();
                newGenome.InitializeRandomGenome(complexity, Problem, MutationSettings);
            }
        }

        public void ProcessGeneration()
        {
            Generation++;

            for (int i = 0; i < Genomes.Count; i++)
            {
                GPGenome genome = Genomes[i];

                if (!genome.Fitness.HasValue && genome.CachedDelegate != null)
                {
                    genome.Fitness = Problem.Evaluate(genome.CachedDelegate);
                }
            }
        }

        public GPGenome GetBestGenome()
        {
            GPGenome bestGenome = null;

            for (int i = 0; i < Genomes.Count; i++)
            {
                GPGenome genome = Genomes[i];

                if (genome.Fitness.HasValue)
                {
                    if (bestGenome == null)
                    {
                        bestGenome = genome;
                    }
                    else if (bestGenome.Fitness.Value < genome.Fitness)
                    {
                        bestGenome = genome;
                    }
                }
            }

            return bestGenome;
        }
    }
}
