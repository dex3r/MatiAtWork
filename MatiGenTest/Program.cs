using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using ICSharpCode.Decompiler.Disassembler;
using ICSharpCode.Decompiler;
using System.IO;
using System.Threading;
using ICSharpCode.Decompiler.Ast;
using MatiGen;
using Utils;
using System.Diagnostics;

namespace MatiGenTesta
{
    public class Program
    {
        static ITempDirManager TempDir;
        static IDecompiler Decompiler;

        static List<Func<Expression, Expression>> unaryExps = new List<Func<Expression, Expression>>();
        static List<Func<Expression, Expression, Expression>> binaryExps = new List<Func<Expression, Expression, Expression>>();
        static List<Func<Expression, Expression, Expression, Expression>> ternaryExps = new List<Func<Expression, Expression, Expression, Expression>>();

        static void Main(string[] args)
        {
            TempDir = new TempDirManager("MatiGenTest");
            Decompiler = new Decompiler(TempDir);

            // Binary:
            binaryExps.Add(Expression.Add);
            binaryExps.Add(Expression.Subtract);
            binaryExps.Add(Expression.Multiply);
            binaryExps.Add(Expression.Divide);

            binaryExps.Add(Expression.LessThan);
            binaryExps.Add(Expression.LessThanOrEqual);
            binaryExps.Add(Expression.GreaterThan);
            binaryExps.Add(Expression.GreaterThanOrEqual);
            binaryExps.Add(Expression.Modulo);

            binaryExps.Add(Expression.IfThen);

            // Unary:
            unaryExps.Add(Expression.PostIncrementAssign);
            unaryExps.Add(Expression.PostDecrementAssign);

            Func<double, double> dd = Math.Sqrt;
            unaryExps.Add(x => Expression.Call(dd.Method, x));

            Func<double, double> sin = Math.Sin;
            unaryExps.Add(x => Expression.Call(sin.Method, x));

            // Ternary:
            ternaryExps.Add(Expression.IfThenElse);

            DoProblem();

            Console.ReadKey();
        }

        static void DoProblem()
        {
            ParameterExpression paramA = Expression.Variable(typeof(double), "paramA");
            ParameterExpression paramB = Expression.Variable(typeof(double), "paramB");

            List<ParameterExpression> parameters = new List<ParameterExpression> { paramA, paramB };

            List<Tuple<double, double, double>> trials = new List<Tuple<double, double, double>>();
            //trials.Add(CreateTest(0, 0));
            trials.Add(CreateTest(5, 5));
            trials.Add(CreateTest(7, 6));
            trials.Add(CreateTest(23, 90));

            long tries = 100;
            long totalTries = 0;
            int minExprCount = 1;
            int maxExprCount = 10;
            Random random = new Random();

            LambdaExpression bestExpr = null;
            Func<double, double, double> bestDele = null;
            double bestFitness = 0f;
            long successfullTries = 0;
            long maxFitnessCount = 0;

        redone:

            for (int i = 0; i < tries; i++)
            {
                totalTries++;

                try
                {
                    int exprCount = random.Next(minExprCount, maxExprCount + 1);
                    LambdaExpression expr = CreateRandomExpression(exprCount, parameters);

                    Func<double, double, double> dele = (Func<double, double, double>)expr.Compile();

                    int validCount = 0;

                    for (int j = 0; j < trials.Count; j++)
                    {
                        Tuple<double, double, double> validRes = trials[j];

                        double result = dele(validRes.Item2, validRes.Item3);
                        if (Math.Abs(result - validRes.Item1) < 0.001f)
                        {
                            validCount++;
                        }
                    }

                    double fitness = validCount / trials.Count;

                    //Console.WriteLine("Fitness: " + _fitness);

                    if (fitness > bestFitness)
                    {
                        bestFitness = fitness;
                        bestExpr = expr;
                        bestDele = dele;
                    }

                    if (Math.Abs(fitness - 1f) < 0.001f)
                    {
                        maxFitnessCount++;
                    }

                    successfullTries++;
                }
                catch { }
            }

            if (maxFitnessCount < 1)
            {
                goto redone;
            }

            Console.WriteLine("Best fitness: " + bestFitness);
            Console.WriteLine("Total tries: " + totalTries);
            Console.WriteLine("Successfull tries: " + successfullTries);
            Console.WriteLine("Maximum fitness runs: " + maxFitnessCount);

            if (bestExpr != null)
            {
                Console.WriteLine("Best expr: " + bestExpr.Body.ToString());
                Console.WriteLine("Best code: " + Decompiler.DecompileExpression(bestExpr));
            }
            else
            {
                Console.WriteLine("No more data to show!");
            }
        }

