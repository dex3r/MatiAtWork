using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen
{
    public interface IDecompiler
    {
        string DecompileExpression(LambdaExpression expression);
    }
}
