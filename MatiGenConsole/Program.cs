using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatiGen;
using System.Linq.Expressions;
using MatiGen.Problems.Maths;

namespace MatiGenConsole
{
	class Program
	{
		static ITempDirManager TempDir;
		static IDecompiler Decompiler;

		static ProblemHandler problem;
		static Population population;

		static void Main(string[] args)
		{
			InitializeProblem();

			population = new Population(problem);
			TempDir = new TempDirManager("MatiGenTest");
			Decompiler = new Decompiler(TempDir);

			PopInit();
			population.ProcessGeneration();

			Genome bestGenome = population.GetBestGenome();
			Report(bestGenome);

			bool cont = true;

			if (bestGenome != null && bestGenome.Fitness.HasValue && bestGenome.Fitness.Value == 1d)
			{
				cont = false;
			}

			while (cont)
			{
				//PopInit();
				population.ProcessGeneration();

				bestGenome = population.GetBestGenome();
				Report(bestGenome);

				if (bestGenome != null && bestGenome.Fitness.HasValue && bestGenome.Fitness.Value == 1d)
				{
					cont = false;
				}
			}

			Console.WriteLine("DONE!");
			Console.ReadKey();
		}

		private static void PopInit()
		{
			population.InitializeRandomPopulation(5000, 1, 2);
		}

		private static void Report(Genome genome)
		{
			if (genome == null)
			{
				Console.WriteLine("There is no best genome");
			}
			else
			{
				Console.WriteLine("Best genome code: ");
				Console.WriteLine(Decompiler.DecompileExpression(genome.FinalExpression));

				Console.WriteLine("Generation: " + population.Generation);
				Console.WriteLine("Best genome fitness: " + genome.Fitness.Value);
				//Console.WriteLine("Average value: " + RevertFitness(genome.Fitness.Value));

				int variablesCount = genome.UsedExpressions.Where(x => typeof(ParameterExpression).IsAssignableFrom(x.GetType())).Count();
				Console.WriteLine("Variables count: " + variablesCount);
			}
		}

		private static double RevertFitness(double fitness)
		{
			return Math.Pow(fitness, 1.0 / 14.205) * 21;
		}

		static void InitializeProblem()
		{
			//TODO: Do it in more civisiled manner.
			var evaluator = BinaryMathProblemEvaluator.AddingProblem;
			problem = new ProblemHandler(typeof(IBinaryMathProblem), evaluator);
		}
	}
}
