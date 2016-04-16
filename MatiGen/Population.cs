using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MatiGen.Problems;

namespace MatiGen
{
    public class Population
    {
        protected readonly static Random RAND = new Random();

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

            MutationSettings = new MutationSettings(GenerateExpressionsFactories(), 0.6, 9);
        }

        private IEnumerable<IExpressionFactory> GenerateExpressionsFactories()
        {
            return new IExpressionFactory[] {
                //new StandardExpressionFactory((Func<Expression>)(() => Expression.Variable(typeof(int)))),
                new StandardExpressionFactory((Func<Expression>)(() => Expression.Variable(typeof(int)))),
                //new StandardExpressionFactory((Func<Expression>)(() => Expression.Variable(typeof(bool)))),

                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.Add,
                    x => x.Type == typeof(int),
                    x => x.Type == typeof(int)),
                //new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.Subtract),
                //new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.Divide),
                //new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.Multiply),
                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.Modulo,
                    x => x.Type == typeof(int),
                    x => x.Type == typeof(int)),

                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.LessThan,
                    x => x.Type == typeof(int),
                    x => x.Type == typeof(int)),
                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.LessThanOrEqual,
                    x => x.Type == typeof(int),
                    x => x.Type == typeof(int)),

                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.GreaterThan,
                    x => x.Type == typeof(int),
                    x => x.Type == typeof(int)),
                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.GreaterThanOrEqual,
                    x => x.Type == typeof(int),
                    x => x.Type == typeof(int)),

                new SameTypeExpressionFactory((Func<Expression, Expression, Expression>)Expression.Equal,
                    x => x.Type != typeof(void)),

                new SameTypeExpressionFactory((Func<Expression, Expression, Expression>)Expression.NotEqual,
                    x => x.Type != typeof(void)),

                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.IfThen,
                    x => x.Type == typeof(bool),
                    x => true),

                new StandardExpressionFactory((Func<Expression, Expression, Expression, Expression>)Expression.IfThenElse,
                    x => x.Type == typeof(bool),
                    x => true,
                    x => true),

                //new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.Assign),
                new AssignExpressionFactory(),

                new StandardExpressionFactory((Func<Expression, Expression>)Expression.PostDecrementAssign,
                    x => x.Type == typeof(int) && x.CheckCanWrite()),
                new StandardExpressionFactory((Func<Expression, Expression>)Expression.PostIncrementAssign,
                    x => x.Type == typeof(int) && x.CheckCanWrite()),

                //new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.And),
                //new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.Or),
                //new StandardExpressionFactory((Func<Expression, Expression>)Expression.Not),
                //new StandardExpressionFactory((Func<Expression, Expression>)Expression.IsTrue),

                //new StandardExpressionFactory((Func<Expression, Expression>)(x => Expression.Call((((Func<double, double>)Math.Sin)).Method, x))),
                //new StandardExpressionFactory((Func<Expression, Expression>)(x => Expression.Call((((Func<double, double>)Math.Cos)).Method, x))),
                //new StandardExpressionFactory((Func<Expression, Expression>)(x => Expression.Call((((Func<double, double>)Math.Abs)).Method, x))),

                //new ConstantExpressionFactory(Expression.Constant(Math.E)),
                //new ConstantExpressionFactory(Expression.Constant(Math.PI)),
                new ConstantExpressionFactory(Expression.Constant(1)),
                new ConstantExpressionFactory(Expression.Constant(-1)),
                new ConstantExpressionFactory(Expression.Constant(0)),
                //new ConstantExpressionFactory(E)

                new StandardExpressionFactory( (Func<Expression>) (() => Expression.Constant(DefaultRandom.Next( Game2048Problem.GameBoardHeight * Game2048Problem.GameBoardWidth )))),

                new StandardExpressionFactory( (Func<Expression, Expression, Expression, Expression>) ((array, x, y) => 
                          Expression.ArrayAccess(array, Expression.Modulo(x, Expression.Constant(Game2048Problem.GameBoardWidth)), Expression.Modulo(y, Expression.Constant(Game2048Problem.GameBoardHeight)))),
                          x => x.Type.IsArray,
                          x => x.Type == typeof(int),
                          x => x.Type == typeof(int)),

                new RandomNumberExpressionFactory(RandomNumberType.Integer),
                //new RandomNumberExpressionFactory(RandomNumberType.Double),

                // These won't work this way (LabelTarter is not a subclass of Expression)
                //new StandardExpressionFactory((Func<Expression, LabelTarget, Expression>)((arg1, label) => Expression.Return(label, arg1))),
                //new StandardExpressionFactory((Func<LabelTarget>)Expression.Label),

                new StandardExpressionFactory((Func<Expression, Expression, Expression>)Expression.Block),
                new StandardExpressionFactory((Func<Expression, Expression, Expression, Expression>)Expression.Block),
                new StandardExpressionFactory((Func<Expression, Expression, Expression, Expression, Expression>)Expression.Block),
                new StandardExpressionFactory((Func<Expression, Expression, Expression, Expression, Expression, Expression>)Expression.Block),
            };
        }

        public void InitializeRandomPopulation(int size, int minComplexity, int maxComplexity)
        {
            Generation = 0;
            this.Size = size;

            Genomes = new List<GPGenome>();
            Random r = new Random();
            for (int i = 0; i < size; i++)
            {
                int complexity = r.Next(minComplexity, maxComplexity + 1);
                GPGenome newGenome = new GPGenome();
                newGenome.InitializeRandomGenome(complexity, Problem, MutationSettings);

                Genomes.Add(newGenome);
            }
        }

        public void ProcessGeneration()
        {
            if (Generation != 0)
            {
                // Clone best genome
                GPGenome bestGenome = GetBestGenome();

                if (bestGenome != null)
                {
                    //TODO: Very inefficient algorithm
                    for (int i = 0; i < 1; i++)
                    {
                        var newList = Genomes.Where(x => !Object.ReferenceEquals(x, bestGenome)).ToList();
                        newList.RemoveAt(RAND.Next(newList.Count));

                        newList.Add(bestGenome);
                        Genomes = newList;

                        Genomes.Add(bestGenome.Clone());
                    }
                }

                MutateGenomes();
            }
            else
            {
                EvaluateGenomes();
            }

            Generation++;
        }

        private void MutateGenomes()
        {
            GPGenome bestGenome = GetBestGenome();

            for (int i = 0; i < Genomes.Count; i++)
            {
                GPGenome genome = Genomes[i];

                if (bestGenome != null && Object.ReferenceEquals(bestGenome, genome))
                {
                    continue;
                }

                GPGenome clone = genome.Clone();
                clone.Mutate(Problem, MutationSettings);
                clone.Fitness = Problem.Evaluate(clone.CachedDelegate);

                if (clone.Fitness.Value >= genome.Fitness.Value)
                {
                    Genomes.RemoveAt(i);
                    Genomes.Insert(i, clone);
                }
            }
        }

        private void EvaluateGenomes()
        {
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
