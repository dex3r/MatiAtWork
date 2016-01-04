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

        private double _reduceChance;

        public double ReduceChance
        {
            get { return _reduceChance; }
            private set { _reduceChance = value; }
        }

        private int _maxNodesToAdd;

        public int MaxNodesToAdd
        {
            get { return _maxNodesToAdd; }
            private set { _maxNodesToAdd = value; }
        }

        public MutationSettings(IEnumerable<IExpressionFactory> availableExpressions, double reduceChance, int maxNodesToAddAmount)
        {
            this.AvailableExpressions = availableExpressions;
            this.ReduceChance = reduceChance;
            this.MaxNodesToAdd = maxNodesToAddAmount;
        }
    }
}
