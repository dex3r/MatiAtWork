﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatiGen.GenericProblems
{
	public interface IGenericAddingProblem
	{
		int A { get; }
		int B { get; }

		void SendResult(int result);
	}
}
