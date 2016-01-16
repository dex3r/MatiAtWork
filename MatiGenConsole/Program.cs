using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatiGen;
using MatiGen.Problems;
using System.Linq.Expressions;

namespace MatiGenConsole
{
    class Program
    {
        static ITempDirManager TempDir;
        static IDecompiler Decompiler;

        static ProblemBase problem;
        static Population population;

        static void Main(string[] args)
        {
            InitializeProblem();

            population = new Population(problem);
            TempDir = new TempDirManager("MatiGenTest");
            Decompiler = new Decompiler(TempDir);

            PopInit();
            population.ProcessGeneration();

            GPGenome bestGenome = population.GetBestGenome();
            Report(bestGenome);

            while (true)
            {
                //PopInit();
                population.ProcessGeneration();

                bestGenome = population.GetBestGenome();
                Report(bestGenome);
            }

            Console.ReadKey();
        }

        private static void PopInit()
        {
            population.InitializeRandomPopulation(1000, 10, 20);
        }

        private static void Report(GPGenome genome)
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
            problem = new Game2048Problem();
        }
    }
}
