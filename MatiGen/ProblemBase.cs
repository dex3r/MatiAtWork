using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen
{
    public abstract class ProblemBase
    {
        private IEnumerable<ParameterExpression> _Parameters = new List<ParameterExpression>();

        public IEnumerable<ParameterExpression> Parameters
        {
            get { return _Parameters; }
            protected set { _Parameters = value; }
        }

        public ProblemBase()
        {
            Parameters = CreateParameters();
        }

        protected virtual IEnumerable<ParameterExpression> CreateParameters()
        {
            return Enumerable.Empty<ParameterExpression>();
        }

        public abstract double Evaluate(Delegate solverMethod);
    }
}
