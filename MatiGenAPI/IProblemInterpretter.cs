using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatiGenAPI
{
    /// <summary>
    /// Abstract entity that can help interpretting a problem by a solver.
    /// </summary>
    /// <typeparam name="TProblem">Type of a problem.</typeparam>
    /// <typeparam name="TProblemResult">Type of the problem result. (ie. 'int' for '(int)a + (int)b' problem)</typeparam>
    /// <typeparam name="TProblemSolver">Type of a problem solver.</typeparam>
    public interface IProblemInterpretter<TProblem, TProblemResult, TProblemSolver>
        where TProblem : IProblem
        where TProblemSolver : IProblemSolver<TProblem, TProblemResult>
    {
    }
}
