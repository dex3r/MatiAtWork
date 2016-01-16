using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatiGenAPI
{
    /// <summary>
    /// Abstract entitity that can solve a problem.
    /// </summary>
    /// <typeparam name="TProblem">Type of a problem.</typeparam>
    /// <typeparam name="TProblemResult">Type of the problem result. (ie. 'int' for '(int)a + (int)b' problem)</typeparam>
    public interface IProblemSolver<TProblem, TProblemResult>
        where TProblem : IProblem
    {
        TProblemResult Solve(TProblem problem, IProblemInterpretter<TProblem, TProblemResult, IProblemSolver<TProblem, TProblemResult>> solver);
    }
}
