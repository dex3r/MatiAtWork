using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatiGen;
using MatiGen.Problems;

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
            population = new Population(problem);
            TempDir = new TempDirManager("MatiGenTest");
            Decompiler = new Decompiler(TempDir);

            population.InitializeRandomPopulation(500, 1, 10);
            population.ProcessGeneration();

            GPGenome bestGenome = population.GetBestGenome();

            if (bestGenome == null)
            {
                Console.WriteLine("There is no best genome");
            }
            else
            {
                Console.WriteLine("Best genome fitness: " + bestGenome.Fitness.Value);
                Console.WriteLine("Best genome code: ");
                Console.WriteLine(Decompiler.DecompileExpression(bestGenome.FinalExpression));
            }
        }

        static void InitializeProblem()
        {
            problem = new AddingProblem();
        }
    }
}
