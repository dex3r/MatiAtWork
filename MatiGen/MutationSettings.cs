using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen
{
    public class MutationSettings
    {
        private IEnumerable<IExpressionFactory> _AvailableExpressions;

        public IEnumerable<IExpressionFactory> AvailableExpressions
        {
            get { return _AvailableExpressions; }
            private set { _AvailableExpressions = value; }
        }

        public MutationSettings(IEnumerable<IExpressionFactory> availableExpressions)
        {
            this.AvailableExpressions = availableExpressions;
        }
    }
}