        private static Tuple<double, double, double> CreateTest(double a, double b)
        {
            Func<double, double, double> validSolution = (xa, xb) => Math.Sin(xa) + Math.Sin(xb);
            return new Tuple<double, double, double>(validSolution(a, b), a, b);
        }

        private static LambdaExpression CreateRandomExpression(int expressionsCount, IEnumerable<ParameterExpression> parameters)
        {
            List<Expression> expressions = new List<Expression>(parameters);
            Random rand = new Random();

            for (int i = 0; i < expressionsCount; i++)
            {
                int r1 = rand.Next(3);
                Expression selectedExpr = null;

                if (r1 == 0)
                {
                    selectedExpr = expressions.Random(rand);
                }
                else if (r1 == 1)
                {
                    var binaryExpr = binaryExps.Random(rand);
                    selectedExpr = binaryExpr(expressions.Random(rand), expressions.Random(rand));
                }
                else if (r1 == 2)
                {
                    var unaryExp = unaryExps.Random(rand);
                    selectedExpr = unaryExp(expressions.Random(rand));
                }
                else
                {
                    Debug.Assert(false, "Invalid random number!");
                }

                expressions.Add(selectedExpr);
            }

            var block = Expression.Block(expressions);
            return Expression.Lambda(block, parameters);
        }

        private static void Test()
        {
            Expression<Func<int, int, int>> multiplication;// = (a, b) => a * b;

            ParameterExpression result = Expression.Variable(typeof(int), "result");
            ParameterExpression paramA = Expression.Parameter(typeof(int), "a");
            ParameterExpression paramB = Expression.Parameter(typeof(int), "b");
            ParameterExpression loopCounter = Expression.Parameter(typeof(int), "i");

            BinaryExpression initLoop = Expression.Assign(loopCounter, paramB);
            BinaryExpression add = Expression.Add(paramA, result);
            BinaryExpression assign = Expression.Assign(result, add);

            LabelTarget labelTarget = Expression.Label("loopBreakLabel");
            GotoExpression loopBreak = Expression.Break(labelTarget);

            UnaryExpression decrementLoopCounter = Expression.PreDecrementAssign(loopCounter);
            BinaryExpression loopBreakCondition = Expression.LessThanOrEqual(loopCounter, Expression.Constant(0));
            ConditionalExpression loopBreakTest = Expression.IfThen(loopBreakCondition, loopBreak);

            Expression loopBody = Expression.Block(assign, decrementLoopCounter, loopBreakCondition, loopBreakTest);
            LoopExpression loop = Expression.Loop(loopBody, labelTarget);

            var parameters = new ParameterExpression[] { result, loopCounter };
            BlockExpression block = Expression.Block(parameters, initLoop, loop, result);
            //BlockExpression block = Expression.Block(parameters, initLoop, loop, result);

            multiplication = Expression.Lambda<Func<int, int, int>>(block, paramA, paramB);

            Console.WriteLine("Body: " + multiplication.Body);
            Console.WriteLine("Block type: " + block.Type);

            var l = multiplication.Compile();
            var r = l(3, 55);

            Console.WriteLine("Result (3, 5): " + r);

            Console.WriteLine(Decompiler.DecompileExpression(multiplication));
        }
    }
}
